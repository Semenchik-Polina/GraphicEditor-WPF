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
    public class Rectangle : Square
    {
        public Rectangle(): base()
        { }

        public Rectangle(Canvas canvas, Color color, Point startPoint, Point endPoint):base (canvas, color, startPoint, endPoint)
        {
            typeName = "Rectangle";
            typeNameRu = "Прямоугольник";
        }

        protected double Width
        {
            get {
                if (startPoint.X > endPoint.X)
                {
                    double temp;
                    temp = endPoint.X;
                    endPoint.X = startPoint.X;
                    startPoint.X = temp;
                }
                return endPoint.X - startPoint.X;
            }
        }

        public override void Draw()
        {
            size = new Size(Width, Height);
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(startPoint, size);

            path.Data = rectangleGeometry;
        }

    }
}
