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
    [Serializable]
    public class CompositeFigure: Figure
    {
        public List<System.Windows.Shapes.Path> partFigures;
    
        public CompositeFigure() { }

        public CompositeFigure(Canvas canvas, Color color, Point startPoint, Point endPoint) : base(canvas, color, startPoint, endPoint)
        {
            MainWindow mainWindow = new MainWindow();
            this.typeName = mainWindow.compFigNameEn;
            this.typeNameRu = mainWindow.compFigNameRu;
        }

        public CompositeFigure(Canvas canvas, Color color, Point startPoint, Point endPoint, List<System.Windows.Shapes.Path> partFigures, 
                                string typeName, string typeNameRu) : this(canvas, color, startPoint, endPoint)
        {
            this.partFigures = partFigures;
            this.typeName = typeName;
            this.typeNameRu = typeNameRu;
        }

        public override void Draw()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < partFigures.Count(); i++)
            {
                geometryGroup.Children.Add(partFigures[i].Data);
            }
            path.Data = geometryGroup;      
        }

        public void Save()
        {

        }
    }
}
