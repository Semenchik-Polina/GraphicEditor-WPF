using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraphicEditor
{
    public class CompositeFigure: Figure
    {
        public Geometry[] partFigures;

        public CompositeFigure(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        { }

        public CompositeFigure(Canvas canvas, Color color, Point startPoint, Point endPoint, Geometry[] partFigures, 
                                string typeName, string typeNameRu) : this(canvas, color, startPoint, endPoint)
        {
            this.partFigures = partFigures;
            this.typeName = typeName;
            this.typeNameRu = typeNameRu;
        }

        public override void Draw()
        {
   /*         Point startPoint = partFigures[0].Bounds.TopLeft;
            Geometry mainFigure = partFigures[0];
            for (int i=1; i< partFigures.Count(); i++)
            {
                if (startPoint.X > partFigures[i].Bounds.TopLeft.X || startPoint.Y > partFigures[i].Bounds.TopLeft.Y)
                {
                    startPoint = partFigures[i].Bounds.TopLeft;
                    mainFigure = partFigures[i];
                }
            }
     */   }
    }
}
