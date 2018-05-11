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
using Microsoft.Win32;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace GraphicEditor
{
   public partial class MainWindow : Window
    {
        protected List<Figure> listOfFigures = new List<Figure>();
        public List<Visual> hitList;
        public Figure curFigure;
        public Color color = Color.FromRgb(0,0,0);
        protected Point startPoint, endPoint;
        Canvas canvas;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            color = Color.FromRgb(0, 0, 0);
            curFigure = new Line(canvas, color, startPoint, endPoint);
            canvas = CanvasMain;
            hitList = new List<Visual>();
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
            if (ListBoxOfFigures.Items.Count != 0)
            {
                ListBoxOfFigures.Items.Remove(ListBoxOfFigures.Items[ListBoxOfFigures.Items.Count - 1]);

            }
            if (listOfFigures.Count != 0)
            {
                listOfFigures.Remove(listOfFigures[listOfFigures.Count - 1]);
            }
        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            listOfFigures.Clear();
            canvas.Children.Clear();
            ListBoxOfFigures.Items.Clear();
        }

        private void BLine_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new Line(CanvasMain, color, startPoint, endPoint);
        }

        private void CanvasMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (curFigure != null)
            {
                endPoint = e.GetPosition(CanvasMain);
                curFigure = (Figure)Activator.CreateInstance(curFigure.GetType(), canvas, color, startPoint, endPoint);
                curFigure.Draw();
                listOfFigures.Add(curFigure);
                ListBoxItem item = new ListBoxItem();
                item.Content = curFigure.typeName;
                ListBoxOfFigures.Items.Add(item);
            }
        }

        public HitTestResultBehavior HitTestResultHandler(HitTestResult result)
        {
            PointHitTestResult hitResult = (PointHitTestResult)result;
            if (result.VisualHit != CanvasMain)
            {
                hitList.Add((Visual)result.VisualHit);
                return HitTestResultBehavior.Continue;
            }
            return HitTestResultBehavior.Stop;
        }

        private void CanvasMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (curFigure != null)
            {
                startPoint = e.GetPosition(canvas);
            }
            else
            {
                Point position = e.GetPosition(canvas);
                hitList.Clear();
                VisualTreeHelper.HitTest(canvas, null, new HitTestResultCallback(HitTestResultHandler), new PointHitTestParameters(position) );
                if (hitList.Count > 0)
                {
          //          MessageBox.Show("You hit " + hitList.Count + " figures");
                    for (int i= canvas.Children.Count-1; i>=0; i--)
                    {
                        if (canvas.Children[i].Equals(hitList[0]))
                        {
                            MessageBox.Show(i.ToString());
                        }
                    }
                }
            }
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Figure>));
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = "C:\\Projects\\ООП\\CoolPaint",
                Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 0
            };
            if (saveFileDialog.ShowDialog() == true)
            {     
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, listOfFigures);
                }
            }
        }

        private void BLoad_Click(object sender, RoutedEventArgs e)
        {
            List<Figure> newList = new List<Figure>();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Figure>));
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = "C:\\Projects\\ООП\\CoolPaint",
                Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 0
            };
            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    try
                    {
                        newList = jsonFormatter.ReadObject(fs) as List<Figure>;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("JSON you loaded is corrupted and raised the following exception: " + ex.Message, "Error");
                    }
                }
            }

            foreach (Figure figure in newList)
            {
                figure.Draw();
                listOfFigures.Add(curFigure);
            }

        }

     /*   private void DeleteItem(object sender, List<> list, int index)
        {
            
        }*/  

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxOfFigures.SelectedItem != null)
            {
                int selectedIndex = ListBoxOfFigures.SelectedIndex;
////////////////////////////////////////////////////////////////////////////////FIX ME 
                canvas.Children.Remove(canvas.Children[selectedIndex]); 
       //         canvas.Children.Remove(canvas.Children[canvas.Children.Count - 1]);

                ListBoxOfFigures.Items.Remove(ListBoxOfFigures.Items[selectedIndex]);
         //       ListBoxOfFigures.Items.Remove(ListBoxOfFigures.Items[ListBoxOfFigures.Items.Count - 1]);


                listOfFigures.Remove(listOfFigures[selectedIndex]);
             }
        }

        private void BChange_Click(object sender, RoutedEventArgs e)
        {
            curFigure = null;
        }

    }

   
}
