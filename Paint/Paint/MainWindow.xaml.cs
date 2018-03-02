﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paint.classes;

namespace Paint
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool flag = false;
        public Point topLeft;
        private classes.Shape shape;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            classes.Shape shape = new classes.Rectangle(Colors.Green, new Point(100, 100), new Point(200, 300));
            classes.Drawing.Draw(shape, canvas);

            shape = new Square(Colors.Blue, new Point(400, 100), new Point(500, 300));
            classes.Drawing.Draw(shape, canvas);

            shape = new classes.Ellipse(Colors.Red, new Point(100, 400), new Point(300, 500));
            classes.Drawing.Draw(shape, canvas);

            shape = new Circle(Colors.Yellow, new Point(800, 400), new Point(900, 600));
            classes.Drawing.Draw(shape, canvas);

            Point[] triangle = new Point[3] { new Point(800, 200), new Point(1000, 200), new Point(1000, 400) };
            shape = new classes.Polygon(Colors.DarkBlue, triangle);
            classes.Drawing.Draw(shape, canvas);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }


        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(canvas);
            shape = new Square(Colors.Purple, new Point (position.X,position.Y), new Point(position.X + 100, position.Y + 200));
            topLeft = position;
            classes.Drawing.Draw(shape, canvas);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = Mouse.GetPosition(canvas);
                shape.bottomRight = position;
            }
        }
    }
}
