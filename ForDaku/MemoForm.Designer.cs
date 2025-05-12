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
            makeRouletteButton = new System.Windows.Forms.Button();
            deckTextBox = new System.Windows.Forms.RichTextBox();
            sortPanel = new System.Windows.Forms.Panel();
            optionComboBox = new System.Windows.Forms.ComboBox();
            sortButton = new System.Windows.Forms.Button();
            memoTitleLabel = new System.Windows.Forms.Label();
            memoTextBox = new System.Windows.Forms.RichTextBox();
            saveLoadPanel = new System.Windows.Forms.Panel();
            saveButton = new System.Windows.Forms.Button();
            loadButton = new System.Windows.Forms.Button();
            urlButtonYT = new System.Windows.Forms.Button();
            urlButtonCF = new System.Windows.Forms.Button();
            urlButtonDC = new System.Windows.Forms.Button();
            urlButtonMM = new System.Windows.Forms.Button();
            urlPanel = new System.Windows.Forms.Panel();
            madeByLabel = new System.Windows.Forms.Label();
            sortPanel.SuspendLayout();
            saveLoadPanel.SuspendLayout();
            urlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // makeRouletteButton
            // 
            makeRouletteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            makeRouletteButton.Location = new System.Drawing.Point(909, 1224);
            makeRouletteButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            makeRouletteButton.Name = "makeRouletteButton";
            makeRouletteButton.Size = new System.Drawing.Size(90, 62);
            makeRouletteButton.TabIndex = 0;
            makeRouletteButton.Text = "룰렛 생성";
            makeRouletteButton.UseVisualStyleBackColor = true;
            makeRouletteButton.Click += makeRouletteButton_Click;
            // 
            // deckTextBox
            // 
            deckTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            deckTextBox.Font = new System.Drawing.Font("굴림", 30F);
            deckTextBox.Location = new System.Drawing.Point(0, 0);
            deckTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            deckTextBox.Name = "deckTextBox";
            deckTextBox.Size = new System.Drawing.Size(900, 1300);
            deckTextBox.TabIndex = 1;
            deckTextBox.Text = "";
            // 
            // sortPanel
            // 
            sortPanel.Controls.Add(optionComboBox);
            sortPanel.Controls.Add(sortButton);
            sortPanel.Location = new System.Drawing.Point(910, 609);
            sortPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            sortPanel.Name = "sortPanel";
            sortPanel.Size = new System.Drawing.Size(98, 131);
            sortPanel.TabIndex = 4;
            // 
            // optionComboBox
            // 
            optionComboBox.Font = new System.Drawing.Font("굴림", 10F);
            optionComboBox.FormattingEnabled = true;
            optionComboBox.Location = new System.Drawing.Point(3, 99);
            optionComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            optionComboBox.Name = "optionComboBox";
            optionComboBox.Size = new System.Drawing.Size(90, 21);
            optionComboBox.TabIndex = 6;
            // 
            // sortButton
            // 
            sortButton.BackColor = System.Drawing.Color.Transparent;
            sortButton.Image = Properties.Resources.arrow_down_wide_short_solid;
            sortButton.Location = new System.Drawing.Point(3, 4);
            sortButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            sortButton.Name = "sortButton";
            sortButton.Size = new System.Drawing.Size(90, 88);
            sortButton.TabIndex = 4;
            sortButton.UseVisualStyleBackColor = false;
            sortButton.Click += sortButton_Click;
            // 
            // memoTitleLabel
            // 
            memoTitleLabel.AutoSize = true;
            memoTitleLabel.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 129);
            memoTitleLabel.Location = new System.Drawing.Point(910, 12);
            memoTitleLabel.Name = "memoTitleLabel";
            memoTitleLabel.Size = new System.Drawing.Size(92, 27);
            memoTitleLabel.TabIndex = 6;
            memoTitleLabel.Text = "Memo";
            // 
            // memoTextBox
            // 
            memoTextBox.Font = new System.Drawing.Font("굴림", 30F);
            memoTextBox.Location = new System.Drawing.Point(910, 50);
            memoTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            memoTextBox.Name = "memoTextBox";
            memoTextBox.Size = new System.Drawing.Size(900, 436);
            memoTextBox.TabIndex = 7;
            memoTextBox.Text = "";
            // 
            // saveLoadPanel
            // 
            saveLoadPanel.Controls.Add(saveButton);
            saveLoadPanel.Controls.Add(loadButton);
            saveLoadPanel.Location = new System.Drawing.Point(910, 509);
            saveLoadPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            saveLoadPanel.Name = "saveLoadPanel";
            saveLoadPanel.Size = new System.Drawing.Size(93, 59);
            saveLoadPanel.TabIndex = 10;
            // 
            // saveButton
            // 
            saveButton.BackColor = System.Drawing.Color.Transparent;
            saveButton.Image = Properties.Resources.floppy_disk_solid;
            saveButton.Location = new System.Drawing.Point(3, 4);
            saveButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(40, 50);
            saveButton.TabIndex = 8;
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += SaveButton_Click;
            // 
            // loadButton
            // 
            loadButton.BackColor = System.Drawing.Color.Transparent;
            loadButton.Image = Properties.Resources.file_import_solid;
            loadButton.Location = new System.Drawing.Point(49, 4);
            loadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            loadButton.Name = "loadButton";
            loadButton.Size = new System.Drawing.Size(40, 50);
            loadButton.TabIndex = 9;
            loadButton.UseVisualStyleBackColor = false;
            loadButton.Click += LoadButton_Click;
            // 
            // urlButtonYT
            // 
            urlButtonYT.BackColor = System.Drawing.Color.Transparent;
            urlButtonYT.Image = Properties.Resources.Youtube_logo;
            urlButtonYT.Location = new System.Drawing.Point(145, 4);
            urlButtonYT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            urlButtonYT.Name = "urlButtonYT";
            urlButtonYT.Size = new System.Drawing.Size(70, 50);
            urlButtonYT.TabIndex = 14;
            urlButtonYT.UseVisualStyleBackColor = false;
            urlButtonYT.Click += UrlButtonYT_Click;
            // 
            // urlButtonCF
            // 
            urlButtonCF.BackColor = System.Drawing.Color.Transparent;
            urlButtonCF.Image = Properties.Resources.images;
            urlButtonCF.Location = new System.Drawing.Point(221, 5);
            urlButtonCF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            urlButtonCF.Name = "urlButtonCF";
            urlButtonCF.Size = new System.Drawing.Size(40, 50);
            urlButtonCF.TabIndex = 13;
            urlButtonCF.UseVisualStyleBackColor = false;
            urlButtonCF.Click += UrlButtonCF_Click;
            // 
            // urlButtonDC
            // 
            urlButtonDC.BackColor = System.Drawing.Color.Transparent;
            urlButtonDC.Image = Properties.Resources.dc;
            urlButtonDC.Location = new System.Drawing.Point(99, 4);
            urlButtonDC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            urlButtonDC.Name = "urlButtonDC";
            urlButtonDC.Size = new System.Drawing.Size(40, 50);
            urlButtonDC.TabIndex = 12;
            urlButtonDC.UseVisualStyleBackColor = false;
            urlButtonDC.Click += UrlButtonDC_Click;
            // 
            // urlButtonMM
            // 
            urlButtonMM.BackColor = System.Drawing.Color.Transparent;
            urlButtonMM.Image = Properties.Resources.마듀메타;
            urlButtonMM.Location = new System.Drawing.Point(3, 4);
            urlButtonMM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            urlButtonMM.Name = "urlButtonMM";
            urlButtonMM.Size = new System.Drawing.Size(90, 50);
            urlButtonMM.TabIndex = 11;
            urlButtonMM.UseVisualStyleBackColor = false;
            urlButtonMM.Click += UrlButtonMM_Click;
            // 
            // urlPanel
            // 
            urlPanel.Controls.Add(urlButtonMM);
            urlPanel.Controls.Add(urlButtonYT);
            urlPanel.Controls.Add(urlButtonDC);
            urlPanel.Controls.Add(urlButtonCF);
            urlPanel.Location = new System.Drawing.Point(1049, 509);
            urlPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            urlPanel.Name = "urlPanel";
            urlPanel.Size = new System.Drawing.Size(266, 59);
            urlPanel.TabIndex = 15;
            // 
            // madeByLabel
            // 
            madeByLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            madeByLabel.AutoSize = true;
            madeByLabel.ForeColor = System.Drawing.Color.Silver;
            madeByLabel.Location = new System.Drawing.Point(1813, 1277);
            madeByLabel.Name = "madeByLabel";
            madeByLabel.Size = new System.Drawing.Size(79, 15);
            madeByLabel.TabIndex = 16;
            madeByLabel.Text = "made by Kuri";
            madeByLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MemoForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1904, 1301);
            Controls.Add(madeByLabel);
            Controls.Add(urlPanel);
            Controls.Add(saveLoadPanel);
            Controls.Add(memoTextBox);
            Controls.Add(memoTitleLabel);
            Controls.Add(sortPanel);
            Controls.Add(deckTextBox);
            Controls.Add(makeRouletteButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "MemoForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "For Daku";
            sortPanel.ResumeLayout(false);
            saveLoadPanel.ResumeLayout(false);
            urlPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button makeRouletteButton;
        private System.Windows.Forms.RichTextBox deckTextBox;
        private System.Windows.Forms.Panel sortPanel;
        private System.Windows.Forms.ComboBox optionComboBox;
        private System.Windows.Forms.Label memoTitleLabel;
        private System.Windows.Forms.RichTextBox memoTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Panel saveLoadPanel;
        private System.Windows.Forms.Button urlButtonMM;
        private System.Windows.Forms.Button urlButtonDC;
        private System.Windows.Forms.Button urlButtonCF;
        private System.Windows.Forms.Button urlButtonYT;
        private System.Windows.Forms.Panel urlPanel;
        private System.Windows.Forms.Label madeByLabel;
    }
}