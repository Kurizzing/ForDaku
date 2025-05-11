namespace ForDaku
{
    partial class RouletteForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RouletteForm));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.rotateButton = new System.Windows.Forms.Button();
            this.triangleDrawPanel = new System.Windows.Forms.Panel();
            this.prizePanel = new System.Windows.Forms.Panel();
            this.addListItem = new ForDaku.MyListItem();
            this.timerControl1 = new ForDaku.TimerControl();
            this.roulettePanel = new ForDaku.DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(981, 210);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(505, 460);
            this.flowLayoutPanel.TabIndex = 1;
            this.flowLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1715, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // rotateButton
            // 
            this.rotateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rotateButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rotateButton.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rotateButton.Location = new System.Drawing.Point(981, 946);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(126, 69);
            this.rotateButton.TabIndex = 5;
            this.rotateButton.Text = "시작";
            this.rotateButton.UseVisualStyleBackColor = false;
            this.rotateButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // triangleDrawPanel
            // 
            this.triangleDrawPanel.Location = new System.Drawing.Point(402, 58);
            this.triangleDrawPanel.Name = "triangleDrawPanel";
            this.triangleDrawPanel.Size = new System.Drawing.Size(93, 53);
            this.triangleDrawPanel.TabIndex = 9;
            this.triangleDrawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.triangleDrawPanel_Paint);
            // 
            // prizePanel
            // 
            this.prizePanel.Location = new System.Drawing.Point(12, 12);
            this.prizePanel.Name = "prizePanel";
            this.prizePanel.Size = new System.Drawing.Size(948, 52);
            this.prizePanel.TabIndex = 10;
            this.prizePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.prizePanel_Paint);
            // 
            // addListItem
            // 
            this.addListItem.ItemColor = System.Drawing.SystemColors.Control;
            this.addListItem.LabelText = "label1";
            this.addListItem.Location = new System.Drawing.Point(981, 687);
            this.addListItem.Name = "addListItem";
            this.addListItem.NumericUpDownValue = 0;
            this.addListItem.Size = new System.Drawing.Size(520, 57);
            this.addListItem.TabIndex = 4;
            this.addListItem.TextBoxValue = "";
            this.addListItem.Load += new System.EventHandler(this.myListItem1_Load_1);
            // 
            // timerControl1
            // 
            this.timerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timerControl1.Location = new System.Drawing.Point(981, 50);
            this.timerControl1.Name = "timerControl1";
            this.timerControl1.Size = new System.Drawing.Size(340, 140);
            this.timerControl1.TabIndex = 7;
            // 
            // roulettePanel
            // 
            this.roulettePanel.Location = new System.Drawing.Point(10, 117);
            this.roulettePanel.Name = "roulettePanel";
            this.roulettePanel.Size = new System.Drawing.Size(950, 923);
            this.roulettePanel.TabIndex = 0;
            this.roulettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // RouletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.prizePanel);
            this.Controls.Add(this.triangleDrawPanel);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.addListItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timerControl1);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.roulettePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RouletteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "xxx";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedPanel roulettePanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private MyListItem addListItem;
        private System.Windows.Forms.Button rotateButton;
        private TimerControl timerControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel triangleDrawPanel;
        private System.Windows.Forms.Panel prizePanel;
    }
}

