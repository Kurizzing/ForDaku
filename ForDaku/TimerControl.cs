using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForDaku
{
    public partial class TimerControl : UserControl
    {
        private Timer timer;
        private TimeSpan remainingTime;

        // UI 컨트롤 필드
        private NumericUpDown numericMinute;
        private NumericUpDown numericSecond;
        private Button btnStart;
        private Button btnReset;
        private Button btnAdd10s;
        private Button btnAdd1m;
        private Button btnAdd5m;
        private Button btnAdd10m;

        public TimerControl()
        {
            InitializeComponent();       // 디자이너 생성 코드
            InitializeCustomUI();        // 사용자 정의 UI 구성

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private void InitializeCustomUI()
        {
            numericMinute = minNumericUpDown;
            numericSecond = secNumericUpDown;

            btnStart = startButton;
            btnReset = resetButton;

            btnAdd10s = tenSecButton;
            btnAdd1m = oneMinButton;
            btnAdd5m = fiveMinButton;
            btnAdd10m = tenMinButton;

            // 이벤트 핸들링
            btnStart.Click += btnStart_Click;
            btnReset.Click += btnReset_Click;
            btnAdd10s.Click += btnAdd10s_Click;
            btnAdd1m.Click += btnAdd1m_Click;
            btnAdd5m.Click += btnAdd5m_Click;
            btnAdd10m.Click += btnAdd10m_Click;
        }

        // 타이머 이벤트 핸들링
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateDisplay();
            }
            else
            {
                timer.Stop();
                MessageBox.Show("타이머 종료!");
            }
        }

        private void UpdateDisplay()
        {
            numericMinute.Value = remainingTime.Minutes;
            numericSecond.Value = remainingTime.Seconds;

        }

        private void StartTimer()
        {
            remainingTime = new TimeSpan(0, (int)numericMinute.Value, (int)numericSecond.Value);
            if (remainingTime.TotalSeconds > 0)
            {
                numericMinute.Enabled = false;
                numericSecond.Enabled = false;
                timer.Start();
            }
        }

        private void ResetTimer()
        {
            timer.Stop();
            remainingTime = TimeSpan.Zero;
            numericMinute.Value = numericSecond.Value = 0;
            numericMinute.Enabled = true;
            numericSecond.Enabled = true;
        }

        private void AddTime(int seconds)
        {
            remainingTime = remainingTime.Add(TimeSpan.FromSeconds(seconds));
            UpdateDisplay();
        }

        // 버튼 이벤트
        private void btnStart_Click(object sender, EventArgs e) => StartTimer();
        private void btnReset_Click(object sender, EventArgs e) => ResetTimer();
        private void btnAdd10s_Click(object sender, EventArgs e) => AddTime(10);
        private void btnAdd1m_Click(object sender, EventArgs e) => AddTime(60);
        private void btnAdd5m_Click(object sender, EventArgs e) => AddTime(300);
        private void btnAdd10m_Click(object sender, EventArgs e) => AddTime(600);

        private void TimerControl_Load(object sender, EventArgs e)
        {
            // 필요 시 초기화 코드 작성
        }

        /// <summary>
        /// 분
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //if (numericMinute.Value > remainingTime.Minutes)
            //    AddTime(60);
            //else
            //    AddTime(-60);
        }

        /// <summary>
        /// 초
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //if (numericMinute.Value > remainingTime.Seconds)
            //    AddTime(1);
            //else
            //    AddTime(-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public class CustomNumericUpDown : NumericUpDown
    {
        public event EventHandler UpButtonClicked;
        public event EventHandler DownButtonClicked;

        public override void UpButton()
        {
            base.UpButton(); // 원래 동작 유지
            UpButtonClicked?.Invoke(this, EventArgs.Empty); // 커스텀 이벤트 발생
        }

        public override void DownButton()
        {
            base.DownButton(); // 원래 동작 유지
            DownButtonClicked?.Invoke(this, EventArgs.Empty); // 커스텀 이벤트 발생
        }
    }
}

