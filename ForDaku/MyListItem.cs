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
        }

        public string TextBoxValue
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public TextBox TextBoxControl
        {
            get { return textBox1; }
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
    }
}
