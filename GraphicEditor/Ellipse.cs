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
    public class Ellipse: Rectangle
    {
        public Ellipse(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Ellipse";
            typeNameRu = "Эллипс";
        }

        public override void Draw(Canvas canvas)
        {
            size = new Size(Width, Height);
            Rect rect = new Rect(startPoint, size);
            EllipseGeometry ellipseGeometry = new EllipseGeometry(rect);

            path.Data = ellipseGeometry;
            try
            {
                canvas.Children.Add(path);
            }
            catch
            {
                MessageBox.Show("error with drawing");
            }
        }
    }
}
