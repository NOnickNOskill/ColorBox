using System;
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

namespace Paint.classes
{
    static class Drawing
    {
        public static void Draw(classes.Shape shape, Canvas canvas)
        {
            //Canvas.SetLeft(shape.drawBase, shape.topLeft.X);
            //Canvas.SetTop(shape.drawBase, shape.topLeft.Y);
            canvas.Children.Add(shape.drawBase);
        }
    }
}
