using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicEditor
{
   public partial class MainWindow : Window
    {
        protected List<Figure> listOfFigures = new List<Figure>();
        public Figure curFigure;
        public Color color = Color.FromRgb(0,0,0);
        protected Point startPoint, endPoint;
        Canvas canvas;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTriangle_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Triangle(canvas, color, startPoint, endPoint);
        }

        private void BSquare_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Square(canvas, color, startPoint, endPoint);
        }

        private void BRectangle_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Rectangle(canvas, color, startPoint, endPoint);
        }

        private void BCircle_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Circle(canvas, color, startPoint, endPoint);
        }

        private void BEllipse_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Ellipse(canvas, color, startPoint, endPoint);
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            if (canvas.Children.Count != 0)
            {
                canvas.Children.Remove(canvas.Children[canvas.Children.Count - 1]);
            }
        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void BLine_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Line(CanvasMain, color, startPoint, endPoint);
        }

        private void CanvasMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            endPoint = e.GetPosition(CanvasMain);

            curFigure = (Figure)Activator.CreateInstance(curFigure.GetType(),canvas, color, startPoint, endPoint);
            curFigure.Draw();
            listOfFigures.Add(curFigure);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(0, 0, 0);
            curFigure = new Line(canvas, color, startPoint, endPoint);
            canvas = CanvasMain;
        }

        private void CanvasMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(CanvasMain);
        }



    }
}
