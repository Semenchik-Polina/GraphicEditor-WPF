using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GraphicEditor
{
    public class Square:Figure
    {
        public Square(Color color, Point startPoint, Point endPoint) : base(color, startPoint, endPoint)
        { }

        protected Size size;

        protected double Height
        {
            get { return EndPoint.Y - StartPoint.Y; }
        }

        public override void Draw()
        {
            size = new Size(Height, Height);
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(StartPoint,size);

            path.Data = rectangleGeometry;
        }
    }
}
