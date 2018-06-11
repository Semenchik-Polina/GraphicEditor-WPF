using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Reflection;
using System.Xml.Serialization;

namespace GraphicEditor
{
    [XmlInclude(typeof(Ellipse))]
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(Square))]
    [XmlInclude(typeof(Rectangle))]
    [Serializable]
    public abstract class Figure
    {
        public Color color;
        public Point startPoint;
        public Point endPoint;
        protected Path path;
        protected Canvas canvas;
        public string typeName;
        public string typeNameRu;
        //    public string lang;

        /*   public string TypeName
           { get
               {
                   if (this.lang == "en")
                       return typeName;
                   else
                       return typeNameRu;
               }
           }
          */
        
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

        public Figure() 
        {  }


        public abstract void Draw();

        public Button MakeButton(string lang)
        {
            Button button = new Button();
            button.Name = "B" + typeName;
            if (lang == "en")
                button.Content = typeName;
            if (lang == "ru")
                button.Content = typeNameRu;
            return button;
        }

        public void ToCanvas()
        {
            try
            {
                canvas.Children.Add(path);
         //       canvas.Children[canvas.Children.Count - 1].Clip = path.Data;
                canvas.InvalidateVisual();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
