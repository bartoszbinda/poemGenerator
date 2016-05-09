using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace PoemGenerator
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ArrayList _nouns, _verbs, _adverbs, _prepositions, _adjectives, _tempnouns;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            ReadDictionary();
        }

        private void ReadDictionary()
        {
            _nouns = ReadWords(@"text\noun.txt");
            _verbs = ReadWords(@"text\verb.txt");
            _adverbs = ReadWords(@"text\adverb.txt");
            _prepositions = ReadWords(@"text\preposition.txt");
            _adjectives = ReadWords(@"text\adjective.txt");
            _tempnouns = ReadWords(@"text\tempnoun.txt");
        }

        private void SaveScript(string filename)
        {
            var t = new TextRange(InputPoem.Document.ContentStart,
                InputPoem.Document.ContentEnd);
            File.WriteAllText(filename, t.Text);
        }

        private void ReadScript(string filename)
        {
            var scriptText = File.ReadAllLines(filename);
            foreach (var line in scriptText)
            {
                InputPoem.AppendText(line);
            }
        }

        private ArrayList ReadWords(string filePath)
        {
            var wordsForDictionary = new ArrayList();
            var readText = File.ReadAllLines(filePath);
            foreach (var s in readText)
            {
                var arr = s.Split(' ');
                foreach (var substring in arr)
                {
                    if (!string.IsNullOrEmpty(substring))
                    {
                        wordsForDictionary.Add(substring);
                    }
                }
            }
            return wordsForDictionary;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            InputPoem.Visibility = Visibility.Hidden;
            OutputPoem.Visibility = Visibility.Hidden;
            GeneratePoem.Visibility = Visibility.Hidden;
            AddToDictionary.Visibility = Visibility.Visible;
            Option.Visibility = Visibility.Visible;
            AppendToDictionary.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            InputPoem.Visibility = Visibility.Visible;
            OutputPoem.Visibility = Visibility.Visible;
            GeneratePoem.Visibility = Visibility.Visible;
            AddToDictionary.Visibility = Visibility.Hidden;
            Option.Visibility = Visibility.Hidden;
            AppendToDictionary.Visibility = Visibility.Hidden;
        }

        private void appendToDictionary_Click(object sender, RoutedEventArgs e)
        {
            if (Option.Text == "Noun")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\noun.txt", line);
                }
            }
            else if (Option.Text == "Verb")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\verb.txt", line);
                }
            }

            else if (Option.Text == "Adverb")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\adverb.txt", line);
                }
            }

            else if (Option.Text == "Preposition")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\preposition.txt", line);
                }
            }

            else if (Option.Text == "Tempnoun")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\tempnoun.txt", line);
                }
            }

            else if (Option.Text == "Adjective")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\adjective.txt", line);
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            InputPoem.Document.Blocks.Clear();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var newScript = new OpenFileDialog();
            newScript.DefaultExt = ".txt";
            newScript.Filter = "Text document  (*.txt)|*.txt";
            newScript.ShowDialog();
            var filename = newScript.FileName;
            ReadScript(filename);
        }

        private void GeneratePoemButtonClick1(object sender, RoutedEventArgs e)
        {
            OutputPoem.Document.Blocks.Clear();
            var random = new Random();
            InputPoem.SelectAll();
            var lines = InputPoem.Selection.Text.Split('\n');
            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        i = i + 1;


                        if (line[i] == 'N')
                        {
                            var r = random.Next(_nouns.Count);

                            OutputPoem.AppendText((string) _nouns[r] + " ");
                        }
                        else if (line[i] == 'A')
                        {
                            var r = random.Next(_adjectives.Count);
                            OutputPoem.AppendText((string) _adjectives[r] + " ");
                        }
                        else if (line[i] == 'V')
                        {
                            var r = random.Next(_verbs.Count);
                            OutputPoem.AppendText((string) _verbs[r] + " ");
                        }
                        else if (line[i] == 'D')
                        {
                            var r = random.Next(_adverbs.Count);
                            OutputPoem.AppendText((string) _adverbs[r] + " ");
                        }
                        else if (line[i] == 'P')
                        {
                            var r = random.Next(_prepositions.Count);
                            OutputPoem.AppendText((string) _prepositions[r] + " ");
                        }
                        else if (line[i] == 'T')
                        {
                            var r = random.Next(_tempnouns.Count);
                            OutputPoem.AppendText((string) _tempnouns[r] + " ");
                        }

                        else if (line[i - 1] == '#' &&
                                 (line[i] == 'V' || line[i] == 'A' || line[i] == 'N' || line[i] == 'D' || line[i] == 'P' ||
                                  line[i] == 'T'))
                        {
                        }
                    }
                    else
                    {
                        OutputPoem.AppendText(line[i].ToString());
                    }
                }
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var saveScriptDialog = new SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text document  (*.txt)|*.txt"
            };
            saveScriptDialog.ShowDialog();
            var filename = saveScriptDialog.FileName;
            SaveScript(filename);
        }
    }
}
