using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Runtime.Serialization;


namespace GraphicEditor
{
    [KnownType(typeof(Figure))]
    [DataContract(Name = "Figure")]
    public abstract class Figure
    {
        [DataMember]
        public Color color;
        [DataMember]
        protected Point startPoint;
        [DataMember]
        protected Point endPoint;
    //    [DataMember]
        public Path path;
        public Canvas canvas;
        public string typeName;

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

        public abstract void Draw();

        ~Figure()
        {
            //   pen.Dispose();
        }
    }
}
