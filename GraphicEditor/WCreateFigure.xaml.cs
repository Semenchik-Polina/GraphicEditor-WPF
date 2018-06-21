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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace GraphicEditor
{
    /// <summary>
    /// Логика взаимодействия для WCreateFigure.xaml
    /// </summary>
    public partial class WCreateFigure : Window
    {
        private string themeColor;
        private XmlDocument xd;
        private string language;
        private const string configPass = "C:\\all you need is\\to study\\ООП\\wpf\\GraphicEditor-WPF\\GraphicEditor\\App.config";
        private MainWindow mainWindow;

        public WCreateFigure()
        {
            InitializeComponent();
        }

        private void ChangeLanguage()
        {
            switch (language)
            {
                case "en":
                    LNameRu.Content = "Russian name";
                    LNameEn.Content = "English name";
                    BAdd.Content = "Add figure";
                    break;
                case "ru":
                    LNameRu.Content = "Русское название";
                    LNameEn.Content = "Английское название";
                    BAdd.Content = "Добавить фигуру";
                    break;
            }
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBNameEn.Text != "" && TBNameRu.Text != "")
                BAdd.IsEnabled = true;
            else
                BAdd.IsEnabled = false;
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.compFigNameEn = TBNameEn.Text;
            mainWindow.compFigNameRu = TBNameRu.Text;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)this.Owner;
            xd = new XmlDocument();
            try
            {
                xd.Load(configPass);
                XmlNode node = xd.SelectSingleNode("//setting[@name = 'Theme']");
                if (node != null && mainWindow.themes.ContainsKey(node.InnerText))
                    themeColor = node.InnerText;
                else
                    themeColor = "gray";
                node = xd.SelectSingleNode("//setting[@name = 'Language']");
                if (node != null && (node.InnerText == "ru" ^ node.InnerText == "en"))
                    language = node.InnerText;
                else
                    language = "en";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                language = "en";
                themeColor = "gray";
            }
            ChangeLanguage();
            Background = new SolidColorBrush(mainWindow.themes[themeColor]);
        }
    }
}
