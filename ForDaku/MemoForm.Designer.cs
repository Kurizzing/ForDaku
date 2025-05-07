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
            this.sortButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.memoTitleLabel = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // makeRouletteButton
            // 
            this.makeRouletteButton.Location = new System.Drawing.Point(1685, 971);
            this.makeRouletteButton.Name = "makeRouletteButton";
            this.makeRouletteButton.Size = new System.Drawing.Size(100, 45);
            this.makeRouletteButton.TabIndex = 0;
            this.makeRouletteButton.Text = "r";
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
            this.richTextBox1.Size = new System.Drawing.Size(1000, 1041);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optionComboBox);
            this.panel1.Controls.Add(this.sortButton);
            this.panel1.Location = new System.Drawing.Point(1006, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 77);
            this.panel1.TabIndex = 4;
            // 
            // optionComboBox
            // 
            this.optionComboBox.FormattingEnabled = true;
            this.optionComboBox.Location = new System.Drawing.Point(3, 50);
            this.optionComboBox.Name = "optionComboBox";
            this.optionComboBox.Size = new System.Drawing.Size(90, 20);
            this.optionComboBox.TabIndex = 6;
            this.optionComboBox.SelectedIndexChanged += new System.EventHandler(this.optionComboBox_SelectedIndexChanged);
            // 
            // sortButton
            // 
            this.sortButton.Location = new System.Drawing.Point(3, 3);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(90, 42);
            this.sortButton.TabIndex = 4;
            this.sortButton.Text = "정렬";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1448, 1020);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // memoTitleLabel
            // 
            this.memoTitleLabel.AutoSize = true;
            this.memoTitleLabel.Font = new System.Drawing.Font("굴림", 20F);
            this.memoTitleLabel.Location = new System.Drawing.Point(1007, 9);
            this.memoTitleLabel.Name = "memoTitleLabel";
            this.memoTitleLabel.Size = new System.Drawing.Size(88, 27);
            this.memoTitleLabel.TabIndex = 6;
            this.memoTitleLabel.Text = "Memo";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("굴림", 30F);
            this.richTextBox2.Location = new System.Drawing.Point(1006, 42);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(886, 270);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1144, 399);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(88, 44);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "저장";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(1144, 449);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(88, 44);
            this.loadButton.TabIndex = 9;
            this.loadButton.Text = "불러오기";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // MemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.memoTitleLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.makeRouletteButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MemoForm";
            this.Text = "For Daku";
            this.Load += new System.EventHandler(this.MemoForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button makeRouletteButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.ComboBox optionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label memoTitleLabel;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
    }
}