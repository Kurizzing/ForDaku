using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForDaku
{
    public partial class TimerControl : UserControl
    {
        private readonly Timer timer = new Timer();
        private TimeSpan remainingTime;

        private readonly Font numericUpDownFont = new Font("굴림", 30F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
        private readonly Size numericSize = new Size(70, 53);

        //SystemColors.ControlLight
        private readonly Color startButtonColor = Color.LightGray;
        private readonly Color restartButtonColor = Color.LightGreen;
        private readonly Color resetButtonColor = Color.LightGray;
        private readonly Color stopButtonColor = Color.LightPink;

        private const string startText = "START";
        private const string restartText = "RESTART";
        private const string resetText = "RESET";
        private const string stopText = "STOP";

        // UI 컨트롤 필드
        private CustomNumericUpDown numericMinute;
        private CustomNumericUpDown numericSecond;

        public TimerControl()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            int numericPosT = minLabel.Location.Y + minLabel.Size.Height - numericSize.Height;
            numericMinute = new CustomNumericUpDown();
            numericMinute.Location = new Point(35, numericPosT);
            numericMinute.Maximum = 59;
            numericMinute.Minimum = 0;
            numericMinute.Font = numericUpDownFont;
            numericMinute.Size = numericSize;
            this.Controls.Add(numericMinute);
            numericMinute.UpButtonClicked += (s, ev) => AddMinutes(1);
            numericMinute.DownButtonClicked += (s, ev) => AddMinutes(-1);

            numericSecond = new CustomNumericUpDown();
            numericSecond.Location = new Point(133, numericPosT);
            numericSecond.Maximum = 59;
            numericSecond.Minimum = 0;
            numericSecond.Size = numericSize;
            numericSecond.Font = numericUpDownFont;
            this.Controls.Add(numericSecond);
            numericSecond.UpButtonClicked += (s, ev) => AddSeconds(1);
            numericSecond.DownButtonClicked += (s, ev) => AddSeconds(-1);

            // 이벤트 핸들링
            startButton.Click += BtnStart_Click;
            resetButton.Click += BtnReset_Click;
            tenSecButton.Click += BtnAdd10s_Click;
            halfMinButton.Click += BtnAdd30s_Click;
            oneMinButton.Click += BtnAdd1m_Click;
            fiveMinButton.Click += BtnAdd5m_Click;

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            startButton.BackColor = startButtonColor;
            resetButton.BackColor = resetButtonColor;
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
                startButton.Click -= BtnStart_Click;
                startButton.Click -= BtnRestart_Click;
                startButton.Click -= BtnStop_Click;

                startButton.Click += BtnStart_Click;
                startButton.Text = startText;
                startButton.BackColor = startButtonColor;
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
                timer.Start();

                startButton.Text = stopText;
                startButton.BackColor = stopButtonColor;

                startButton.Click -= BtnStart_Click;
                startButton.Click += BtnStop_Click;
            }
        }

        private void RestartTimer()
        {
            timer.Start();

            startButton.Text = stopText;
            startButton.BackColor = stopButtonColor;

            startButton.Click -= BtnRestart_Click;
            startButton.Click += BtnStop_Click;
        }

        private void StopTimer()
        {
            timer.Stop();

            startButton.Text = restartText;
            startButton.BackColor = restartButtonColor;

            startButton.Click -= BtnStop_Click;
            startButton.Click += BtnRestart_Click;
        }

        private void ResetTimer()
        {
            timer.Stop();
            remainingTime = TimeSpan.Zero;
            numericMinute.Value = numericSecond.Value = 0;

            startButton.Text = startText;
            startButton.BackColor = startButtonColor;

            // clear
            startButton.Click -= BtnStart_Click;
            startButton.Click -= BtnRestart_Click;
            startButton.Click -= BtnStop_Click;

            startButton.Click += BtnStart_Click;

            UpdateDisplay();
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
        private void BtnStart_Click(object sender, EventArgs e) => StartTimer();
        private void BtnReset_Click(object sender, EventArgs e) => ResetTimer();
        private void BtnRestart_Click(object sender, EventArgs e) => RestartTimer();
        private void BtnStop_Click(object sender, EventArgs e) => StopTimer();
        private void BtnAdd10s_Click(object sender, EventArgs e) => AddSeconds(10);
        private void BtnAdd30s_Click(object sender, EventArgs e) => AddSeconds(30);
        private void BtnAdd1m_Click(object sender, EventArgs e) => AddMinutes(1);
        private void BtnAdd5m_Click(object sender, EventArgs e) => AddMinutes(5);

        private void oneMinButton_Click(object sender, EventArgs e)
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

