using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    [KnownType(typeof(Line))]
    [DataContract(Name = "Line")]
    public class Line: Figure
    {
        public Line(Canvas canvas, Color color, Point startPoint, Point endPoint):base (canvas, color, startPoint, endPoint)
        {
            typeName = "Line";
        }

        public override void Draw()
        {
            LineGeometry lineGeometry = new LineGeometry();
            lineGeometry.StartPoint = StartPoint;
            lineGeometry.EndPoint = EndPoint;

            path.Data = lineGeometry;
            canvas.Children.Add(path);
        }
    }
}
