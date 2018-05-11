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
    [KnownType(typeof(Ellipse))]
    [DataContract(Name = "Ellipse")]
    public class Ellipse: Rectangle
    {
        public Ellipse(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Ellipse";
        }

        protected double RadiusY
        {
            get { return Height / 2; }
        }

        protected double RadiusX
        {
            get { return Width / 2; }
        }

        protected Point Center
        {
            get { return new Point(startPoint.X + RadiusX, startPoint.Y + RadiusY); }
        }

        public override void Draw()
        {
            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.Center = Center;
            ellipseGeometry.RadiusX = RadiusX;
            ellipseGeometry.RadiusY = RadiusY;

            path.Data = ellipseGeometry;
            canvas.Children.Add(path);
        }
    }
}
