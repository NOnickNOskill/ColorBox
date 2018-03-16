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
    class Rectangle : Shape
    {
       
        
        public Rectangle(Color color, Point topLeft, Point bottomRight) : base(color, topLeft, bottomRight)
        {
            drawBase = new System.Windows.Shapes.Rectangle();
            SetFill();
            SetSides();
        }

        protected override void SetSides()
        {
            if (bottomRight.X - topLeft.X >= 0)
            {
                Width = bottomRight.X - topLeft.X;
            }

            if (bottomRight.Y - topLeft.Y >= 0)
            {
                Height = bottomRight.Y - topLeft.Y;
            }
          
        }
    }
}
