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
using System.Xml.Linq;
using System.Xml;

namespace GraphicEditor
{
   public partial class MainWindow : Window
    {
        protected List<Figure> listOfFigures = new List<Figure>();
        public List<Visual> hitList;
        private List<Figure> additionalFigList, basicFigures;
        public Figure curFigure;
        public Color color = Color.FromRgb(0,0,0);
        protected Point startPoint, endPoint;
        public Canvas canvas;
        private string themeColor, lang;
        private const string configPass = "C:\\all you need is\\to study\\ООП\\wpf\\GraphicEditor-WPF\\GraphicEditor\\App.config";
        public Dictionary<string, Color> themes = new Dictionary<string, Color>();

        public MainWindow()
        {
            InitializeComponent();
            themes.Add("gray", Color.FromRgb(200, 200, 200));
            themes.Add("blue", Color.FromRgb(202, 225, 250));
            themes.Add("pink", Color.FromRgb(243, 225, 250));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            basicFigures = new List<Figure>();
            curFigure = new Line(canvas, color, startPoint, endPoint);
            canvas = CanvasMain;
            hitList = new List<Visual>();
            additionalFigList = new List<Figure>();

            ReadConfig();
            DrawBasicButtons();

            Background = new SolidColorBrush(themes[themeColor]);        
        }

        private void DrawBasicButtons ()
        {
            string nspace = "GraphicEditor";
            IEnumerable<Type> Types = from t in Assembly.GetExecutingAssembly().GetTypes()
                                      where t.IsClass && t.Namespace == nspace && t.IsSubclassOf(typeof(Figure))
                                      select t;
            foreach (Type t in Types)
            {
                Figure figureType = (Figure)Activator.CreateInstance(t, canvas, color, startPoint, endPoint);
                basicFigures.Add((Figure)figureType);
                Button figureButton = new Button();
                figureButton = figureType.MakeButton(lang);
                figureButton.Click += FigureButton_Click;
                ShapesPanel.Children.Add(figureButton);
            }
        }

        public void ReadConfig()
        {
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.Load(configPass);
                XmlNode node = xd.SelectSingleNode("//setting[@name = 'Theme']");
                if (node != null)
                    themeColor = node.InnerText;
                node = xd.SelectSingleNode("//setting[@name = 'Language']");
                if (node != null)
                    lang = node.InnerText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                lang = "en";
                themeColor = "gray";
            }
            ChangeLanguage();
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            Background = new SolidColorBrush(themes[themeColor]);
        }

            public void ChangeLanguage()
        {
            switch (lang)
            {
                case "en":
                    BClear.Content = "Clear";
                    BChange.Content = "Move";
                    BSave.Content = "Save";
                    BLoad.Content = "Load";
                    BRemove.Content = "Remove";
                    BLoadFigures.Content = "Load figures";
                    break;
                case "ru":
                    BClear.Content = "Очистить";
                    BChange.Content = "Передвинуть";
                    BSave.Content = "Сохранить";
                    BLoad.Content = "Загрузить";
                    BRemove.Content = "Удалить";
                    BLoadFigures.Content = "Загрузить фигуры";
                    break;
            }
        }

        private void FigureButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < basicFigures.Count; i++)
            {
                if ((sender as Button).Name == "B" + basicFigures[i].typeName)
                {
                    curFigure = basicFigures[i];
                }
            }
        }

        private void BLoadFigures_Click(object sender, RoutedEventArgs e)
        {
            additionalFigList = new List<Figure>();
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "dll Files (*.dll)|*.dll|All files (*.*)|*.*",
                FilterIndex = 0
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Assembly libAssembly = Assembly.LoadFrom(openFileDialog.FileName);
                //BTriangle.IsEnabled = false;
                //BTriangle.Content = "UnLoad triangles";
                foreach (Type t in libAssembly.GetExportedTypes())
                {
                    if (t.IsClass && typeof(ITriangle).IsAssignableFrom(t))
                    {
                        Figure triangle = (Figure)Activator.CreateInstance(t, canvas, color, startPoint, endPoint);
                        additionalFigList.Add((Figure)triangle);
                        Button triangleButton = new Button();
                        triangleButton = triangle.MakeButton(lang);
                        triangleButton.Click += TriangleButton_Click;
                        ShapesPanel.Children.Add(triangleButton);
                    }
                }
            }
        }

        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i=0; i<additionalFigList.Count; i++)
            {
                if ((sender as Button).Name == "B"+additionalFigList[i].typeName)
                {
                    curFigure = additionalFigList[i];
                }
            }
        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            listOfFigures.Clear();
            canvas.Children.Clear();
            ListBoxOfFigures.Items.Clear();
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

        private void DrawAnyFigure(Figure curFigure, Color color, Point startPoint, Point endPoint)
        {
            curFigure = (Figure)Activator.CreateInstance(curFigure.GetType(), canvas, color, startPoint, endPoint);
            curFigure.Draw(canvas);
            listOfFigures.Add(curFigure);
            ListBoxItem item = new ListBoxItem();
            if (lang == "ru")
                item.Content = curFigure.typeNameRu;
            else
                item.Content = curFigure.typeName;
            ListBoxOfFigures.Items.Add(item);
        }

        private void CanvasMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            endPoint = e.GetPosition(CanvasMain);
            if (curFigure != null)
            {
                DrawAnyFigure(curFigure, color, startPoint, endPoint);
            }
            else
            {
                try
                {
                    MoveFigure(listOfFigures[ListBoxOfFigures.SelectedIndex], startPoint, endPoint);
                }
                catch
                {
                    MessageBox.Show("No shape found");
                }
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
                Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 0
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    var json = JsonConvert.SerializeObject(listOfFigures, Newtonsoft.Json.Formatting.None, settings);
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
                        DrawAnyFigure(figure, figure.color, figure.startPoint, figure.endPoint);
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

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            GraphicEditor.Settings settings = new GraphicEditor.Settings();
            settings.Owner = this;
            settings.ShowDialog();
            ReadConfig();
            for (int i = basicFigures.Count + additionalFigList.Count; i > 0; i--)
            {
                ShapesPanel.Children.Remove(ShapesPanel.Children[i]);
            }
            foreach (Figure fig in basicFigures)
            {
                Button figureButton = new Button();
                figureButton = fig.MakeButton(lang);
                figureButton.Click += FigureButton_Click;
                ShapesPanel.Children.Add(figureButton);
            }
            foreach (Figure fig in additionalFigList)
            {
                Button figureButton = new Button();
                figureButton = fig.MakeButton(lang);
                figureButton.Click += FigureButton_Click;
                ShapesPanel.Children.Add(figureButton);
            }
      /*      for (int i = 0; i < ListBoxOfFigures.Items.Count; i++)
            {
                if (lang == "en")
                    ListBoxOfFigures.Items[i] = 
            }*/
        }

        private void BChange_Click(object sender, RoutedEventArgs e)
        {
            curFigure = null;
        }

    }

   
}
