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
    [KnownType(typeof(Circle))]
    [DataContract(Name = "Circle")]
    public class Circle: Square
    {
        public Circle(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Circle";
        }

        protected double RadiusY
        {
            get { return Height / 2; }
        }

        protected Point Center
        {
            get { return new Point(startPoint.X+RadiusY, startPoint.Y+ RadiusY); }
        }

        public override void Draw()
        {
            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.Center = Center;
            ellipseGeometry.RadiusX = RadiusY;
            ellipseGeometry.RadiusY = RadiusY;

            path.Data = ellipseGeometry;
            canvas.Children.Add(path);
        }
    }
}
