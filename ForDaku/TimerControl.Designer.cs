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
            oneMinButton = new Button();
            fiveMinButton = new Button();
            tenMinButton = new Button();
            label1 = new Label();
            label2 = new Label();
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
            resetButton.Click += resetButton_Click;
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
            startButton.Click += button2_Click;
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
            // oneMinButton
            // 
            oneMinButton.BackColor = System.Drawing.Color.Transparent;
            oneMinButton.FlatStyle = FlatStyle.Flat;
            oneMinButton.Location = new System.Drawing.Point(92, 105);
            oneMinButton.Margin = new Padding(3, 4, 3, 4);
            oneMinButton.Name = "oneMinButton";
            oneMinButton.Size = new System.Drawing.Size(60, 40);
            oneMinButton.TabIndex = 5;
            oneMinButton.Text = "+1분";
            oneMinButton.UseVisualStyleBackColor = false;
            // 
            // fiveMinButton
            // 
            fiveMinButton.BackColor = System.Drawing.Color.Transparent;
            fiveMinButton.FlatStyle = FlatStyle.Flat;
            fiveMinButton.Location = new System.Drawing.Point(175, 105);
            fiveMinButton.Margin = new Padding(3, 4, 3, 4);
            fiveMinButton.Name = "fiveMinButton";
            fiveMinButton.Size = new System.Drawing.Size(60, 40);
            fiveMinButton.TabIndex = 6;
            fiveMinButton.Text = "+5분";
            fiveMinButton.UseVisualStyleBackColor = false;
            // 
            // tenMinButton
            // 
            tenMinButton.BackColor = System.Drawing.Color.Transparent;
            tenMinButton.FlatStyle = FlatStyle.Flat;
            tenMinButton.Location = new System.Drawing.Point(258, 105);
            tenMinButton.Margin = new Padding(3, 4, 3, 4);
            tenMinButton.Name = "tenMinButton";
            tenMinButton.Size = new System.Drawing.Size(60, 40);
            tenMinButton.TabIndex = 7;
            tenMinButton.Text = "+10분";
            tenMinButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(110, 70);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(19, 15);
            label1.TabIndex = 8;
            label1.Text = "분";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(209, 70);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(19, 15);
            label2.TabIndex = 9;
            label2.Text = "초";
            // 
            // TimerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLight;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tenMinButton);
            Controls.Add(fiveMinButton);
            Controls.Add(oneMinButton);
            Controls.Add(tenSecButton);
            Controls.Add(startButton);
            Controls.Add(resetButton);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TimerControl";
            Size = new System.Drawing.Size(338, 160);
            Load += TimerControl_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private Button resetButton;
        private Button startButton;
        private Button tenSecButton;
        private Button oneMinButton;
        private Button fiveMinButton;
        private Button tenMinButton;
        private Label label1;
        private Label label2;
    }
}
