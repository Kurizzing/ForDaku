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
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            rotateButton = new System.Windows.Forms.Button();
            triangleDrawPanel = new System.Windows.Forms.Panel();
            prizePanel = new System.Windows.Forms.Panel();
            addListItem = new MyListItem();
            roulettePanel = new DoubleBufferedPanel();
            timerControl = new TimerControl();
            resultLabel = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Location = new System.Drawing.Point(981, 262);
            flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(505, 575);
            flowLayoutPanel.TabIndex = 1;
            // 
            // rotateButton
            // 
            rotateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            rotateButton.BackColor = System.Drawing.SystemColors.ControlLight;
            rotateButton.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 129);
            rotateButton.Location = new System.Drawing.Point(981, 922);
            rotateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            rotateButton.Name = "rotateButton";
            rotateButton.Size = new System.Drawing.Size(126, 86);
            rotateButton.TabIndex = 5;
            rotateButton.Text = "시작";
            rotateButton.UseVisualStyleBackColor = false;
            rotateButton.Click += RotateButton_Click;
            // 
            // triangleDrawPanel
            // 
            triangleDrawPanel.Location = new System.Drawing.Point(402, 72);
            triangleDrawPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            triangleDrawPanel.Name = "triangleDrawPanel";
            triangleDrawPanel.Size = new System.Drawing.Size(93, 66);
            triangleDrawPanel.TabIndex = 9;
            triangleDrawPanel.Paint += TriangleDrawPanel_Paint;
            // 
            // prizePanel
            // 
            prizePanel.Location = new System.Drawing.Point(12, 15);
            prizePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            prizePanel.Name = "prizePanel";
            prizePanel.Size = new System.Drawing.Size(948, 65);
            prizePanel.TabIndex = 10;
            prizePanel.Paint += PrizePanel_Paint;
            // 
            // addListItem
            // 
            addListItem.ItemColor = System.Drawing.SystemColors.Control;
            addListItem.LabelText = "label1";
            addListItem.Location = new System.Drawing.Point(981, 859);
            addListItem.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            addListItem.Name = "addListItem";
            addListItem.NumericUpDownValue = 0;
            addListItem.Size = new System.Drawing.Size(520, 71);
            addListItem.TabIndex = 4;
            addListItem.TextBoxValue = "";
            addListItem.Load += AddListItem_Load;
            // 
            // roulettePanel
            // 
            roulettePanel.Location = new System.Drawing.Point(10, 146);
            roulettePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            roulettePanel.Name = "roulettePanel";
            roulettePanel.Size = new System.Drawing.Size(950, 1154);
            roulettePanel.TabIndex = 0;
            roulettePanel.Paint += RoulettePanel_Paint;
            // 
            // timerControl
            // 
            timerControl.BackColor = System.Drawing.SystemColors.ControlLight;
            timerControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            timerControl.Location = new System.Drawing.Point(1019, 55);
            timerControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            timerControl.Name = "timerControl";
            timerControl.Size = new System.Drawing.Size(338, 160);
            timerControl.TabIndex = 11;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Location = new System.Drawing.Point(1583, 212);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new System.Drawing.Size(36, 15);
            resultLabel.TabIndex = 12;
            resultLabel.Text = "result";
            // 
            // RouletteForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1904, 1041);
            Controls.Add(resultLabel);
            Controls.Add(timerControl);
            Controls.Add(prizePanel);
            Controls.Add(triangleDrawPanel);
            Controls.Add(rotateButton);
            Controls.Add(addListItem);
            Controls.Add(flowLayoutPanel);
            Controls.Add(roulettePanel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "RouletteForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "룰렛";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DoubleBufferedPanel roulettePanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private MyListItem addListItem;
        private System.Windows.Forms.Button rotateButton;
        private System.Windows.Forms.Panel triangleDrawPanel;
        private System.Windows.Forms.Panel prizePanel;
        private TimerControl timerControl;
        private System.Windows.Forms.Label resultLabel;
    }
}

