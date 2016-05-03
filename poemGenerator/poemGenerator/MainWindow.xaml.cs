using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ArrayList nouns = new ArrayList();
        public ArrayList verbs = new ArrayList();
        public ArrayList adjectives = new ArrayList();



        public MainWindow()
        {
            InitializeComponent();
            ReadDictionary();
        }
        private void ReadDictionary()
        {
            ReadAdjectives();
            ReadNouns();
            ReadVerbs();

        }
        private void SaveScript(string filename)
        {
            TextRange t = new TextRange(InputPoem.Document.ContentStart,
                                     InputPoem.Document.ContentEnd);
            File.WriteAllText(filename, t.Text);
        }
        private void ReadScript(string filename)
        {
            string[] ScriptText = File.ReadAllLines(filename);
            foreach (string line in ScriptText)
            {
                InputPoem.AppendText(line);
            }

        }
        private void ReadNouns()
        {
            var path = @"text\noun.txt";
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                var arr = s.Split(' ');
                foreach (string substring in arr)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        nouns.Add(substring);
                    }
                }

            }


        }
        private void ReadAdjectives()
        {
            var path = @"text\adjective.txt";
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                var arr = s.Split(' ');
                foreach (string substring in arr)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        adjectives.Add(substring);
                    }
                }
            }
        }
        private void ReadVerbs()
        {
            var path = @"text\verb.txt";
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                var arr = s.Split(' ');
                foreach (string substring in arr)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        verbs.Add(substring);
                    }
                }
            }

        }

        private void richTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {



        }

        private void outputPoem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            outputPoem.Document.Blocks.Clear();
            Random rnd = new Random();
            InputPoem.SelectAll();
            string[] lines = InputPoem.Selection.Text.Split('\n');
            foreach (string line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {


                    if (line[i] == '#')
                    {
                        i = i + 1;

                        if (line[i] == 'N')
                        {
                            int r = rnd.Next(nouns.Count);

                            outputPoem.AppendText((string)nouns[r] + " ");
                        }
                        else if (line[i] == 'A')
                        {
                            int r = rnd.Next(adjectives.Count);
                            outputPoem.AppendText((string)adjectives[r] + " ");
                        }
                        else if (line[i] == 'V')
                        {
                            int r = rnd.Next(verbs.Count);
                            outputPoem.AppendText((string)verbs[r] + " ");
                        }

                        else if (line[i - 1] == '#' && (line[i] == 'V' || line[i] == 'A' || line[i] == 'N'))
                        {

                        }



                    }
                    else
                    {
                        outputPoem.AppendText(line[i].ToString());
                    }


                }
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            InputPoem.Visibility = Visibility.Hidden;
            outputPoem.Visibility = Visibility.Hidden;
            GeneratePoem.Visibility = Visibility.Hidden;
            AddToDictionary.Visibility = Visibility.Visible;
            option.Visibility = Visibility.Visible;
            appendToDictionary.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            InputPoem.Visibility = Visibility.Visible;
            outputPoem.Visibility = Visibility.Visible;
            GeneratePoem.Visibility = Visibility.Visible;
            AddToDictionary.Visibility = Visibility.Hidden;
            option.Visibility = Visibility.Hidden;
            appendToDictionary.Visibility = Visibility.Hidden;

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
            else if (option.Text == "Adjective")
            {

                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (string line in lines)
                {
                    if (line != null) File.AppendAllText("text\\adjective.txt", line);
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            InputPoem.Document.Blocks.Clear();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog NewScript = new OpenFileDialog();
            NewScript.DefaultExt = ".txt";
            NewScript.Filter = "Text document  (*.txt)|*.txt";
            NewScript.ShowDialog();
            var filename = NewScript.FileName;
            ReadScript(filename);


        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            outputPoem.Document.Blocks.Clear();
            Random rnd = new Random();
            InputPoem.SelectAll();
            string[] lines = InputPoem.Selection.Text.Split('\n');
            foreach (string line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {


                    if (line[i] == '#')
                    {
                        i = i + 1;

                        if (line[i] == 'N')
                        {
                            int r = rnd.Next(nouns.Count);

                            outputPoem.AppendText((string)nouns[r] + " ");
                        }
                        else if (line[i] == 'A')
                        {
                            int r = rnd.Next(adjectives.Count);
                            outputPoem.AppendText((string)adjectives[r] + " ");
                        }
                        else if (line[i] == 'V')
                        {
                            int r = rnd.Next(verbs.Count);
                            outputPoem.AppendText((string)verbs[r] + " ");
                        }

                        else if (line[i - 1] == '#' && (line[i] == 'V' || line[i] == 'A' || line[i] == 'N'))
                        {

                        }



                    }
                    else
                    {
                        outputPoem.AppendText(line[i].ToString());
                    }


                }
            }

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            SaveFileDialog SaveScriptDialog = new SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text document  (*.txt)|*.txt"
            };
            SaveScriptDialog.ShowDialog();
            var filename = SaveScriptDialog.FileName;
            SaveScript(filename);

        }
    }

}
