using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        public SaveWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new MainWindow();
            window1.Show();
            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void AddToDictionary_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void appendToDictionary_Click(object sender, RoutedEventArgs e)
        {
            if (option.Text == "Noun")
            {


                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (string line in lines)
                {
                    if (line != null) File.AppendAllText("text\\noun.txt", line);
                }

            }
            else if (option.Text == "Verb")
            {

                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (string line in lines)
                {
                    if (line != null) File.AppendAllText("text\\verb.txt", line);
                }


            }
            else if(option.Text == "Adjective")
            {

                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (string line in lines)
                {
                    if (line != null) File.AppendAllText("text\\adjective.txt", line);
                }
            }

        }
    }
}
