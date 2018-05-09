using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GraphicEditor
{
    public class Rectangle : Square
    {
        public Rectangle(Color color, Point startPoint, Point endPoint):base (color, startPoint, endPoint)
        {  }

        protected double Width
        {
            get { return EndPoint.X - StartPoint.X; }
        }

        public override void Draw()
        {
            size = new Size(Width, Height);
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(StartPoint, size);

            path.Data = rectangleGeometry;
        }
    }
}
