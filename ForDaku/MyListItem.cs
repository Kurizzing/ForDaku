using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForDaku
{
    public partial class MyListItem : UserControl
    {
        public MyListItem()
        {
            InitializeComponent();

            button1.Click += button1_Click;
        }

        public string TextBoxValue
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public int NumericUpDownValue
        {
            get { return (int)numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }

        public NumericUpDown NumericUpDownControl
        {
            get { return numericUpDown1; }
        }

        public Button ButtonControl
        {
            get { return button1; }
        }

        public string LabelText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public Color ItemColor
        {
            get { return panel1.BackColor; }
            set { panel1.BackColor = value; }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //// UserControl을 포함한 FlowLayoutPanel에서 자신을 제거
            //FlowLayoutPanel parentPanel = this.Parent as FlowLayoutPanel;
            //if (parentPanel != null)
            //{
            //    parentPanel.Controls.Remove(this);  // UserControl 제거
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
