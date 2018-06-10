using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TriangleInterfaceLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GraphicEditor;

namespace TriangleLib
{
    public class RegularTriangle : Figure, ITriangle
    {
        public Figure curFigure;
     
        public RegularTriangle(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "RegularTriangle";
            typeNameRu = "Правильный треугольник";
        }

        public override void Draw()
        {
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            pf.IsClosed = true;
            double side = (endPoint.X - startPoint.X);
            pf.Segments.Add(new LineSegment(new Point(endPoint.X, startPoint.Y), true));
            pf.Segments.Add(new LineSegment(new Point(startPoint.X+side/2, startPoint.Y + Math.Sqrt(3)*side/2 ), true));

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            path.Data = pg;
           
        }
    }
}
