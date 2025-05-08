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
        private CustomNumericUpDown numericMinute;
        private CustomNumericUpDown numericSecond;
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
            numericMinute = new CustomNumericUpDown();
            numericMinute.Location = new Point(35, 17);
            numericMinute.Maximum = 59;
            numericMinute.Minimum = 0;
            numericMinute.Font = new Font("굴림", 30F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
            numericMinute.Size = new Size(70, 53);
            this.Controls.Add(numericMinute);

            numericSecond = new CustomNumericUpDown();
            numericSecond.Location = new Point(133, 17);
            numericSecond.Maximum = 59;
            numericSecond.Minimum = 0;
            numericSecond.Size = new Size(70, 53);
            numericSecond.Font = new Font("굴림", 30F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
            this.Controls.Add(numericSecond);

            numericMinute.UpButtonClicked += (s, ev) => AddMinutes(1);
            numericMinute.DownButtonClicked += (s, ev) => AddMinutes(-1);

            numericSecond.UpButtonClicked += (s, ev) => AddSeconds(1);
            numericSecond.DownButtonClicked += (s, ev) => AddSeconds(-1);


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

                // clear
                btnStart.Click -= btnStart_Click;
                btnStart.Click -= btnRestart_Click;
                btnStart.Click -= btnStop_Click;

                btnStart.Click += btnStart_Click;
                btnStart.Text = "Start";
                MessageBox.Show("타이머 종료!");
            }
        }

        private void UpdateDisplay()
        {
            numericMinute.Value = remainingTime.Minutes;
            numericSecond.Value = remainingTime.Seconds;
            label3.Text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
        }

        private void StartTimer()
        {
            remainingTime = new TimeSpan(0, (int)numericMinute.Value, (int)numericSecond.Value);
            if (remainingTime.TotalSeconds > 0)
            {
                //numericMinute.Enabled = false;
                //numericSecond.Enabled = false;
                timer.Start();

                startButton.Text = "Stop";
                btnStart.Click -= btnStart_Click;
                btnStart.Click += btnStop_Click;
            }
        }

        private void RestartTimer()
        {
            timer.Start();

            startButton.Text = "Stop";
            btnStart.Click -= btnRestart_Click;
            btnStart.Click += btnStop_Click;
        }

        private void StopTimer()
        {
            timer.Stop();

            btnStart.Text = "ReStart";
            btnStart.Click -= btnStop_Click;
            btnStart.Click += btnRestart_Click;
        }

        private void ResetTimer()
        {
            timer.Stop();
            remainingTime = TimeSpan.Zero;
            numericMinute.Value = numericSecond.Value = 0;

            btnStart.Text = "Start";
            // clear
            btnStart.Click -= btnStart_Click;
            btnStart.Click -= btnRestart_Click;
            btnStart.Click -= btnStop_Click;

            btnStart.Click += btnStart_Click;

            UpdateDisplay();
            //numericMinute.Enabled = true;
            //numericSecond.Enabled = true;
        }

        private void AddMinutes(int minutes)
        {
            if (remainingTime.Minutes + minutes > 59)
            {
                remainingTime = new TimeSpan(remainingTime.Hours, 59, remainingTime.Seconds);
            }
            else if (remainingTime.Minutes + minutes < 0)
            {
                remainingTime = new TimeSpan(remainingTime.Hours, 0, remainingTime.Seconds);
            }
            else
            {
                remainingTime = remainingTime.Add(TimeSpan.FromMinutes(minutes));
            }
            UpdateDisplay();
        }

        private void AddSeconds(int seconds)
        {
            if (remainingTime.Seconds + seconds > 59)
            {
                remainingTime = new TimeSpan(remainingTime.Hours, remainingTime.Minutes, 59);
            }
            else if (remainingTime.Seconds + seconds < 0)
            {
                remainingTime = new TimeSpan(remainingTime.Hours, remainingTime.Minutes, 0);
            }
            else
            {
                remainingTime = remainingTime.Add(TimeSpan.FromSeconds(seconds));
            }
            UpdateDisplay();
        }

        // 버튼 이벤트
        private void btnStart_Click(object sender, EventArgs e) => StartTimer();
        private void btnReset_Click(object sender, EventArgs e) => ResetTimer();
        private void btnRestart_Click(object sender, EventArgs e) => RestartTimer();
        private void btnStop_Click(object sender, EventArgs e) => StopTimer();
        private void btnAdd10s_Click(object sender, EventArgs e) => AddSeconds(10);
        private void btnAdd1m_Click(object sender, EventArgs e) => AddMinutes(1);
        private void btnAdd5m_Click(object sender, EventArgs e) => AddMinutes(5);
        private void btnAdd10m_Click(object sender, EventArgs e) => AddMinutes(10);

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

        protected override void UpdateEditText()
        {
            this.Text = this.Value.ToString("00");
        }
    }
}

