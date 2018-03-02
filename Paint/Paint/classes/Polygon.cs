﻿using System;
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
    class Polygon : Shape
    {
        public  Polygon(Color color, Point[] vertecies) : base(color)
        {
            drawBase = new System.Windows.Shapes.Polygon();
            SetFill();

            ((System.Windows.Shapes.Polygon)drawBase).Points = new PointCollection(vertecies);
        }

        protected override void SetSides()
        {
        
        }

    }
}
