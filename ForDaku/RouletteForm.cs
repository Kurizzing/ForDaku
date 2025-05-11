using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace ForDaku
{
    
    public partial class RouletteForm : Form
    {
        private MemoForm memoForm;
        List<(string, int)> itemList;
        float rotationAngle = 0f;
        Timer spinTimer;
        float spinVelocity = 40f; // 초기 속도
        float spinDeceleration = 0.5f; // 감속도

        bool isDecelerating = false;
        float minVelocity = 0.1f; // 멈출 기준 속도

        int rouletteBorderWidth = 5; // 룰렛 테두리 두께
        int triangleHeight = 40; // 삼각형 높이
        int triangleWidth = 40; // 삼각형 너비
        float margin = 20;

        // 회전 버튼
        int originalRotateButtonY;          // 버튼의 원래 Y 위치
        bool moveUp = true;     // 위로 움직일지 아래로 움직일지 플래그
        int amplitude = 5;      // 진동 범위
        int speed = 75;         // 타이머 간격(ms)
        Timer timer1 = new Timer();
        
        MyListItem prizeItem = null; // 당첨 아이템
        Font prizeFont = new Font("굴림", 30, FontStyle.Bold);

        // for test
        public RouletteForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // 더블 버퍼링 활성화


            string txt = File.ReadAllText("C:/Users/lkuku/Desktop/a.txt");

            itemList = new List<(string, int)>();
            string[] lines = txt.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);


            foreach (var line in lines)
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    string deckName = parts[0];
                    int count = int.Parse(parts[1]);

                    itemList.Add((deckName, count));
                }
            }

            if (itemList != null)
                for (int i = 0; i < itemList.Count; i++)
                {
                    addItemToRoulette(itemList[i].Item1, itemList[i].Item2);
                }

            this.SizeChanged += Form1_SizeChanged;


            RepositionControls();
            
            timer1.Interval = speed;
            timer1.Tick += Timer1_Tick;

        }

        public RouletteForm(MemoForm memoForm, List<(string, int)> itemList)
        {
            this.memoForm = memoForm;
            if (itemList != null)
            {
                this.itemList = itemList;
            }

            InitializeComponent();
            this.DoubleBuffered = true; // 더블 버퍼링 활성화

            if (itemList != null)
                for (int i = 0; i < itemList.Count; i++)
                {
                    addItemToRoulette(itemList[i].Item1, itemList[i].Item2);
                }

            this.SizeChanged += Form1_SizeChanged;

            RepositionControls();
            timer1.Interval = speed;
            timer1.Tick += Timer1_Tick;


        }

        private void RepositionControls()
        {
            // ! 룰렛 연관 컨트롤들 화면 배치
            // 크기 조정
            roulettePanel.Height = (ClientSize.Height - rotateButton.Height - prizePanel.Height - triangleHeight - 150);
            roulettePanel.Width = roulettePanel.Height;

            //// prizePanel 크기 조정
            //// 텍스트 크기 계산
            //SizeF textSize;
            //using (Graphics g = this.CreateGraphics())
            //{
            //    textSize = g.MeasureString("Hello", this.Font);
            //}
            //prizePanel.Width = (int)(textSize.Width);
            //prizePanel.Height = (int)(textSize.Height); // 텍스트 크기 조정

            prizePanel.Width = (int)(roulettePanel.Width * 0.9f);

            // 위치 조정
            float panelX = this.ClientSize.Width / 4;
            float panelY = ClientSize.Height / 2;

            //float prizeLabelX = panelX;
            //float prizeLabelL = CenterToLT(prizeLabelX, 20, prizeLabel.Width, prizeLabel.Height).Item1;
            //float prizeLabelT = margin;
            //prizeLabel.Location = new Point((int)prizeLabelL, (int)prizeLabelT);

            float prizePanelX = panelX;
            float prizePanelL = CenterToLT(prizePanelX, 20, prizePanel.Width, prizePanel.Height).Item1;
            float prizePanelT = margin;
            prizePanel.Location = new Point((int)prizePanelL, (int)prizePanelT);

            triangleDrawPanel.Width = triangleWidth;
            triangleDrawPanel.Height = triangleHeight;
            float trianglePanelX = panelX;
            float trianglePanelL = CenterToLT(trianglePanelX, 20, triangleDrawPanel.Width, triangleDrawPanel.Height).Item1;
            float trianglePanelT = prizePanelT + prizePanel.Height + margin * 0.5f;

            triangleDrawPanel.Location = new Point((int)trianglePanelL, (int)trianglePanelT);

            float panelL = CenterToLT(panelX, panelY, roulettePanel.Width, roulettePanel.Height).Item1;
            float panelT = trianglePanelT + triangleDrawPanel.Height;
            roulettePanel.Location = new Point((int)panelL, (int)panelT);

            // rotateButton은 남은 공간 중앙에 위치
            float remainingSpace = ClientSize.Height - (panelT + roulettePanel.Height);
            rotateButton.Width = (int)(roulettePanel.Width * 0.5f);
            float rotateButtonX = panelX;
            float rotateButtonY = panelT + roulettePanel.Height + remainingSpace / 2;
            (float rotateButtonL, float rotateButtonT) = CenterToLT(rotateButtonX, rotateButtonY, rotateButton.Width, rotateButton.Height);
            //float rotateButtonT = panelT + roulettePanel.Height + margin;
            rotateButton.Location = new Point((int)rotateButtonL, (int)rotateButtonT);
            originalRotateButtonY = rotateButton.Location.Y; // 버튼의 원래 Y 위치 저장

            UpdateRoulette();
            UpdatePrizePanel();

            // ! 항목 리스트, 타이머 화면 배치
            // 항목 리스트 가로: 클라이언트의 1/4
            // 항목 리스트 세로: 타이머, 항목 아이템 추가 제외한 모든 공간
            // 항목 리스트 바로 밑에 항목 아이템 추가, 그 밑에 타이머 순
            // Center 기준으로 할 필요없이 LT로 다 될듰? 룰렛 패널 기준으로

            //flowLayoutPanel.Width = (int)(ClientSize.Width / 4);
            flowLayoutPanel.Height = (int)(ClientSize.Height - (timerControl1.Height + addListItem.Height + margin * 4));
            float flowLayoutPanelL = roulettePanel.Location.X + roulettePanel.Width + margin;

            timerControl1.Location = new Point((int)(flowLayoutPanelL), (int)(margin));
            flowLayoutPanel.Location = new Point((int)(flowLayoutPanelL), (int)(timerControl1.Location.Y + timerControl1.Height + margin));

            addListItem.Location = new Point((int)(flowLayoutPanel.Location.X), (int)(flowLayoutPanel.Location.Y + flowLayoutPanel.Height + margin));


            //flowLayoutPanel.Height = (int)(ClientSize.Height - (timerControl1.Height + addListItem.Height + margin * 4));
            //flowLayoutPanel.Location = new Point((int)(roulettePanel.Location.X + roulettePanel.Width + margin), (int)margin);

            //addListItem.Location = new Point((int)(flowLayoutPanel.Location.X), (int)(flowLayoutPanel.Location.Y + flowLayoutPanel.Height + margin));

            //timerControl1.Location = new Point((int)(flowLayoutPanel.Location.X), (int)(addListItem.Location.Y + addListItem.Height + margin));
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            RepositionControls();
        }

        

        private (float, float) LTToCenter(float x, float y, float width, float height)
        {
            return (x + width / 2, y + height / 2);
        }

        private (float, float) CenterToLT(float x, float y, float width, float height)
        {
            return (x - width / 2, y - height / 2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void triangleDrawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 중심을 가운데로 이동
            g.TranslateTransform(triangleDrawPanel.Width / 2, 0);

            // 삼각형 좌표 (아래쪽을 향하는 삼각형)
            Point[] triangle =
            {
                new Point(0, triangleHeight),     // 꼭짓점 아래쪽
                new Point((int)(-triangleWidth/2.0f), 0),   // 왼쪽 위
                new Point((int)(+triangleWidth/2.0f), 0)     // 오른쪽 위
            };
            g.FillPolygon(Brushes.Red, triangle);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //g.ResetTransform(); // 변환 초기화
            //g.TranslateTransform(0, triangleHeight);
            // todo: 패널 높이 조절 자동화 필요

            DrawRoulette(g, roulettePanel.Width, roulettePanel.Height);  // 이 결과가 가로세로 비율 1:1이 되어야 원이 됨
        }

        void StartSpin()
        {
            spinVelocity = 40f;
            isDecelerating = false;

            if (spinTimer == null)
            {
                spinTimer = new Timer();
                spinTimer.Interval = 16;
                spinTimer.Tick += SpinTimer_Tick;
            }

            spinTimer.Start();
        }

        void StartDeceleration()
        {
            isDecelerating = true;
        }

        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            rotationAngle += spinVelocity;
            if (rotationAngle >= 360f)
                rotationAngle -= 360f;

            // 감속 중일 때만 속도 감소
            if (isDecelerating)
            {
                spinVelocity -= spinDeceleration;
                if (spinVelocity <= minVelocity)
                {
                    spinVelocity = 0;
                    spinTimer.Stop();
                    isDecelerating = false;

                    rotateButton.Text = "시작";
                    rotateButton.BackColor = SystemColors.ControlLight; // 색상 변경

                    rotateButton.Enabled = true;
                }
            }

            UpdateRoulette();
        }

        void DrawRoulette(Graphics g, int width, int height)
        {
            label1.Text = $"{width} {height}";
            if (GetAllCount() == 0)
            {
                return;
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 중심점 계산
            Rectangle rect = new Rectangle(0, 0, width, height); // 원 영역
            PointF center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

            List<MyListItem> itemList = new List<MyListItem>();
            // flowlayout에서 리스트 추출
            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                if (ctrl is MyListItem item)
                {
                    itemList.Add(item);
                }
            }

            Brush brush = Brushes.Blue; // 색상 선택
            float startAngle = 0;       // 시작 각도 (0도)
            float sweepAngle = 0;     // 파이 조각 각도 (120도)

            // 회전 적용
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(rotationAngle);
            g.TranslateTransform(-center.X, -center.Y);

            // 원 그리기
            foreach (MyListItem item in itemList)
            {
                brush = new SolidBrush(item.ItemColor);
                float probability = item.NumericUpDownValue / (float)GetAllCount();
                sweepAngle = ProbabilityToDegree(probability);

                g.FillPie(brush, rect, startAngle, sweepAngle);

                startAngle += sweepAngle;
            }

            // 원 테두리 그리기

            // borderWidth는 그린 영역 안으로 절반 밖으로 절반 생김
            // 원 주위에 20픽셀 정도 검은색 테두리 추가
            using (Pen borderPen = new Pen(Color.Black, rouletteBorderWidth)) // 테두리 색상과 두께 설정
            {
                //Rectangle borderRect = new Rectangle(rect.X + rouletteBorderWidth, rect.Y + rouletteBorderWidth, rect.Width - rouletteBorderWidth, rect.Height - rouletteBorderWidth);
                g.TranslateTransform(rouletteBorderWidth * 0.5f, rouletteBorderWidth * 0.5f);
                Rectangle borderRect = new Rectangle(0, 0, width - rouletteBorderWidth, height - rouletteBorderWidth);
                g.DrawEllipse(borderPen, borderRect); // 원 테두리 그리기

                g.TranslateTransform(-rouletteBorderWidth * 0.5f, -rouletteBorderWidth * 0.5f); // 돌아가기
            }

            // 항목 이름 그리기
            startAngle = 0; // 시작 각도 초기화
            sweepAngle = 0;
            foreach (MyListItem item in itemList)
            {
                float probability = item.NumericUpDownValue / (float)GetAllCount();
                sweepAngle = ProbabilityToDegree(probability);

                // 중간 각도
                float midAngle = startAngle + sweepAngle / 2;
                float radius = rect.Width / 2 * 0.7f;
                double radians = midAngle * Math.PI / 180;

                PointF textPos = new PointF(
                    center.X + (float)(radius * Math.Cos(radians)),
                    center.Y + (float)(radius * Math.Sin(radians))
                );

                //using (Font font = new Font("굴림", 16))
                //using (StringFormat sf = new StringFormat())
                //{
                //    sf.Alignment = StringAlignment.Center;         // 수평 중앙 정렬
                //    sf.LineAlignment = StringAlignment.Center;     // 수직 중앙 정렬

                //    var state = g.Save();
                //    g.TranslateTransform(textPos.X, textPos.Y);
                //    g.RotateTransform(midAngle);
                //    g.DrawString(item.TextBoxValue, font, Brushes.Black, PointF.Empty, sf);
                //    g.Restore(state);
                //}

                Font font = new Font("굴림", 20, FontStyle.Bold);
                string text = item.TextBoxValue;

                // 텍스트 크기 계산
                SizeF textSize = g.MeasureString(text, font);

                if (textSize.Width > roulettePanel.Width * 0.4f)
                {
                    // 텍스트가 패널보다 길 경우
                    float scale = (roulettePanel.Width * 0.4f) / textSize.Width;
                    font = new Font(font.FontFamily, font.Size * scale, font.Style);
                    textSize = g.MeasureString(text, font);
                }

                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(
                            text,
                            font.FontFamily,
                            (int)font.Style,
                            g.DpiY * font.Size / 72f,
                            PointF.Empty,
                            sf
                        );

                        var state = g.Save();

                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias; // ✅ 안티에일리어싱 적용
                        g.TranslateTransform(textPos.X, textPos.Y);
                        g.RotateTransform(midAngle);

                        using (Pen outlinePen = new Pen(Color.Black, 4f) { LineJoin = LineJoin.Round })
                        {
                            g.DrawPath(outlinePen, path);
                        }

                        g.FillPath(Brushes.White, path);

                        g.Restore(state);
                    }
                }

                //using (Font font = new Font("굴림", 16, FontStyle.Bold)) // Bold가 테두리 효과에 잘 보임
                //using (StringFormat sf = new StringFormat())
                //{
                //    sf.Alignment = StringAlignment.Center;
                //    sf.LineAlignment = StringAlignment.Center;

                //    var state = g.Save();
                //    g.TranslateTransform(textPos.X, textPos.Y);
                //    g.RotateTransform(midAngle);

                //    // 테두리용: 검정색을 여러 방향으로 오프셋하여 그림
                //    int outlineSize = 2;
                //    for (int dx = -outlineSize; dx <= outlineSize; dx++)
                //    {
                //        for (int dy = -outlineSize; dy <= outlineSize; dy++)
                //        {
                //            if (dx == 0 && dy == 0) continue;
                //            PointF offsetPoint = new PointF(dx, dy);
                //            g.DrawString(item.TextBoxValue, font, Brushes.Black, offsetPoint, sf);
                //        }
                //    }

                //    // 본문용: 중심에 흰색으로 텍스트 그림
                //    g.DrawString(item.TextBoxValue, font, Brushes.White, PointF.Empty, sf);

                //    g.Restore(state);
                //}

                // 현재 당첨 아이템
                if (startAngle <= PointDegree(rotationAngle) && PointDegree(rotationAngle) <= startAngle + sweepAngle)
                {
                    if (prizeItem != item)
                    {
                        prizeItem = item;
                        UpdatePrizePanel();
                    }
                    
                    //PrizeLabelUpdate(item.TextBoxValue, e);

                }

                startAngle += sweepAngle;
            }


            
        }

        float PointDegree(float degree)
        {
            return (270 - degree + 360) % 360;
        }

        void UpdateRoulette()
        {
            roulettePanel.Invalidate();
            roulettePanel.Update();
        }

        float ProbabilityToDegree(float probability)
        {
            return (float)(probability * 360.0);
        }

        float DegreeToRadian(float degree)
        {
            return (float)(degree * Math.PI / 180.0);
        }

        float radianToDegree(float radian)
        {
            return (float)(radian * 180.0 / Math.PI);
        }

        int GetAllCount()
        {
            int count = 0;
            foreach (MyListItem item in flowLayoutPanel.Controls)
            {
                count += item.NumericUpDownValue;
            }
            return count;
        }

        private void addItemToRoulette(string textBoxValue, int numericUpDownValue)
        {
            var item = new MyListItem();
            item.ItemColor = GenerateDistinctColor();
            item.TextBoxValue = textBoxValue;
            item.TextBoxControl.TextChanged += (s, ev) =>
            {
                UpdateRoulette();
                UpdatePrizePanel();
            };
            item.NumericUpDownValue = numericUpDownValue;
            item.ButtonControl.Text = "-";
            item.ButtonControl.BackColor = Color.LightPink;
            item.NumericUpDownControl.ValueChanged += (s, ev) =>
            {
                UpdateProbability();
                UpdateRoulette();
                UpdatePrizePanel();
            };
            item.ButtonControl.Click += (s, ev) =>
            {
                flowLayoutPanel.Controls.Remove(item);
                UpdateProbability();
                UpdateRoulette();
                UpdatePrizePanel();
            };
            flowLayoutPanel.Controls.Add(item);
            UpdateProbability();
            UpdateRoulette();
            UpdatePrizePanel();
        }

        /// <summary>
        /// 버튼 클릭 시 새로운 MyListItem을 생성하여 FlowLayoutPanel에 추가합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            addItemToRoulette(addListItem.TextBoxValue, addListItem.NumericUpDownValue);

        }

        public void UpdateProbability()
        {
            int allCount = GetAllCount();
            foreach (MyListItem item in flowLayoutPanel.Controls)
            {
                item.LabelText = (item.NumericUpDownValue / (float)allCount * 100).ToString("0.0") + "%";
            }
        }

        private void myListItem1_Load(object sender, EventArgs e)
        {

        }

        List<double> usedHues = new List<double>();
        Random rnd = new Random();

        // (newHue, 0.6, 0.95); -> 153, 242.25
        Color GenerateDistinctColor()
        {
            int maxAttempts = 100;
            // s: 채도 색의 선명한 정도, 맑고 탁한 정도, 채도가 높으면 다른 색이 섞이지 않음
            int sMin = 130;
            int sMax = 150;
            // v: 명도 색의 밝기 정도, 색이 얼마나 밝은지 어두운지
            int vMin = 220;
            int vMax = 240;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                double newHue = rnd.NextDouble() * 360;

                if (usedHues.All(h => Math.Abs(h - newHue) >= 30 || Math.Abs(h - newHue) >= 330))
                {
                    usedHues.Add(newHue);

                    double s = rnd.Next(sMin, sMax) / 255.0;

                    double v = rnd.Next(vMin, vMax) / 255.0;

                    return ColorFromHSV(newHue, s, v);
                }
            }

            // fallback (비슷한 색이라도 무조건 하나 리턴)
            double fallbackHue = rnd.NextDouble() * 360;
            double fallbackS = rnd.Next(sMin, sMax) / 255.0;
            double fallbackV = rnd.Next(vMin, vMax) / 255.0;

            return ColorFromHSV(fallbackHue, fallbackS, fallbackV);
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0: return Color.FromArgb(v, t, p);
                case 1: return Color.FromArgb(q, v, p);
                case 2: return Color.FromArgb(p, v, t);
                case 3: return Color.FromArgb(p, q, v);
                case 4: return Color.FromArgb(t, p, v);
                default: return Color.FromArgb(v, p, q);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            if (rotateButton.Text == "시작")
            {
                StartSpin();                   // 회전 시작
                rotateButton.Text = "정지!";         // 텍스트 변경
                rotateButton.BackColor = Color.LightPink; // 색상 변경
                timer1.Start();

            }
            else if (rotateButton.Text == "정지!")
            {
                // 버튼 클릭 시 애니메이션 중지
                timer1.Stop();
                rotateButton.Location = new Point(rotateButton.Location.X, originalRotateButtonY); // 원래 위치로 복원

                StartDeceleration();           // 감속 시작
                rotateButton.Enabled = false;       // 버튼 비활성화
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // 위아래로 움직임
            if (moveUp)
            {
                rotateButton.Top = originalRotateButtonY - amplitude;
            }
            else
            {
                rotateButton.Top = originalRotateButtonY + amplitude;
            }

            moveUp = !moveUp; // 방향 반전
        }

        private void ToMemoFormButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            memoForm.Show();
        }

        private void prizeLabel_Click(object sender, EventArgs e)
        {

        }

        void UpdatePrizePanel()
        {
            prizePanel.Invalidate();
            prizePanel.Update();
        }


        private void timerControl1_Load(object sender, EventArgs e)
        {

        }

        private void myListItem1_Load_1(object sender, EventArgs e)
        {
            addListItem.ButtonControl.Text = "+";
            addListItem.ButtonControl.BackColor = Color.LightGreen;
            addListItem.LabelText = "";
            addListItem.ButtonControl.Click += (s, ev) =>
            {
                flowLayoutPanel.SuspendLayout();
                addItemToRoulette(addListItem.TextBoxValue, addListItem.NumericUpDownValue);


                flowLayoutPanel.ResumeLayout(true);
                // 스크롤 맨 아래로
                flowLayoutPanel.ScrollControlIntoView(flowLayoutPanel.Controls[flowLayoutPanel.Controls.Count-1]);
            };
        }

        private void prizePanel_Paint(object sender, PaintEventArgs e)
        {
            if (prizeItem == null)
            {
                return;
            }
            // 텍스트 설정
            string text = prizeItem.TextBoxValue;
            Font font = prizeFont;
            Brush innerBrush = new SolidBrush(prizeItem.ItemColor);  // 텍스트 내부 색 (지정색)
            Pen outerPen = new Pen(Color.Black, 5f) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round };  // 텍스트 외곽 색 (검은색)

            // StringFormat 설정
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias; // ✅ 안티에일리어싱 적용

            // 텍스트 크기 계산
            SizeF textSize = e.Graphics.MeasureString(text, font);

            if (textSize.Width > prizePanel.Width)
            {
                // 텍스트가 패널보다 길 경우
                float scale = prizePanel.Width / textSize.Width;
                font = new Font(font.FontFamily, font.Size * scale, font.Style);
                textSize = e.Graphics.MeasureString(text, font);
            }

            //// 텍스트를 중앙에 배치하기 위한 좌표 계산
            //float x = (prizeLabel.Width - textSize.Width) / 2;
            //float y = (prizeLabel.Height - textSize.Height) / 2;

            // GraphicsPath를 사용하여 텍스트를 추가
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddString(text, font.FontFamily, (int)font.Style, e.Graphics.DpiY * font.Size / 72f, new PointF(prizePanel.Width * 0.5f, prizePanel.Height * 0.5f), stringFormat);

                // 텍스트 외곽선 그리기 (검은색)
                e.Graphics.DrawPath(outerPen, path);

                // 텍스트 내부 채우기 (지정색)
                e.Graphics.FillPath(innerBrush, path);
            }
        }
    }

    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
