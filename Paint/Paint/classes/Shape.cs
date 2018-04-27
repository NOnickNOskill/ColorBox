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
using Newtonsoft.Json;

namespace Paint.classes
{
    [JsonObject(MemberSerialization.OptIn)]
    abstract public class Shape
    {
        [JsonProperty]
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
        [JsonProperty]
        public Point TopLeft
        {
            get
            {
                return topLeft;
            }
            set
            {
                topLeft = value;
                SetPosition();
            }
        }
        protected Point bottomRight;
        [JsonProperty]
        public Point BottomRight
        {
            get
            {
                return bottomRight;
            }
            set
            {
                SetVertex2X(value);
                SetVertex2Y(value);
                SetPosition();
                SetSides();
            }
        }

        [JsonProperty]
        public Type factoryType;

        public virtual double Width
        {
            get
            {
                return drawBase.Width;
            }
            set
            {
                drawBase.Width = value;
            }
        }
        public virtual double Height
        {
            get
            {
                return drawBase.Height;
            }
            set
            {
                drawBase.Height = value;
            }
        }
        [JsonProperty]
        public double OffsetX
        {
            get
            {
                return offsetx;
            }
            set
            {
                offsetx = value;
                Move(offsetx, offsety);
            }
        }

        private void Move(double offsetx, double offsety)
        {
            drawBase.RenderTransform = new TranslateTransform(offsetx, offsety);
        }
        [JsonProperty]
        public double OffsetY
        {
            get
            {
                return offsety;
            }
            set
            {
                offsety = value;
                Move(offsetx, offsety);
            }
        }

        private double offsetx = 0, offsety = 0;

        public bool reverseX, reverseY;

        protected void SetVertex2X(Point v2)
        {
            double v1X = TopLeft.X;
            if (reverseX)
            {
                v1X += Width;
            }

            if (v1X > v2.X)
            {
                topLeft.X = v2.X;

                bottomRight.X = v1X;
                reverseX = true;
            }
            else
            {
                bottomRight.X = v2.X;
                reverseX = false;
            }
        }

        protected void SetVertex2Y(Point v2)
        {
            double v1Y = TopLeft.Y;
            if (reverseY)
            {
                v1Y += Height;
            }

            if (v1Y > v2.Y)
            {
                topLeft.Y = v2.Y;

                bottomRight.Y = v1Y;
                reverseY = true;
            }
            else
            {
                bottomRight.Y = v2.Y;
                reverseY = false;
            }
        }

        public void SetPosition()
        {
            Canvas.SetLeft(drawBase, topLeft.X);
            Canvas.SetTop(drawBase, topLeft.Y);
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
