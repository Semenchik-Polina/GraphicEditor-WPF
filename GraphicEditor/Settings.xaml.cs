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
        private string themeColor;
        private XmlDocument xd;
        private string language;
        private const string configPass = "C:\\all you need is\\to study\\ООП\\wpf\\GraphicEditor-WPF\\GraphicEditor\\App.config";
        MainWindow mainWindow;

        public Settings()
        {
            InitializeComponent();
            mainWindow = new MainWindow();
            xd = new XmlDocument();
            try
            {
                xd.Load(configPass);
                XmlNode node = xd.SelectSingleNode("//setting[@name = 'Theme']");
                if (node != null)
                    themeColor = node.InnerText;
                node = xd.SelectSingleNode("//setting[@name = 'Language']");
                if (node != null)
                    language = node.InnerText;
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

        private void Gray_Checked(object sender, RoutedEventArgs e)
        {
            themeColor = "gray";
            Background = new SolidColorBrush(mainWindow.themes[themeColor]);
        }

        private void Blue_Checked(object sender, RoutedEventArgs e)
        {
            themeColor = "blue";
            Background = new SolidColorBrush(mainWindow.themes[themeColor]);
        }

        private void Pink_Checked(object sender, RoutedEventArgs e)
        {
            themeColor = "pink";
            Background = new SolidColorBrush(mainWindow.themes[themeColor]);
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
            ChangeLanguage();
        }

        private void RBRuLang_Click(object sender, RoutedEventArgs e)
        {
            language = "ru";
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            switch (language)
            {
                case "en":
                    RBBlue.Content = "Blue";
                    RBGray.Content = "Gray";
                    RBPink.Content = "Pink";
                    LTheme.Content = "Theme";
                    LLanguage.Content = "Language";
                    BSave.Content = "Save";
                    break;
                case "ru":
                    RBBlue.Content = "Синяя";
                    RBGray.Content = "Серая";
                    RBPink.Content = "Розовая";
                    LTheme.Content = "Тема";
                    LLanguage.Content = "Язык";
                    BSave.Content = "Сохранить";
                    break;
            }
        }
    }
}
