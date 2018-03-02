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
    class FigureList 
    {
        public List<Shape> allShapes = new List<Shape>()
        {
           new Rectangle(Colors.Black, new Point(), new Point()),
           new Square(Colors.Black, new Point(), new Point()),
           new Circle(Colors.Black, new Point(), new Point()),
           new Ellipse(Colors.Black, new Point(), new Point()),
           new Polygon(Colors.Black, new Point[3] {new Point(), new Point(), new Point()})
        };
    }
}
