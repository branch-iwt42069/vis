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


namespace _4
{
    public partial class Form1 : Form//разделённый на несколько файлов, класс с именем Form1 - наследник класса Form
    {
        public Form1()// Процедура создания формы Form1
        {
            InitializeComponent();// Инициализация компонентов.
            label5.Text = Convert.ToString(DateTime.Now.ToLongTimeString());
        }
        
        void update_path()//Конструктор,выводит путь к файлу снизу в левом углу
        {
            label3.Text = fileListBox1.Path;
            label4.Text = fileListBox1.FileName;
            label4.Location = new Point(label3.Location.X + label3.Width + 3, label4.Location.Y);
        }

        //поиска и выбора элемента в др ListBox
        private void driveListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            dirListBox1.Path = driveListBox1.SelectedItem.ToString();
            fileListBox1.Path = dirListBox1.Path;
            update_path();
        }

        private void dirListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fileListBox1.Path = dirListBox1.Path;
            update_path();
        }
        // передаёт путь папки левого проводника в проводник по середине
        private void dirListBox1_Change(object sender, EventArgs e)
        {
           fileListBox1.Path = dirListBox1.Path;
            update_path();

        }
        //когда на файл картинки кликаешь,в крайне правом поле она показывается
        private void fileListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = new Bitmap(fileListBox1.Path + "\\" + fileListBox1.FileName); 
            }
            catch
            {
                pictureBox1.Image = null;
            }
            update_path();
        }

        private void timer1_Tick(object sender, EventArgs e)//вывод времени 
        {
            if(radioButton1.Checked == false)
                label5.Text = Convert.ToString(DateTime.Now.ToLongTimeString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Меняет набор файлов в зависимости от выбора в комбо боксе 
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    fileListBox1.Pattern = "*.*";
                    break;
                case 1:
                    fileListBox1.Pattern = "*.jpg;*.png;*.gif;*.bmp";
                    break;
            }  
           
        }
    }
}
