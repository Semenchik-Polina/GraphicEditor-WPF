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

        public override void Draw()
        {
            size = new Size(Height, Height);
            Rect rect = new Rect(StartPoint, size);
            EllipseGeometry ellipseGeometry = new EllipseGeometry(rect);
            
            path.Data = ellipseGeometry;
            canvas.Children.Add(path);
        }
    }
}
