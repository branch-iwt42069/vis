using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                int m = Convert.ToInt32(numericUpDown2.Value);
                //main.numericUpDown1.Value = n;
                //main.numericUpDown2.Value = m;
                main.size(n, m);
            }
            this.Close();
        } 
    }
}