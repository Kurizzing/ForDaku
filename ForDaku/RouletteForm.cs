using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using Font = System.Drawing.Font;

namespace ForDaku
{

    public partial class RouletteForm : Form
    {
        private MemoForm memoForm;
        List<(string, int)> itemList;

        private float rotationAngle = 0f;  // 현재 회전 각도
        private float spinVelocity;        // 회전 속도
        private Timer spinTimer = null;           // 타이머
        private Stopwatch spinStopwatch = new Stopwatch(); // 스톱워치 (회전 시간 측정)
        private const float spinStartVelocity = 40f; // 초기 회전 속도

        private readonly Brush triangleBrush = new SolidBrush(Color.Red); // 삼각형 색상
        private readonly Font itemFont = new("굴림체", 30, FontStyle.Bold);
        private readonly Font prizeFont = new Font("굴림체", 40, FontStyle.Bold);

        private const int extraSpins = 15; // 추가 회전 수
        private const string rotateText = "시작";
        private const string stopText = "정지";
        private readonly Color rotateColor = SystemColors.ControlLight;
        private readonly Color stopColor = Color.LightPink;

        private const int rouletteBorderWidth = 5; // 룰렛 테두리 두께
        private const int triangleHeight = 40; // 삼각형 높이
        private const int triangleWidth = 40; // 삼각형 너비
        private const float margin = 20f;

        // s: 채도 색의 선명한 정도, 맑고 탁한 정도, 채도가 높으면 다른 색이 섞이지 않음
        private const int sMin = 130;
        private const int sMax = 150;
        // v: 명도 색의 밝기 정도, 색이 얼마나 밝은지 어두운지
        private const int vMin = 220;
        private const int vMax = 240;

        // 회전 버튼
        int originalRotateButtonY;          // 버튼의 원래 Y 위치
        bool moveUp = true;                 // 위로 움직일지 아래로 움직일지 플래그
        int amplitude = 5;                  // 진동 범위
        int speed = 75;                     // 타이머 간격(ms)
        Timer buttonVibrationTimer = new Timer();

        Random random = new Random(); // 클래스 맨 위에 선언
        float targetAngle = 0f;          // 감속 후 도달할 목표 각도
        float startAngle = 0f;           // 감속 시작 각도
        bool isStopping = false;         // 정지 중 플래그
        private const float decelerationDuration = 10f; // 감속 시간 (초)
        //float elapsedTime = 0f;          // 경과 시간

        List<double> usedHues = new List<double>();
        MyListItem prizeItem = null; // 당첨 아이템
        

        // for test
        public RouletteForm()
        {
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

            InitializeComponent();
            this.DoubleBuffered = true; // 더블 버퍼링 활성화


            if (itemList != null)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    AddItemToRoulette(itemList[i].Item1, itemList[i].Item2);
                }
            }

