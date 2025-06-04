using System;
using System.Windows.Forms;

namespace ForDaku
{
    partial class TimerControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            resetButton = new Button();
            startButton = new Button();
            tenSecButton = new Button();
            halfMinButton = new Button();
            oneMinButton = new Button();
            fiveMinButton = new Button();
            minLabel = new Label();
            secLabel = new Label();
            SuspendLayout();
            // 
            // resetButton
            // 
            resetButton.Location = new System.Drawing.Point(238, 53);
            resetButton.Margin = new Padding(3, 4, 3, 4);
            resetButton.Name = "resetButton";
            resetButton.Size = new System.Drawing.Size(80, 32);
            resetButton.TabIndex = 2;
            resetButton.Text = "RESET";
            resetButton.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            startButton.Location = new System.Drawing.Point(238, 20);
            startButton.Margin = new Padding(3, 4, 3, 4);
            startButton.Name = "startButton";
            startButton.Size = new System.Drawing.Size(80, 31);
            startButton.TabIndex = 3;
            startButton.Text = "START";
            startButton.UseVisualStyleBackColor = true;
            // 
            // tenSecButton
            // 
            tenSecButton.BackColor = System.Drawing.Color.Transparent;
            tenSecButton.FlatStyle = FlatStyle.Flat;
            tenSecButton.Location = new System.Drawing.Point(17, 105);
            tenSecButton.Margin = new Padding(3, 4, 3, 4);
            tenSecButton.Name = "tenSecButton";
            tenSecButton.Size = new System.Drawing.Size(60, 40);
            tenSecButton.TabIndex = 4;
            tenSecButton.Text = "+10초";
            tenSecButton.UseVisualStyleBackColor = false;
            // 
            // halfMinButton
            // 
            halfMinButton.BackColor = System.Drawing.Color.Transparent;
            halfMinButton.FlatStyle = FlatStyle.Flat;
            halfMinButton.Location = new System.Drawing.Point(92, 105);
            halfMinButton.Margin = new Padding(3, 4, 3, 4);
            halfMinButton.Name = "halfMinButton";
            halfMinButton.Size = new System.Drawing.Size(60, 40);
            halfMinButton.TabIndex = 5;
            halfMinButton.Text = "+30초";
            halfMinButton.UseVisualStyleBackColor = false;
            halfMinButton.Click += oneMinButton_Click;
            // 
            // oneMinButton
            // 
            oneMinButton.BackColor = System.Drawing.Color.Transparent;
            oneMinButton.FlatStyle = FlatStyle.Flat;
            oneMinButton.Location = new System.Drawing.Point(175, 105);
            oneMinButton.Margin = new Padding(3, 4, 3, 4);
            oneMinButton.Name = "oneMinButton";
            oneMinButton.Size = new System.Drawing.Size(60, 40);
            oneMinButton.TabIndex = 6;
            oneMinButton.Text = "+1분";
            oneMinButton.UseVisualStyleBackColor = false;
            // 
            // fiveMinButton
            // 
            fiveMinButton.BackColor = System.Drawing.Color.Transparent;
            fiveMinButton.FlatStyle = FlatStyle.Flat;
            fiveMinButton.Location = new System.Drawing.Point(258, 105);
            fiveMinButton.Margin = new Padding(3, 4, 3, 4);
            fiveMinButton.Name = "fiveMinButton";
            fiveMinButton.Size = new System.Drawing.Size(60, 40);
            fiveMinButton.TabIndex = 7;
            fiveMinButton.Text = "+5분";
            fiveMinButton.UseVisualStyleBackColor = false;
            // 
            // minLabel
            // 
            minLabel.AutoSize = true;
            minLabel.Location = new System.Drawing.Point(110, 70);
            minLabel.Margin = new Padding(0);
            minLabel.Name = "minLabel";
            minLabel.Size = new System.Drawing.Size(19, 15);
            minLabel.TabIndex = 8;
            minLabel.Text = "분";
            // 
            // secLabel
            // 
            secLabel.AutoSize = true;
            secLabel.Location = new System.Drawing.Point(209, 70);
            secLabel.Margin = new Padding(0);
            secLabel.Name = "secLabel";
            secLabel.Size = new System.Drawing.Size(19, 15);
            secLabel.TabIndex = 9;
            secLabel.Text = "초";
            // 
            // TimerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLight;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(secLabel);
            Controls.Add(minLabel);
            Controls.Add(fiveMinButton);
            Controls.Add(oneMinButton);
            Controls.Add(halfMinButton);
            Controls.Add(tenSecButton);
            Controls.Add(startButton);
            Controls.Add(resetButton);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TimerControl";
            Size = new System.Drawing.Size(338, 160);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button resetButton;
        private Button startButton;
        private Button tenSecButton;
        private Button halfMinButton;
        private Button oneMinButton;
        private Button fiveMinButton;
        private Label minLabel;
        private Label secLabel;
    }
}
