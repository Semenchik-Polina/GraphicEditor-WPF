using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicEditor
{
    public class Triangle: Figure
    {
        public Triangle(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Triangle";
        }

        public override void Draw(Canvas canvas)
        {
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            pf.IsClosed = true;
            pf.Segments.Add(new LineSegment(new Point(endPoint.X,startPoint.Y), true));
            pf.Segments.Add(new LineSegment(new Point((startPoint.X + endPoint.X) / 2, endPoint.Y),true));

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            path.Data = pg;
            try
            {
                canvas.Children.Add(path);
            }
            catch
            { }
        }
    }
}
