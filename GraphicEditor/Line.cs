using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class Line: Figure
    {
        public Line(Color color, Point startPoint, Point endPoint):base (color, startPoint, endPoint)
        { }

        public override void Draw()
        {
            LineGeometry lineGeometry = new LineGeometry();
            lineGeometry.StartPoint = StartPoint;
            lineGeometry.EndPoint = EndPoint;

            path.Data = lineGeometry;
        }
    }
}
