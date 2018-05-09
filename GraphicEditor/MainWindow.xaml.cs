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
        List<Figure> listOfFigures = new List<Figure>();
        Figure curFigure;
        Color color;
        Point startPoint, endPoint;

        public MainWindow()
        {
            InitializeComponent();
            color = Color.FromRgb(0,0,0);
            curFigure = new Line(color, startPoint, endPoint);
        }

        private void BTriangle_Click(object sender, RoutedEventArgs e)
        {
            curFigure = 
        }

        private void BSquare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BRectangle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BCircle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BEllipse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BLine_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Line(color, startPoint, endPoint);
        }

       
    }
}
