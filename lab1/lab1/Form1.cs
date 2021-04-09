using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();
            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader Reader = new StreamReader(OpenDlg.FileName, Encoding.Default);
                richTextBox1.Text = Reader.ReadToEnd();
                Reader.Close();
            }
            OpenDlg.Dispose();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();
            if (SaveDlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Writer = new StreamWriter(SaveDlg.FileName);
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    Writer.WriteLine((string)listBox3.Items[i]);
                }
                Writer.Close();
            }
            SaveDlg.Dispose();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Зырянов, Ярыгин - ИП-916");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            richTextBox1.Clear();
            textBox1.Clear();
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            listBox1.BeginUpdate();

            string[] Strings = richTextBox1.Text.Split(new char[] { '\n', '\t', ' ' },
            StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in Strings)
            {
                string Str = s.Trim();

                if (Str == String.Empty)
                    continue;
                if (radioButton1.Checked)
                    listBox1.Items.Add(Str);
                if (radioButton2.Checked)
                {
                    if (Regex.IsMatch(Str, @"\d"))
                        listBox1.Items.Add(Str);
                }
                if (radioButton3.Checked)
                {
                    if (Regex.IsMatch(Str, @"\w+@\w+\.\w+"))
                        listBox1.Items.Add(Str);
                }
            }
            listBox1.EndUpdate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            string Find = textBox1.Text;
            if (checkBox1.Checked)
            {
                foreach (string String in listBox1.Items)
                {
                    if (String.Contains(Find))
                        listBox3.Items.Add(String);
                }
            }
            if (checkBox2.Checked)
            {
                foreach (string String in listBox2.Items)
                {
                    if (String.Contains(Find))
                        listBox3.Items.Add(String);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form2 AddRec = new Form2();
            AddRec.Owner = this;
            AddRec.ShowDialog();

        }

        void DeleteSelectedItems(ListBox listbox)
        {
            for (int i = listbox.Items.Count - 1; i >= 0; i--)
            {
                if (listbox.GetSelected(i))
                    listbox.Items.RemoveAt(i);
            }

        }
        private void button14_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems(listBox1);
            DeleteSelectedItems(listBox2);
        }
        private void MoveSelectedItems(ListBox lstFrom, ListBox lstTo)
        {
            lstTo.BeginUpdate();
            foreach (object item in lstFrom.SelectedItems)
            {
                lstTo.Items.Add(item);
            }
            lstTo.EndUpdate();
            DeleteSelectedItems(lstFrom);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(listBox1, listBox2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(listBox2, listBox1);
        }

        private void MoveAllItems(ListBox lstFrom, ListBox lstTo)
        {
            lstTo.Items.AddRange(lstFrom.Items);
            lstFrom.Items.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MoveAllItems(listBox1, listBox2);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MoveAllItems(listBox2, listBox1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }


        void BubbleSort(int n, string[] A)
        {
            int i, j;
            string t;
            for (i = 0; i < n - 1; i++)
            {
                for (j = n - 1; j > i; j--)
                {
                    if (String.Compare(A[j],A[j - 1])<0 )
                    {
                        t = A[j];
                        A[j] = A[j - 1];
                        A[j - 1] = t;
                    }
                }
            }
        }
        void BubbleLengthSort(int n, string[] A)
        {
            int i, j;
            string t;
            for (i = 0; i < n - 1; i++)
            {
                for (j = n - 1; j > i; j--)
                {
                    if (A[j].Length< A[j - 1].Length)
                    {
                        t = A[j];
                        A[j] = A[j - 1];
                        A[j - 1] = t;
                    }
                }
            }
        }

        void SortListBox(ListBox list, ComboBox combox)
        {
            int n = list.Items.Count;
            string[] stringArray = new string[n];
            this.Cursor = Cursors.WaitCursor;
            switch (combox.SelectedIndex)
            {
                case 0:
                    list.Sorted = true;
                    break;
                case 1:
                    list.Sorted = false;
                    for (int i = n - 1; i >= 0; i--)
                    {
                        stringArray[i]=list.Items[i].ToString();
                    }
                    list.Items.Clear();
                    BubbleSort(n, stringArray);
                    Array.Reverse(stringArray);
                    for(int i=0; i < n; i++)
                    {
                        list.Items.Add(stringArray[i]);
                    }
                    break;
                case 2:
                    list.Sorted = false;
                    for (int i = n - 1; i >= 0; i--)
                    {
                        stringArray[i] = list.Items[i].ToString();
                    }
                    list.Items.Clear();
                    BubbleLengthSort(n, stringArray);
                    for (int i = 0; i < n; i++)
                    {
                        list.Items.Add(stringArray[i]);
                    }
                    break;
                case 3:
                    list.Sorted = false;
                    list.Sorted = false;
                    for (int i = n - 1; i >= 0; i--)
                    {
                        stringArray[i] = list.Items[i].ToString();
                    }
                    list.Items.Clear();
                    BubbleLengthSort(n, stringArray);
                    Array.Reverse(stringArray);
                    for (int i = 0; i < n; i++)
                    {
                        list.Items.Add(stringArray[i]);
                    }
                    break;
                default:
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Выберите критерий сортировки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            this.Cursor = Cursors.Default;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            SortListBox(listBox1, comboBox1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SortListBox(listBox2, comboBox2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
