using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace GraphicEditor
{
    public abstract class Figure
    {
        public Color color;
        public Point startPoint;
        public Point endPoint;
        protected Path path;
        protected Canvas canvas;
        public string typeName;
            
        public Figure(Canvas canvas, Color color, Point startPoint, Point endPoint)
        {
            path = new Path();
            SolidColorBrush brush = new SolidColorBrush(color);
            this.path.Stroke = brush;
            path.StrokeThickness = 3;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.canvas = canvas;
        }

        public abstract void Draw(Canvas canvas);

    }
}
