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
    abstract class Shape : ICloneable
    {
        public Color Color
        {
            get
            {
                return color; 
            }
            set
            {
                color = value;
                SetFill();
            }
        }
        protected Color color;
        public  Point topLeft;
        public Point TopLeft
        {
            get
            {
                return topLeft;
            }
            set
            {
                topLeft = value;
                SetSides();
            }
        }
        protected Point bottomRight;
        public Point BottomRight
        {
            get
            {
                return bottomRight;
            }
            set
            {
                bottomRight = value;
                SetSides();
            }
        }

        public System.Windows.Shapes.Shape drawBase;

        public Shape(Color color, Point topLeft = new Point(), Point bottomRight = new Point())
        {
            this.color = color;
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        protected void SetFill()
        {
            drawBase.Fill = new SolidColorBrush(color);
        }

        public Object Clone()
        {
            drawBase = new System.Windows.Shapes.Rectangle();
            return MemberwiseClone();
        }

        protected abstract void SetSides();
      
    }
}
