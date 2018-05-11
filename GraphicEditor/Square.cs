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
                return endPoint.Y - startPoint.Y;
            }
        }

        public override void Draw(Canvas canvas)
        {
            size = new Size(Height, Height);
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(startPoint,size);

            path.Data = rectangleGeometry;
            try
            {
                canvas.Children.Add(path);
            }
            catch
            { }
        }
    }
}
