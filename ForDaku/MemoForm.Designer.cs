namespace ForDaku
{
    partial class MemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoForm));
            this.makeRouletteButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optionComboBox = new System.Windows.Forms.ComboBox();
            this.memoTitleLabel = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.urlButtonYT = new System.Windows.Forms.Button();
            this.urlButtonCF = new System.Windows.Forms.Button();
            this.urlButtonDC = new System.Windows.Forms.Button();
            this.urlButtonMM = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.sortButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // makeRouletteButton
            // 
            this.makeRouletteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.makeRouletteButton.Location = new System.Drawing.Point(909, 979);
            this.makeRouletteButton.Name = "makeRouletteButton";
            this.makeRouletteButton.Size = new System.Drawing.Size(90, 50);
            this.makeRouletteButton.TabIndex = 0;
            this.makeRouletteButton.Text = "룰렛 생성";
            this.makeRouletteButton.UseVisualStyleBackColor = true;
            this.makeRouletteButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Font = new System.Drawing.Font("굴림", 30F);
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(900, 1041);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optionComboBox);
            this.panel1.Controls.Add(this.sortButton);
            this.panel1.Location = new System.Drawing.Point(910, 487);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 105);
            this.panel1.TabIndex = 4;
            // 
            // optionComboBox
            // 
            this.optionComboBox.Font = new System.Drawing.Font("굴림", 10F);
            this.optionComboBox.FormattingEnabled = true;
            this.optionComboBox.Location = new System.Drawing.Point(3, 79);
            this.optionComboBox.Name = "optionComboBox";
            this.optionComboBox.Size = new System.Drawing.Size(90, 21);
            this.optionComboBox.TabIndex = 6;
            this.optionComboBox.SelectedIndexChanged += new System.EventHandler(this.optionComboBox_SelectedIndexChanged);
            // 
            // memoTitleLabel
            // 
            this.memoTitleLabel.AutoSize = true;
            this.memoTitleLabel.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.memoTitleLabel.Location = new System.Drawing.Point(910, 10);
            this.memoTitleLabel.Name = "memoTitleLabel";
            this.memoTitleLabel.Size = new System.Drawing.Size(92, 27);
            this.memoTitleLabel.TabIndex = 6;
            this.memoTitleLabel.Text = "Memo";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("굴림", 30F);
            this.richTextBox2.Location = new System.Drawing.Point(910, 40);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(900, 350);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.saveButton);
            this.panel2.Controls.Add(this.loadButton);
            this.panel2.Location = new System.Drawing.Point(910, 407);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(93, 47);
            this.panel2.TabIndex = 10;
            // 
            // urlButtonYT
            // 
            this.urlButtonYT.BackColor = System.Drawing.Color.Transparent;
            this.urlButtonYT.Image = global::ForDaku.Properties.Resources.Youtube_logo;
            this.urlButtonYT.Location = new System.Drawing.Point(145, 3);
            this.urlButtonYT.Name = "urlButtonYT";
            this.urlButtonYT.Size = new System.Drawing.Size(70, 40);
            this.urlButtonYT.TabIndex = 14;
            this.urlButtonYT.UseVisualStyleBackColor = false;
            this.urlButtonYT.Click += new System.EventHandler(this.urlButtonYT_Click);
            // 
            // urlButtonCF
            // 
            this.urlButtonCF.BackColor = System.Drawing.Color.Transparent;
            this.urlButtonCF.Image = global::ForDaku.Properties.Resources.images;
            this.urlButtonCF.Location = new System.Drawing.Point(221, 4);
            this.urlButtonCF.Name = "urlButtonCF";
            this.urlButtonCF.Size = new System.Drawing.Size(40, 40);
            this.urlButtonCF.TabIndex = 13;
            this.urlButtonCF.UseVisualStyleBackColor = false;
            this.urlButtonCF.Click += new System.EventHandler(this.urlButtonCF_Click);
            // 
            // urlButtonDC
            // 
            this.urlButtonDC.BackColor = System.Drawing.Color.Transparent;
            this.urlButtonDC.Image = global::ForDaku.Properties.Resources.dc;
            this.urlButtonDC.Location = new System.Drawing.Point(99, 3);
            this.urlButtonDC.Name = "urlButtonDC";
            this.urlButtonDC.Size = new System.Drawing.Size(40, 40);
            this.urlButtonDC.TabIndex = 12;
            this.urlButtonDC.UseVisualStyleBackColor = false;
            this.urlButtonDC.Click += new System.EventHandler(this.urlButtonDC_Click);
            // 
            // urlButtonMM
            // 
            this.urlButtonMM.BackColor = System.Drawing.Color.Transparent;
            this.urlButtonMM.Image = global::ForDaku.Properties.Resources.마듀메타;
            this.urlButtonMM.Location = new System.Drawing.Point(3, 3);
            this.urlButtonMM.Name = "urlButtonMM";
            this.urlButtonMM.Size = new System.Drawing.Size(90, 40);
            this.urlButtonMM.TabIndex = 11;
            this.urlButtonMM.UseVisualStyleBackColor = false;
            this.urlButtonMM.Click += new System.EventHandler(this.urlButtonMM_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.Transparent;
            this.saveButton.Image = global::ForDaku.Properties.Resources.floppy_disk_solid;
            this.saveButton.Location = new System.Drawing.Point(3, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(40, 40);
            this.saveButton.TabIndex = 8;
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.Color.Transparent;
            this.loadButton.Image = global::ForDaku.Properties.Resources.file_import_solid;
            this.loadButton.Location = new System.Drawing.Point(49, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(40, 40);
            this.loadButton.TabIndex = 9;
            this.loadButton.UseVisualStyleBackColor = false;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.BackColor = System.Drawing.Color.Transparent;
            this.sortButton.Image = global::ForDaku.Properties.Resources.arrow_down_wide_short_solid;
            this.sortButton.Location = new System.Drawing.Point(3, 3);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(90, 70);
            this.sortButton.TabIndex = 4;
            this.sortButton.UseVisualStyleBackColor = false;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.urlButtonMM);
            this.panel3.Controls.Add(this.urlButtonYT);
            this.panel3.Controls.Add(this.urlButtonDC);
            this.panel3.Controls.Add(this.urlButtonCF);
            this.panel3.Location = new System.Drawing.Point(1049, 407);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 47);
            this.panel3.TabIndex = 15;
            // 
            // MemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.memoTitleLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.makeRouletteButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "For Daku";
            this.Load += new System.EventHandler(this.MemoForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button makeRouletteButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox optionComboBox;
        private System.Windows.Forms.Label memoTitleLabel;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button urlButtonMM;
        private System.Windows.Forms.Button urlButtonDC;
        private System.Windows.Forms.Button urlButtonCF;
        private System.Windows.Forms.Button urlButtonYT;
        private System.Windows.Forms.Panel panel3;
    }
}