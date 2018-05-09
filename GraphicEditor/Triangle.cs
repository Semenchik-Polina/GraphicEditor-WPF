﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GraphicEditor
{
    public class Triangle: Figure
    {
        public Triangle(Color color, Point startPoint, Point endPoint) : base(color, startPoint, endPoint)
        { }

        public override void Draw()
        {
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            pf.IsClosed = true;
            pf.Segments.Add(new LineSegment(new Point(endPoint.X,startPoint.Y), true));
            pf.Segments.Add(new LineSegment(new Point((startPoint.X + endPoint.X) / 2, endPoint.Y),true));

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            path.Data = pg;
        }
    }
}
