using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public static TextBox _textBox1;

        public Form1()
        {
            InitializeComponent();
            _textBox1 = textBox1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
