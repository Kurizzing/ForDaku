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
            this.itemAddButton = new System.Windows.Forms.Button();
            this.rotateButton = new System.Windows.Forms.Button();
            this.prizeLabel = new System.Windows.Forms.Label();
            this.myListItem1 = new ForDaku.MyListItem();
            this.roulettePanel = new ForDaku.DoubleBufferedPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(830, 50);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(807, 460);
            this.flowLayoutPanel.TabIndex = 1;
            this.flowLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // itemAddButton
            // 
            this.itemAddButton.Location = new System.Drawing.Point(1373, 525);
            this.itemAddButton.Name = "itemAddButton";
            this.itemAddButton.Size = new System.Drawing.Size(157, 57);
            this.itemAddButton.TabIndex = 2;
            this.itemAddButton.Text = "+";
            this.itemAddButton.UseVisualStyleBackColor = true;
            this.itemAddButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // rotateButton
            // 
            this.rotateButton.Location = new System.Drawing.Point(848, 772);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(126, 69);
            this.rotateButton.TabIndex = 5;
            this.rotateButton.Text = "회전";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // prizeLabel
            // 
            this.prizeLabel.AutoSize = true;
            this.prizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prizeLabel.Font = new System.Drawing.Font("굴림", 20F);
            this.prizeLabel.Location = new System.Drawing.Point(489, 9);
            this.prizeLabel.Name = "prizeLabel";
            this.prizeLabel.Size = new System.Drawing.Size(326, 27);
            this.prizeLabel.TabIndex = 6;
            this.prizeLabel.Text = "label1111111111111111";
            this.prizeLabel.Click += new System.EventHandler(this.prizeLabel_Click);
            // 
            // myListItem1
            // 
            this.myListItem1.ItemColor = System.Drawing.SystemColors.Control;
            this.myListItem1.LabelText = "label1";
            this.myListItem1.Location = new System.Drawing.Point(830, 525);
            this.myListItem1.Name = "myListItem1";
            this.myListItem1.NumericUpDownValue = 0;
            this.myListItem1.Size = new System.Drawing.Size(520, 57);
            this.myListItem1.TabIndex = 4;
            this.myListItem1.TextBoxValue = "";
            // 
            // roulettePanel
            // 
            this.roulettePanel.Location = new System.Drawing.Point(10, 50);
            this.roulettePanel.Name = "roulettePanel";
            this.roulettePanel.Size = new System.Drawing.Size(800, 820);
            this.roulettePanel.TabIndex = 0;
            this.roulettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // RouletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.prizeLabel);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.myListItem1);
            this.Controls.Add(this.itemAddButton);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.roulettePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RouletteForm";
            this.Text = "룰렛";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedPanel roulettePanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button itemAddButton;
        private MyListItem myListItem1;
        private System.Windows.Forms.Button rotateButton;
        private System.Windows.Forms.Label prizeLabel;
    }
}

