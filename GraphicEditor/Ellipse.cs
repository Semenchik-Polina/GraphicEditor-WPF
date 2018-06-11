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
    [Serializable]
    public class Ellipse: Rectangle
    {
        public Ellipse() : base()
        { }

        public Ellipse(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            typeName = "Ellipse";
            typeNameRu = "Эллипс";
        }

        public override void Draw()
        {
            size = new Size(Width, Height);
            Rect rect = new Rect(startPoint, size);
            EllipseGeometry ellipseGeometry = new EllipseGeometry(rect);

            path.Data = ellipseGeometry;
        }
    }
}
