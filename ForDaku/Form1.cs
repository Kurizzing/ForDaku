using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ForDaku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawRoulette(e);
        }

        void DrawRoulette(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, panel1.Width, panel1.Height); // 파이 그릴 영역

            List<MyListItem> itemList = new List<MyListItem>();

            // flowlayout에서 리스트 추출
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is MyListItem item)
                {
                    itemList.Add(item);
                }
            }

            Brush brush = Brushes.Blue; // 색상 선택
            float startAngle = 0;       // 시작 각도 (0도)
            float sweepAngle = 0;     // 파이 조각 각도 (120도)

            foreach (MyListItem item in itemList)
            {
                // 색상 가져오기
                brush = new SolidBrush(item.ItemColor);
                float probability = item.NumericUpDownValue / (float)GetAllCount();
                sweepAngle = ProbabilityToDegree(probability);

                g.FillPie(brush, rect, startAngle, sweepAngle);

                // 중심과 각도 계산
                float midAngle = startAngle + sweepAngle / 2;
                PointF center = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
                float radius = rect.Width / 2 * 0.7f;
                double radians = midAngle * Math.PI / 180;

                PointF textPos = new PointF(
                    center.X + (float)(radius * Math.Cos(radians)),
                    center.Y + (float)(radius * Math.Sin(radians))
                );

                using (Font font = new Font("굴림", 16))
                {
                    // 현재 상태 저장
                    var state = g.Save();

                    // 텍스트 회전 처리
                    g.TranslateTransform(textPos.X, textPos.Y);
                    g.RotateTransform(midAngle); // midAngle만큼 회전

                    // 텍스트 그리기 (로컬 좌표 기준이므로 약간 위치 조정)
                    g.DrawString(item.TextBoxValue, font, Brushes.Black, new PointF(-20, -10));

                    // 원래대로 복구
                    g.Restore(state);
                }

                startAngle += sweepAngle;
            }


        }

        void UpdateRoulette()
        {
            panel1.Invalidate();
            panel1.Update();
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
            foreach (MyListItem item in flowLayoutPanel1.Controls)
            {
                count += item.NumericUpDownValue;
            }
            return count;
        }

        /// <summary>
        /// 버튼 클릭 시 새로운 MyListItem을 생성하여 FlowLayoutPanel에 추가합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var item = new MyListItem();
            item.ItemColor = GenerateDistinctColor();
            item.TextBoxValue = myListItem1.TextBoxValue;
            item.NumericUpDownValue = myListItem1.NumericUpDownValue;
            item.NumericUpDownControl.ValueChanged += (s, ev) =>
            {
                UpdateProbability();
                UpdateRoulette();
            };
            item.ButtonControl.Click += (s, ev) =>
            {
                flowLayoutPanel1.Controls.Remove(item);
                UpdateProbability();
                UpdateRoulette();
            };
            flowLayoutPanel1.Controls.Add(item);
            UpdateProbability();
            UpdateRoulette();
        }

        public void UpdateProbability()
        {
            int allCount = GetAllCount();
            foreach (MyListItem item in flowLayoutPanel1.Controls)
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

    }
}
