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
    public class RightTriangle : Figure, ITriangle
    {
        public Figure curFigure;
     
        public RightTriangle(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "RightTriangle";
            typeNameRu = "Прямоугольный треугольник";
        }

        public override void Draw()
        {
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            pf.IsClosed = true;
            pf.Segments.Add(new LineSegment(new Point(endPoint.X, endPoint.Y), true));
            pf.Segments.Add(new LineSegment(new Point(startPoint.X, endPoint.Y), true));

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            path.Data = pg;
        }
    }
}
