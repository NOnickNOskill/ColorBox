using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint.classes
{
    class CircleCreator : Creator
    {
        public override Shape FactoryMethod(Color color, Point topLeft, Point bottomRight)
        {
            return new Circle(color, topLeft, bottomRight);
        }
    }
}
