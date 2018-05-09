using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public abstract class Figure
    {
        public Color color;
        protected Point startPoint;
        protected Point endPoint;
        public Path path;

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value;}
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
            
        public Figure(Color color, Point startPoint, Point endPoint)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            this.path.Stroke = brush;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public abstract void Draw();

        ~Figure()
        {
         //   pen.Dispose();
        }
    }
}
