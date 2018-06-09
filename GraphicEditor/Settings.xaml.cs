using System.Configuration;
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
using System.Xml.Linq;
using Microsoft.Win32;

namespace GraphicEditor
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
    //    string sAttr;
        public string themeColor;
        private XmlDocument xd;
        private string language;
        private const string configPass = "C:\\all you need is\\to study\\ООП\\wpf\\GraphicEditor-WPF\\GraphicEditor\\App.config";

        public Settings()
        {
            InitializeComponent();
            xd = new XmlDocument();
            xd.Load(configPass);
        }

        private void Gray_Checked(object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromRgb(200, 200, 200));
            themeColor = "gray";
        }

        private void Blue_Checked(object sender, RoutedEventArgs e)
        {
            themeColor = "blue";
            this.Background = new SolidColorBrush(Color.FromRgb(202, 225, 250));
        }

        private void Pink_Checked(object sender, RoutedEventArgs e)
        {
            themeColor = "pink";
            this.Background = new SolidColorBrush(Color.FromRgb(243, 225, 250));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            XmlNode node = xd.SelectSingleNode("//setting[@name = 'Theme']");
            if (node != null)
                node.InnerText = themeColor;
            node = xd.SelectSingleNode("//setting[@name = 'Language']");
            if (node != null)
                node.InnerText = language;
            xd.Save(configPass);
            this.Close();
        }

        private void RBEnLang_Click(object sender, RoutedEventArgs e)
        {
            language = "en";
        }

        private void RBRuLang_Click(object sender, RoutedEventArgs e)
        {
            language = "ru";
        }
    }
}
