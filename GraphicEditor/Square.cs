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
    [KnownType(typeof(Square))]
    [DataContract(Name = "Square")]
    public class Square:Figure
    {
        public Square(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Square";
        }

        protected Size size;

        protected double Height
        {
            get {
                if (startPoint.Y > endPoint.Y)
                {
                    Point temp = new Point();
                    temp = endPoint;
                    endPoint = startPoint;
                    startPoint = temp;
                }
                return EndPoint.Y - StartPoint.Y;
            }
        }

        public override void Draw()
        {
            size = new Size(Height, Height);
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(StartPoint,size);

            path.Data = rectangleGeometry;
            canvas.Children.Add(path);
        }
    }
}
