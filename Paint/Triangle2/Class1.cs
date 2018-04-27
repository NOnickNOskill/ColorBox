using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.classes;
using Paint;
using System.Windows.Media;
using System.Windows;

namespace Square2
{
    public class Square : Rectangle, Plugin
    {
        public override double Width
        {
            get
            {
                return drawBase.Width;
            }
            set
            {
                base.Width = value;
                if (value <= Height)
                {
                    base.Width = value;
                    base.Height = value;
                }
                else
                {
                    base.Height = Width;
                }
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
                base.Height = value;
                if (value <= Width)
                {
                    base.Height = value;
                    base.Width = value;
                }
                else
                {
                    base.Height = Width;
                }
            }
        }

        public Square(Color color, Point topLeft, Point bottomRight) : base(color, topLeft, bottomRight)
        {
        }
    }

    class SquareCreator : Creator, IPluginFactory
    {
        public override Shape FactoryMethod(Color color, Point topLeft, Point bottomRight)
        {
            return new Square(color, topLeft, bottomRight);
        }
    }

}
