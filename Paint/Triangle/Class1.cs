using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.classes;
using Paint;
using System.Windows.Media;
using System.Windows;

namespace Triangle
{
    public class Triangle : Shape, Plugin
    {
        public Triangle(Color color, Point topLeft, Point bottomRight) : base(color, topLeft, bottomRight)
        {
            drawBase = new System.Windows.Shapes.Polygon();
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

    public class TriangleCreator : Creator, IPluginFactory
    {
        public override Shape FactoryMethod(Color color, Point topLeft, Point bottomRight)
        {
            return new Triangle(color, topLeft, bottomRight);
        }
    }

}
