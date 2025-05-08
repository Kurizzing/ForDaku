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
            this.minNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.secNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.resetButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.tenSecButton = new System.Windows.Forms.Button();
            this.oneMinButton = new System.Windows.Forms.Button();
            this.fiveMinButton = new System.Windows.Forms.Button();
            this.tenMinButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // minNumericUpDown
            // 
            this.minNumericUpDown.Font = new System.Drawing.Font("굴림", 30F);
            this.minNumericUpDown.Location = new System.Drawing.Point(35, 17);
            this.minNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minNumericUpDown.Name = "minNumericUpDown";
            this.minNumericUpDown.Size = new System.Drawing.Size(70, 53);
            this.minNumericUpDown.TabIndex = 0;
            this.minNumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // secNumericUpDown
            // 
            this.secNumericUpDown.Font = new System.Drawing.Font("굴림", 30F);
            this.secNumericUpDown.Location = new System.Drawing.Point(133, 17);
            this.secNumericUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.secNumericUpDown.Name = "secNumericUpDown";
            this.secNumericUpDown.Size = new System.Drawing.Size(70, 53);
            this.secNumericUpDown.TabIndex = 1;
            this.secNumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(238, 44);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(80, 26);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "RESET";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(238, 17);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(80, 25);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "START";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // tenSecButton
            // 
            this.tenSecButton.BackColor = System.Drawing.Color.Transparent;
            this.tenSecButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tenSecButton.Location = new System.Drawing.Point(17, 103);
            this.tenSecButton.Name = "tenSecButton";
            this.tenSecButton.Size = new System.Drawing.Size(60, 32);
            this.tenSecButton.TabIndex = 4;
            this.tenSecButton.Text = "+10초";
            this.tenSecButton.UseVisualStyleBackColor = false;
            // 
            // oneMinButton
            // 
            this.oneMinButton.BackColor = System.Drawing.Color.Transparent;
            this.oneMinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oneMinButton.Location = new System.Drawing.Point(92, 103);
            this.oneMinButton.Name = "oneMinButton";
            this.oneMinButton.Size = new System.Drawing.Size(60, 32);
            this.oneMinButton.TabIndex = 5;
            this.oneMinButton.Text = "+1분";
            this.oneMinButton.UseVisualStyleBackColor = false;
            // 
            // fiveMinButton
            // 
            this.fiveMinButton.BackColor = System.Drawing.Color.Transparent;
            this.fiveMinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fiveMinButton.Location = new System.Drawing.Point(175, 103);
            this.fiveMinButton.Name = "fiveMinButton";
            this.fiveMinButton.Size = new System.Drawing.Size(60, 32);
            this.fiveMinButton.TabIndex = 6;
            this.fiveMinButton.Text = "+5분";
            this.fiveMinButton.UseVisualStyleBackColor = false;
            // 
            // tenMinButton
            // 
            this.tenMinButton.BackColor = System.Drawing.Color.Transparent;
            this.tenMinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tenMinButton.Location = new System.Drawing.Point(258, 103);
            this.tenMinButton.Name = "tenMinButton";
            this.tenMinButton.Size = new System.Drawing.Size(60, 32);
            this.tenMinButton.TabIndex = 7;
            this.tenMinButton.Text = "+10분";
            this.tenMinButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "분";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "초";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // TimerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tenMinButton);
            this.Controls.Add(this.fiveMinButton);
            this.Controls.Add(this.oneMinButton);
            this.Controls.Add(this.tenSecButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.secNumericUpDown);
            this.Controls.Add(this.minNumericUpDown);
            this.Name = "TimerControl";
            this.Size = new System.Drawing.Size(340, 140);
            this.Load += new System.EventHandler(this.TimerControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown minNumericUpDown;
        private NumericUpDown secNumericUpDown;
        private Button resetButton;
        private Button startButton;
        private Button tenSecButton;
        private Button oneMinButton;
        private Button fiveMinButton;
        private Button tenMinButton;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
