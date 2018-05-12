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
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using TriangleInterfaceLib;

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
            String LibName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "TriangleLib.dll");
            if (!File.Exists(LibName)) { Console.Write("File not found"); return; }
            ///Загружаем сборку
            Assembly libAssembly = Assembly.LoadFrom(LibName);
            BTriangle.IsEnabled = false;
            ///в цикле проходим по всем public-типам сборки
            foreach (Type t in libAssembly.GetExportedTypes())
            {
                if (t.IsClass && typeof(ITriangle).IsAssignableFrom(t))
                {
                    ///создаем объект полученного класса
                //    ITriangle triangle = (ITriangle)Activator.CreateInstance(t, canvas, color, startPoint, endPoint);
                    ///вызываем его метод GetAboutText
                    MessageBox.Show("!");
                    Button TriangleButton = new Button();
                    TriangleButton.Name = t.Name;
           //         typeList.Add(t);
           //         TriangleButton.Click +=
                    
                }
            }
        }

     /*   private void IsoTriangleButton_Click(object sender, RoutedEventArgs e)
        {
            curFigure = new  (canvas, color, startPoint, endPoint);
        }
        */
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

        private void DrawAnyFigure(Figure curFigure, Point startPoint, Point endPoint)
        {
            curFigure = (Figure)Activator.CreateInstance(curFigure.GetType(), canvas, color, startPoint, endPoint);
            curFigure.Draw(canvas);
            listOfFigures.Add(curFigure);
            ListBoxItem item = new ListBoxItem();
            item.Content = curFigure.typeName;
            ListBoxOfFigures.Items.Add(item);
        }

        private void CanvasMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            endPoint = e.GetPosition(CanvasMain);
            if (curFigure != null)
            {
                DrawAnyFigure(curFigure, startPoint, endPoint);
            }
            else
            {
                MoveFigure(listOfFigures[ListBoxOfFigures.SelectedIndex], startPoint, endPoint);
            }
        }

        private void MoveFigure(Figure figure, Point startPoint, Point endPoint)
        {
            figure = listOfFigures[ListBoxOfFigures.SelectedIndex];
            figure.startPoint.X += endPoint.X - startPoint.X;
            figure.startPoint.Y += endPoint.Y - startPoint.Y;
            figure.endPoint.X += endPoint.X - startPoint.X;
            figure.endPoint.Y += endPoint.Y - startPoint.Y;
            figure.Draw(canvas);
        }
    
        private void CanvasMain_MouseDown(object sender, MouseButtonEventArgs e)
        { 
            startPoint = e.GetPosition(canvas);
            
            if (curFigure == null)
            {
                Point position = e.GetPosition(canvas);
                hitList.Clear();
                VisualTreeHelper.HitTest(canvas, null, new HitTestResultCallback(HitTestResultHandler), new PointHitTestParameters(position) );
                if (hitList.Count > 0)
                {
                    for (int i= canvas.Children.Count-1; i>=0; i--)
                    {
                        if (canvas.Children[i].Equals(hitList[0]))
                        {
                            ListBoxOfFigures.SelectedIndex = i;
                        }
                    }
                }
            }
        }

        readonly JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
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
                    var json = JsonConvert.SerializeObject(listOfFigures, Formatting.None, settings);
                    var writeStream = new StreamWriter(fs);
                    writeStream.Write(json);
                    writeStream.Flush();
                }
            }
        }

        private void BLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = "C:\\Projects\\ООП\\CoolPaint",
                Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 0
            };
            if (openFileDialog.ShowDialog() == true)
            {
                using (var fStream = File.OpenRead(openFileDialog.FileName))
                {
                    var json = new StreamReader(fStream).ReadToEnd();
                    var figures = JsonConvert.DeserializeObject<List<Figure>>(json, settings);
                    foreach (Figure figure in figures)
                    {
                        //   Figure nextFigure;
                        //   nextFigure = (Figure)Activator.CreateInstance(figure.GetType(), canvas, figure.color, figure.startPoint, figure.endPoint);
                        //   nextFigure.Draw(canvas);
                        /*
                        figure.Draw(canvas);    
                        listOfFigures.Add(figure);
                        ListBoxOfFigures.Items.Add(figure.typeName);*/
                        DrawAnyFigure(figure, figure.startPoint, figure.endPoint);
                    }
                }
            }
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxOfFigures.SelectedItem != null)
            {
                int selectedIndex = ListBoxOfFigures.SelectedIndex;
                canvas.Children.Remove(canvas.Children[selectedIndex]); 
                ListBoxOfFigures.Items.Remove(ListBoxOfFigures.Items[selectedIndex]);
                listOfFigures.Remove(listOfFigures[selectedIndex]);
             }
        }

      

        private void BChange_Click(object sender, RoutedEventArgs e)
        {
            curFigure = null;
        }

    }

   
}