            this.SizeChanged += RouletteForm_SizeChanged;
            RepositionControls();
            InitializeControls();
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
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    AddItemToRoulette(itemList[i].Item1, itemList[i].Item2);
                }
            }

            this.SizeChanged += RouletteForm_SizeChanged;

            RepositionControls();
            InitializeControls();

        }

        private void InitializeControls()
        {
            buttonVibrationTimer.Interval = speed;
            buttonVibrationTimer.Tick += Timer1_Tick;

            resultLabel.AutoSize = false;
            resultLabel.BackColor = Color.White;  // 필요 시 반투명도 색 설정
            resultLabel.ForeColor = Color.Black;
            resultLabel.Font = new Font("굴림", 30, FontStyle.Bold);
            resultLabel.TextAlign = ContentAlignment.MiddleCenter;
            resultLabel.BringToFront(); // 다른 컨트롤보다 앞에 오도록
            resultLabel.Visible = false;
        }

        private void ShowResult(string result)
        {
            resultLabel.Text = result;
            resultLabel.BringToFront();  // 가장 앞에 위치시킴
            resultLabel.Visible = true;

            Timer resultTimer = new Timer();
            resultTimer.Interval = 2000;
            resultTimer.Tick += (s, e) =>
            {
                resultTimer.Stop();
                resultLabel.Visible = false;
                resultTimer.Dispose();
            };
            resultTimer.Start();
        }

        private void RepositionControls()
        {
            // ! 룰렛 연관 컨트롤들 화면 배치
            // 크기 조정
            roulettePanel.Height = (ClientSize.Height - rotateButton.Height - prizePanel.Height - triangleHeight - 150);
            roulettePanel.Width = roulettePanel.Height;

            resultLabel.Width = this.ClientSize.Width;
            resultLabel.Height = 70; // 높이 설정
            resultLabel.Location = new Point(0, (this.ClientSize.Height - resultLabel.Height) / 2);

            prizePanel.Width = (int)(roulettePanel.Width * 0.9f);

            // 위치 조정
            float panelX = this.ClientSize.Width / 4;
            float panelY = ClientSize.Height / 2;

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
            
            rotateButton.Location = new Point((int)rotateButtonL, (int)rotateButtonT);
            originalRotateButtonY = rotateButton.Location.Y; // 버튼의 원래 Y 위치 저장

            UpdateRoulette();
            UpdatePrizePanel();

            // ! 항목 리스트, 타이머 화면 배치
            // 항목 리스트 가로: 클라이언트의 1/4
            // 항목 리스트 세로: 타이머, 항목 아이템 추가 제외한 모든 공간
            // 항목 리스트 바로 밑에 항목 아이템 추가, 그 밑에 타이머 순

            flowLayoutPanel.Height = (int)(ClientSize.Height - (timerControl.Height + addListItem.Height + margin * 4));
            float flowLayoutPanelL = roulettePanel.Location.X + roulettePanel.Width + margin * 5;

            timerControl.Location = new Point((int)(flowLayoutPanelL), (int)(margin));
            flowLayoutPanel.Location = new Point((int)(flowLayoutPanelL), (int)(timerControl.Location.Y + timerControl.Height + margin));

            addListItem.Location = new Point((int)(flowLayoutPanel.Location.X), (int)(flowLayoutPanel.Location.Y + flowLayoutPanel.Height + margin));
        }

        private void RouletteForm_SizeChanged(object sender, EventArgs e)
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

        private void TriangleDrawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 중심을 가운데로 이동
            g.TranslateTransform(triangleDrawPanel.Width / 2, 0);

            // 삼각형 좌표 (아래쪽을 향하는 삼각형)
            Point[] triangle =
            {
                new Point(0, triangleHeight),               // 꼭짓점 아래쪽
                new Point((int)(-triangleWidth/2.0f), 0),   // 왼쪽 위
                new Point((int)(+triangleWidth/2.0f), 0)    // 오른쪽 위
            };
            g.FillPolygon(triangleBrush, triangle);
        }

        private void RoulettePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawRoulette(g, roulettePanel.Width, roulettePanel.Height);
        }

        void DrawRoulette(Graphics g, int width, int height)
        {
            if (GetAllCount() == 0)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 중심점 계산
            Rectangle rect = new Rectangle(0, 0, width, height); // 원 영역
            PointF center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

            // flowlayout에서 리스트 추출
            List<MyListItem> itemList = new List<MyListItem>();
            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                if (ctrl is MyListItem item)
                {
                    itemList.Add(item);
                }
            }

            Brush brush = Brushes.Blue; // 색상 선택(임시)
            float startAngle = 0;       // 시작 각도 (0도)
            float sweepAngle = 0;       // 파이 조각 각도 (120도)

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
            using (Pen borderPen = new Pen(Color.Black, rouletteBorderWidth)) // 테두리 색상과 두께 설정
            {
                // borderWidth는 그린 영역 안으로 절반 밖으로 절반 생김
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

                string text = item.TextBoxValue;

                Font font = itemFont; // 폰트 설정
                // 텍스트 크기 계산
                SizeF textSize = g.MeasureString(text, font);

                if (textSize.Width > roulettePanel.Width * 0.4f)
                {
                    // 텍스트가 패널보다 길 경우
                    float scale = (roulettePanel.Width * 0.4f) / textSize.Width;
                    font = new Font(font.FontFamily, font.Size * scale, font.Style);
                    //textSize = g.MeasureString(text, font);
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

                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
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

                // 현재 당첨 아이템
                if (startAngle <= PointDegree(rotationAngle) && PointDegree(rotationAngle) <= startAngle + sweepAngle)
                {
                    if (prizeItem != item)
                    {
                        prizeItem = item;
                        UpdatePrizePanel();
                    }
                }

                startAngle += sweepAngle;
            }
        }

        float PointDegree(float degree)
        {
            float result = ((270 - degree) % 360 + 360) % 360;
            return result;
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

        private void AddItemToRoulette(string textBoxValue, int numericUpDownValue)
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

        void StartDeceleration()
        {
            if (isStopping) return;

            isStopping = true;
            //elapsedTime = 0f;
            startAngle = rotationAngle;

            float randomTargetOffset = random.Next(0, 360);
            float fullSpins = extraSpins * 360;
            targetAngle = startAngle + fullSpins + randomTargetOffset;
        }

        float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            if (isStopping)
            {
                //elapsedTime += spinTimer.Interval / 1000f;
                float elapsedTime = spinStopwatch.ElapsedMilliseconds / 1000f;
                float t = Math.Min(elapsedTime / decelerationDuration, 1f); // 0~1 보간

                // 부드러운 ease-out 감속 (곡선 사용)
                //float smoothT = 1 - (1 - t) * (1 - t); // easeOutQuad
                float smoothT = 1 - (float)Math.Pow(1 - t, 3); // EaseOutCubic

                rotationAngle = Lerp(startAngle, targetAngle, smoothT);

                if (t >= 1f)
                {
                    isStopping = false;
                    spinTimer.Stop();
                    spinStopwatch.Stop(); // 스톱워치 정지
                    spinVelocity = 0f;

                    // 당첨 항목 계산
                    ShowResult(prizeItem.TextBoxValue);

                    rotateButton.Text = rotateText;
                    rotateButton.BackColor = rotateColor;
                    rotateButton.Enabled = true;
                }
            }
            else
            {
                // 계속 회전 중
                rotationAngle += spinVelocity;
                if (rotationAngle >= 360f) rotationAngle -= 360f;
            }

            UpdateRoulette();
        }

        void StartSpin()
        {
            // 회전 속도 초기화
            spinVelocity = spinStartVelocity;

            // 타이머가 없으면 새로 생성
            if (spinTimer == null)
            {
                spinTimer = new Timer();
                spinTimer.Interval = 16;  // 16ms마다 타이머 틱 (약 60FPS)
                spinTimer.Tick += SpinTimer_Tick;
            }

            // 타이머 시작
            spinTimer.Start();
            spinStopwatch.Restart(); // 스톱워치 시작
        }

        public void UpdateProbability()
        {
            int allCount = GetAllCount();
            foreach (MyListItem item in flowLayoutPanel.Controls)
            {
                item.LabelText = (item.NumericUpDownValue / (float)allCount * 100).ToString("0.0") + "%";
            }
        }

        // (newHue, 0.6, 0.95); -> 153, 242.25
        Color GenerateDistinctColor()
        {
            int maxAttempts = 100;
            
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                double newHue = random.NextDouble() * 360;

                if (usedHues.All(h => Math.Abs(h - newHue) >= 30 || Math.Abs(h - newHue) >= 330))
                {
                    usedHues.Add(newHue);

                    double s = random.Next(sMin, sMax) / 255.0;

                    double v = random.Next(vMin, vMax) / 255.0;

                    return ColorFromHSV(newHue, s, v);
                }
            }

            // fallback (비슷한 색이라도 무조건 하나 리턴)
            double fallbackHue = random.NextDouble() * 360;
            double fallbackS = random.Next(sMin, sMax) / 255.0;
            double fallbackV = random.Next(vMin, vMax) / 255.0;

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

        private void RotateButton_Click(object sender, EventArgs e)
        {
            if (rotateButton.Text == rotateText && GetAllCount() > 0)
            {
                StartSpin();                            // 회전 시작
                rotateButton.Text = stopText;           // 텍스트 변경
                rotateButton.BackColor = stopColor;     // 색상 변경
                buttonVibrationTimer.Start();
            }
            else if (rotateButton.Text == stopText)
            {
                buttonVibrationTimer.Stop();                       // 버튼 클릭 시 애니메이션 중지
                rotateButton.Location = new Point(rotateButton.Location.X, originalRotateButtonY); // 원래 위치로 복원

                StartDeceleration();                // 감속 시작
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

        void UpdatePrizePanel()
        {
            prizePanel.Invalidate();
            prizePanel.Update();
        }

        private void AddListItem_Load(object sender, EventArgs e)
        {
            addListItem.ButtonControl.Text = "+";
            addListItem.ButtonControl.BackColor = Color.LightGreen;
            addListItem.LabelText = "";
            addListItem.ButtonControl.Click += (s, ev) =>
            {
                flowLayoutPanel.SuspendLayout();
                AddItemToRoulette(addListItem.TextBoxValue, addListItem.NumericUpDownValue);

                flowLayoutPanel.ResumeLayout(true);
                // 스크롤 맨 아래로
                flowLayoutPanel.ScrollControlIntoView(flowLayoutPanel.Controls[flowLayoutPanel.Controls.Count - 1]);
            };
        }

        private void PrizePanel_Paint(object sender, PaintEventArgs e)
        {
            if (prizeItem == null)
                return;

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

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // 텍스트 크기 계산
            SizeF textSize = e.Graphics.MeasureString(text, font);

            if (textSize.Width > prizePanel.Width)
            {
                // 텍스트가 패널보다 길 경우
                float scale = prizePanel.Width / textSize.Width;
                font = new Font(font.FontFamily, font.Size * scale, font.Style);
                textSize = e.Graphics.MeasureString(text, font);
            }

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
    }
}
