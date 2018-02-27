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
    class Ellipse : Rectangle
    {
        public override double Width
        {
            get
            {
                return drawBase.Width;
            }
            set
            {
                drawBase.Width = value;
                CornerRounding();
            }
        }
        public override double Height
        {
            get
            {
                return drawBase.Height;
            }
            set
            {
                drawBase.Height = value;
                CornerRounding();
            }
        }

        public Ellipse(Color color, Point topLeft, Point bottomRight) : base(color, topLeft, bottomRight)
        {
            CornerRounding();
        }

        protected void CornerRounding()
        {
            ((System.Windows.Shapes.Rectangle)drawBase).RadiusX = drawBase.Width / 2;
            ((System.Windows.Shapes.Rectangle)drawBase).RadiusY = drawBase.Height / 2;
        }
    }
}
