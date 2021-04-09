using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6
{
    public partial class Form1 : Form
    {
        bool DraggingR = false;
        bool DraggingC = false;
        bool DraggingS = false;
        int X, Y, dX, dY;
        int LastClicked = 0;
        Rectangle Rectangle = new Rectangle(10, 10, 210, 100);
        Rectangle Circle = new Rectangle(230, 10, 150, 150);
        Rectangle Square = new Rectangle(390, 10, 150, 150);
        int RectangleX = 0;
        int RectangleY = 0;
        int CircleX = 0;
        int CircleY = 0;
        int SquareX = 0;
        int SquareY = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.X < Rectangle.X + Rectangle.Width) && (e.X > Rectangle.X))
            {
                if ((e.Y < Rectangle.Y + Rectangle.Height) && (e.Y > Rectangle.Y))
                {
                    DraggingR = true;
                    RectangleX = e.X - Rectangle.X;
                    RectangleY = e.Y - Rectangle.Y;
                    LastClicked = 1;
                    return;
                }
            }
            if ((e.X < Circle.X + Circle.Width) && (e.X > Circle.X))
            {
                if ((e.Y < Circle.Y + Circle.Height) && (e.Y > Circle.Y))
                {
                    DraggingC = true;
                    CircleX = e.X - Circle.X;
                    CircleY = e.Y - Circle.Y;
                    LastClicked = 2;
                    return;
                }
            }
            if ((e.X < Square.X + Square.Width) && (e.X > Square.X))
            {
                if ((e.Y < Square.Y + Square.Height) && (e.Y > Square.Y))
                {
                    DraggingS = true;
                    SquareX = e.X - Square.X;
                    SquareY = e.Y - Square.Y;
                    LastClicked = 3;
                    return;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, Square);
            e.Graphics.FillEllipse(Brushes.Red, Circle);
            e.Graphics.FillRectangle(Brushes.Yellow, Rectangle);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            DraggingR = false;
            DraggingC = false;
            DraggingS = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = "Информация";
            if (DraggingR)
            {
                Rectangle.X = e.X - RectangleX;
                Rectangle.Y = e.Y - RectangleY;
            }
            if (DraggingC)
            {
                Circle.X = e.X - CircleX;
                Circle.Y = e.Y - CircleY;
            }
            if (DraggingS)
            {
                Square.X = e.X - SquareX;
                Square.Y = e.Y - SquareY;
            }
            if ((label1.Location.X < Rectangle.X + Rectangle.Width) && (label1.Location.X + label1.Width > Rectangle.X))
            {
                if ((label1.Location.Y < Rectangle.Y + Rectangle.Height) && (label1.Location.Y + label1.Height > Rectangle.Y))
                {
                    if (label3.Text == "Информация")
                    {
                        label3.Text = "Жёлтый прямоугольник";
                    }
                    else
                        label3.Text = string.Concat(label3.Text, "\nЖёлтый прямоугольник");
                }
            }
            if ((label1.Location.X < Circle.X + Circle.Width) && (label1.Location.X + label1.Width > Circle.X))
            {
                if ((label1.Location.Y < Circle.Y + Circle.Height) && (label1.Location.Y + label1.Height > Circle.Y))
                {
                    if (label3.Text == "Информация")
                    {
                        label3.Text = "Красный круг";
                    }
                    else
                        label3.Text = string.Concat(label3.Text, "\nКрасный круг");
                }
            }
            if ((label1.Location.X < Square.X + Square.Width) && (label1.Location.X + label1.Width > Square.X))
            {
                if ((label1.Location.Y < Square.Y + Square.Height) && (label1.Location.Y + label1.Height > Square.Y))
                {
                    if (label3.Text == "Информация")
                    {
                        label3.Text = "Синий квадрат";
                    }
                    else
                        label3.Text = string.Concat(label3.Text, "\nСиний квадрат");
                }
            }
            if (LastClicked == 1)
            {
                if ((label2.Location.X < Rectangle.X + Rectangle.Width) && (label2.Location.X + label2.Width > Rectangle.X))
                {
                    if ((label2.Location.Y < Rectangle.Y + Rectangle.Height) && (label2.Location.Y + label2.Height > Rectangle.Y))
                    {
                        X = Rectangle.X;
                        Y = Rectangle.Y;
                        dX = RectangleX;
                        dY = RectangleY;
                        Rectangle.X = Circle.X;
                        Rectangle.Y = Circle.Y;
                        RectangleX = CircleX;
                        RectangleY = CircleY;
                        Circle.X = X;
                        Circle.Y = Y;
                        CircleX = dX;
                        CircleY = dY;
                        DraggingR = false;
                        DraggingC = true;
                        LastClicked = 0;
                    }
                }
            }
            if (LastClicked == 2)
            {
                if ((label2.Location.X < Circle.X + Circle.Width) && (label2.Location.X + label2.Width >Circle.X))
                {
                    if ((label2.Location.Y < Circle.Y + Circle.Height) &&(label2.Location.Y + label2.Height > Circle.Y))
                    {
                        X = Circle.X;
                        Y = Circle.Y;
                        dX = CircleX;
                        dY = CircleY;
                        Circle.X = Square.X;
                        Circle.Y = Square.Y;
                        CircleX = SquareX;
                        CircleY = SquareY;
                        Square.X = X;
                        Square.Y = Y;
                        SquareX = dX;
                        SquareY = dY;
                        DraggingC = false;
                        DraggingS = true;
                        LastClicked = 0;
                    }
                }
            }
            if (LastClicked == 3)
            {
                if ((label2.Location.X < Square.X + Square.Width) && (label2.Location.X + label2.Width > Square.X))
                {
                    if ((label2.Location.Y < Square.Y + Square.Height) && (label2.Location.Y + label2.Height > Square.Y))
                    {
                        X = Square.X;
                        Y = Square.Y;
                        dX = SquareX;
                        dY = SquareY;
                        Square.X = Rectangle.X;
                        Square.Y = Rectangle.Y;
                        SquareX = RectangleX;
                        SquareY = RectangleY;
                        Rectangle.X = X;
                        Rectangle.Y = Y;
                        RectangleX = dX;
                        RectangleY = dY;
                        DraggingS = false;
                        DraggingR = true;
                        LastClicked = 0;
                    }
                }
            }
            pictureBox1.Invalidate();

        }
    }
}
