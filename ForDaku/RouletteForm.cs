using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 중심을 panel1의 위로 이동
            g.TranslateTransform(roulettePanel.ClientSize.Width / 2, 0);

            int triangleHeight = 20; // 삼각형 높이
            int triangleWidth = 20; // 삼각형 너비
            // 삼각형 좌표 (아래쪽을 향하는 삼각형)
            Point[] triangle =
            {
                new Point(0, triangleHeight),     // 꼭짓점 아래쪽
                new Point((int)(-triangleWidth/2.0f), 0),   // 왼쪽 위
                new Point((int)(+triangleWidth/2.0f), 0)     // 오른쪽 위
            };
            g.FillPolygon(Brushes.Red, triangle);
            
            g.ResetTransform(); // 변환 초기화
            g.TranslateTransform(0, 20);
            // todo: 패널 높이 조절 자동화 필요

            DrawRoulette(g, roulettePanel.Width, roulettePanel.Height - triangleHeight);  // 이 결과가 가로세로 비율 1:1이 되어야 원이 됨
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

                    rotateButton.Text = "회전";
                    rotateButton.Enabled = true;
                }
            }

            UpdateRoulette();
        }

        void DrawRoulette(Graphics g, int width, int height)
        {
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

            foreach (MyListItem item in itemList)
            {
                brush = new SolidBrush(item.ItemColor);
                float probability = item.NumericUpDownValue / (float)GetAllCount();
                sweepAngle = ProbabilityToDegree(probability);

                g.FillPie(brush, rect, startAngle, sweepAngle);

                // 중간 각도
                float midAngle = startAngle + sweepAngle / 2;
                float radius = rect.Width / 2 * 0.7f;
                double radians = midAngle * Math.PI / 180;

                PointF textPos = new PointF(
                    center.X + (float)(radius * Math.Cos(radians)),
                    center.Y + (float)(radius * Math.Sin(radians))
                );

                using (Font font = new Font("굴림", 16))
                {
                    var state = g.Save();
                    g.TranslateTransform(textPos.X, textPos.Y);
                    g.RotateTransform(midAngle);
                    g.DrawString(item.TextBoxValue, font, Brushes.Black, new PointF(-20, -10));
                    g.Restore(state);
                }

                // 현재 당첨 아이템
                if (startAngle <= PointDegree(rotationAngle) && PointDegree(rotationAngle) <= startAngle + sweepAngle)
                {
                    prizeLabel.Text = item.TextBoxValue;
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
            item.NumericUpDownValue = numericUpDownValue;
            item.NumericUpDownControl.ValueChanged += (s, ev) =>
            {
                UpdateProbability();
                UpdateRoulette();
            };
            item.ButtonControl.Click += (s, ev) =>
            {
                flowLayoutPanel.Controls.Remove(item);
                UpdateProbability();
                UpdateRoulette();
            };
            flowLayoutPanel.Controls.Add(item);
            UpdateProbability();
            UpdateRoulette();
        }

        /// <summary>
        /// 버튼 클릭 시 새로운 MyListItem을 생성하여 FlowLayoutPanel에 추가합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            addItemToRoulette(myListItem1.TextBoxValue, myListItem1.NumericUpDownValue);

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

        Color GenerateDistinctColor()
        {
            int maxAttempts = 100;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                double newHue = rnd.NextDouble() * 360;

                // 최소 거리 기준: 다른 색상과 최소 30도 이상 떨어질 것
                if (usedHues.All(h => Math.Abs(h - newHue) >= 30 || Math.Abs(h - newHue) >= 330))
                {
                    usedHues.Add(newHue);
                    return ColorFromHSV(newHue, 0.6, 0.95);
                }
            }

            // fallback (비슷한 색이라도 무조건 하나 리턴)
            double fallbackHue = rnd.NextDouble() * 360;
            return ColorFromHSV(fallbackHue, 0.6, 0.95);
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
            if (rotateButton.Text == "회전")
            {
                StartSpin();                   // 회전 시작
                rotateButton.Text = "정지";         // 텍스트 변경
            }
            else if (rotateButton.Text == "정지")
            {
                StartDeceleration();           // 감속 시작
                rotateButton.Enabled = false;       // 버튼 비활성화
            }
        }

        private void ToMemoFormButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            memoForm.Show();
        }

        private void prizeLabel_Click(object sender, EventArgs e)
        {

        }

        private void timerControl1_Load(object sender, EventArgs e)
        {

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
