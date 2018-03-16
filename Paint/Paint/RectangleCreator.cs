using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace Paint.classes
{
    class RectangleCreator : Creator
    {
        public override Shape FactoryMethod(Color color, Point topLeft, Point bottomRight)
        {
            return new Rectangle(color, topLeft, bottomRight);
        }
    }
}
