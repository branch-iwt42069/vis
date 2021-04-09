using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    //partial - говорит о том, что код класса находится в нескольких файлах, т.е. часть непосредственно в указанном вами примере, а часть непосредственно в файле дизайнера этой же формы.
    //Form1 : Form - это говорит о том, что класс Form1 наследуется от класса Form
    public partial class Form1 : Form
    {
        private int left_lbText;
        private bool isLabelsEmpty;
        private Color txtBoxClr;
        private int counter;
        public Form1()
        {
            InitializeComponent();
            hScrollBar1.Value = button1.Width;
            hScrollBar2.Value = label1.Width;
            label5.Left = label3.Left;
            left_lbText = label5.Left;
            isLabelsEmpty = false;
            counter = 0;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            button1.Width = hScrollBar1.Value;
            button2.Width = hScrollBar1.Value;
            button3.Width = hScrollBar1.Value;

            

        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            label1.Width = hScrollBar2.Value;
            label2.Width = hScrollBar2.Value;
            label3.Width = hScrollBar2.Value;
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            isLabelsEmpty = true;
            label3.Text = string.Empty;
            label2.Text = string.Empty;
            label1.Text = string.Empty;

        }

        private void label5_Click(object sender, EventArgs e)
        {
            
            if (isLabelsEmpty)
            {
                string temp = button1.Text;
                button1.Text = button2.Text;
                button2.Text = button3.Text;
                button3.Text = temp;
            }
            else
            {
                MessageBox.Show("Сначала нажмите заголовок \"Текст\"", "Сообщение");
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            label6.BackColor = SystemColors.ActiveBorder;
            textBox1.DoDragDrop(textBox1.Text, DragDropEffects.Move);
        }

        private void label6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void label6_DragDrop(object sender, DragEventArgs e)
        {
            label6.Text = (string)e.Data.GetData(DataFormats.Text);
            textBox1.Text = string.Empty;
            label6.BackColor = Color.AliceBlue;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                txtBoxClr = dlg.Color;
            }
            dlg.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            counter++;
            Point p1 = new Point();
            TabPage newTabPage = new TabPage();
            RichTextBox newRichTxtBox = new RichTextBox();
            newRichTxtBox.Text = richTextBox1.Text;
            newRichTxtBox.SelectionAlignment = HorizontalAlignment.Center;
            newRichTxtBox.Font = new Font("Consolas", 20f, FontStyle.Bold);
            newRichTxtBox.ForeColor = Color.LawnGreen;
            newRichTxtBox.ReadOnly = true;
            newRichTxtBox.BackColor = txtBoxClr;
            p1.X = (int)(tabPage3.Width * 0.1);
            p1.Y = (int)(tabPage3.Height * 0.06);
            newRichTxtBox.Location = p1;
            newRichTxtBox.Width = (int)(tabPage3.Width * 0.8);
            newRichTxtBox.Height = (int)(tabPage3.Height * 0.88);
            newTabPage.Text = "Card" + counter.ToString();
            newTabPage.BackColor = Color.LawnGreen;
            newTabPage.Controls.Add(newRichTxtBox);
            tabControl1.TabPages.Add(newTabPage);
        }

        private void From1_Resize(object sender, EventArgs e)
        {

        }
    }
}
