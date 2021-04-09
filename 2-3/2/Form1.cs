using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2
{
    public partial class Form1 : Form
    {
        bool drawing;
        int historyCounter; //Счетчик истории

        GraphicsPath currentPath;
        Point oldLocation;
    public Pen currentPen;
        Color historyColor;
        DashStyle hystoryDashStyle;
        List<Image> History; //Список для истории


        public Form1()
        {
            InitializeComponent();
            drawing = false; //Переменная, ответственная за рисование
            currentPen = new Pen(Color.Black); //Инициализация пера с черным цветом
            currentPen.Width = trackBar1.Value;  //Инициализация толщины пера
            History = new List<Image>(); //Инициализация списка для истории

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Paint ver 1.0.1\nЗырянов - ИП-916 \nЯрыгин - ИП-916");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picDrawingSurface.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка ?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: saveToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                }

            }

            History.Clear();
            historyCounter = 0;

            picDrawingSurface.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            int x = panel2.Width;
            int y = panel2.Height;
            Bitmap pic = new Bitmap(x-25, y-10);
            picDrawingSurface.BackColor = Color.White;
            picDrawingSurface.Image = pic;
            Graphics g = Graphics.FromImage(picDrawingSurface.Image);
            g.Clear(Color.White);
            g.DrawImage(picDrawingSurface.Image, 0, 0, x-25, y-10);
            picDrawingSurface.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            History.Add(new Bitmap(picDrawingSurface.Image));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }
            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNGImage | *.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4; //По умолчанию будет выбрано последнее расширение *.png
            SaveDlg.ShowDialog();
            if (SaveDlg.FileName != "") //Если введено не пустое имя
            {
                System.IO.FileStream fs =(System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.picDrawingSurface.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.picDrawingSurface.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3: 
                        this.picDrawingSurface.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        this.picDrawingSurface.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNGImage | *.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1; //По умолчанию будет выбрано первое расширение *.jpg
            if (OP.ShowDialog() != DialogResult.Cancel)
            {
                picDrawingSurface.Load(OP.FileName);
                picDrawingSurface.AutoSize = true;
                picDrawingSurface.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                History.Add(new Bitmap(picDrawingSurface.Image));
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    drawing = true;
                    oldLocation = e.Location;
                    currentPath = new GraphicsPath();
                    historyColor = currentPen.Color;
                    currentPen.Color = Color.White;
                    hystoryDashStyle = currentPen.DashStyle;
                    currentPen.DashStyle = DashStyle.Solid;
                    
                }
            }
        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            History.RemoveRange(historyCounter + 1, History.Count - historyCounter - 1);
            History.Add(new Bitmap(picDrawingSurface.Image));
            if (historyCounter + 1 < 10)
                historyCounter++;
            if (History.Count - 1 == 10)
                History.RemoveAt(0);

            if (e.Button == MouseButtons.Right)
            {
                currentPen.Color = historyColor;
                currentPen.DashStyle = hystoryDashStyle;
            }
            drawing = false;
            try
            {
                currentPath.Dispose();
            }
            catch { };

        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Graphics g = Graphics.FromImage(picDrawingSurface.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                picDrawingSurface.Invalidate();
            }
            label1.Text = e.X.ToString() + ", " + e.Y.ToString();
        }

        private void picDrawingSurface_MouseLeave(object sender, EventArgs e)
        {
            label1.Text = "X, Y";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Width: " + trackBar1.Value;
            currentPen.Width = trackBar1.Value;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                picDrawingSurface.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count - 1)
            {
                picDrawingSurface.Image = new Bitmap(History[++historyCounter]);
            }
            else MessageBox.Show("История пуста");  
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Solid;
            solidToolStripMenuItem.Checked = true;
            DotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;

        }

        private void DotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDot;
            solidToolStripMenuItem.Checked = false;
            DotToolStripMenuItem.Checked = true;
            dashDotDotToolStripMenuItem.Checked = false;

        }

        private void dashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDotDot;
            solidToolStripMenuItem.Checked = false;
            DotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = true;

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 AddRec = new Form2(currentPen.Color);
            AddRec.Owner = this;
            AddRec.ShowDialog();
        }

            private void toolStripButton4_Click(object sender, EventArgs e)
        {
            colorToolStripMenuItem_Click(sender, e);
        }
    }
}
