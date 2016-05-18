using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;


namespace PoemGenerator
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public static DataTable DataTableFromTextFile(string location, char delimiter = ',')
{
    DataTable result;

    string[] LineArray = File.ReadAllLines(location);

    result = FormDataTable(LineArray, delimiter);

    return result;
}

private static DataTable FormDataTable(string[] LineArray, char delimiter)
{
    DataTable dt = new DataTable();

    AddColumnToTable(LineArray, delimiter, ref dt);

    AddRowToTable(LineArray, delimiter, ref dt);

    return dt;
}

private static void AddRowToTable(string[] valueCollection, char delimiter, ref DataTable dt)
{

    for (int i = 1; i < valueCollection.Length; i++)
    {
        string[] values = valueCollection[i].Split(delimiter);
        DataRow dr = dt.NewRow();
        for (int j = 0; j < values.Length; j++)
        {
            dr[j] = values[j];
        }
        dt.Rows.Add(dr);
    }
}

private static void AddColumnToTable(string[] columnCollection, char delimiter, ref DataTable dt)
{
    string[] columns = columnCollection[0].Split(delimiter);
    foreach (string columnName in columns)
    {
        DataColumn dc = new DataColumn(columnName, typeof(string));
        dt.Columns.Add(dc);
    }
}


public partial class MainWindow
    {
        private readonly Random _random = new Random();
        private ArrayList _nouns, _verbs, _adverbs, _prepositions, _adjectives, _tempnouns;

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
            if (String.isEmptyOrNull(filename)) return;
            var t = new TextRange(InputPoem.Document.ContentStart,
                InputPoem.Document.ContentEnd);
            File.WriteAllText(filename, t.Text);
        }

        private void ReadScript(string filename)
        {
            if (String.isEmptyOrNull(filename)) return;
            var scriptText = File.ReadAllLines(filename);
            foreach (var line in scriptText)
            {
                InputPoem.AppendText(line);
            }
        }

        private static ArrayList ReadWords(string filePath)
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

        private void AddToDictionaryMenuItemClick(object sender, RoutedEventArgs e)
        {
            InputPoem.Visibility = Visibility.Hidden;
            OutputPoem.Visibility = Visibility.Hidden;
            GeneratePoem.Visibility = Visibility.Hidden;
            AddToDictionary.Visibility = Visibility.Visible;
            Option.Visibility = Visibility.Visible;
            AppendToDictionary.Visibility = Visibility.Visible;
            editDictionary.Visibility = Visibility.Visible;


        }

        private void GeneratePoemItemClick1(object sender, RoutedEventArgs e)
        {
            InputPoem.Visibility = Visibility.Visible;
            OutputPoem.Visibility = Visibility.Visible;
            GeneratePoem.Visibility = Visibility.Visible;
            AddToDictionary.Visibility = Visibility.Hidden;
            Option.Visibility = Visibility.Hidden;
            AppendToDictionary.Visibility = Visibility.Hidden;
            editDictionary.Visibility = Visibility.Hidden;
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
                editDictionary.DataSource = Helper.DataTableFromTextFile("text\\noun.txt", "\n");

            }
            else if (Option.Text == "Verb")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\verb.txt", line);
                }
                editDictionary.DataSource = Helper.DataTableFromTextFile("text\\verb.txt", "\n");

            }

            else if (Option.Text == "Adverb")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\adverb.txt", line);
                }
                editDictionary.DataSource = Helper.DataTableFromTextFile("text\\adverb.txt", "\n");

            }

            else if (Option.Text == "Preposition")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\preposition.txt", line);
                }
                editDictionary.DataSource = Helper.DataTableFromTextFile("text\\preposition.txt", "\n");
               
            }

            else if (Option.Text == "Tempnoun")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\tempnoun.txt", line);
                }
                editDictionary.DataSource = Helper.DataTableFromTextFile("text\\tempnoun.txt", "\n");

            }

            else if (Option.Text == "Adjective")
            {
                AddToDictionary.SelectAll();

                var lines = AddToDictionary.Selection.Text.Split('\n');

                foreach (var line in lines)
                {
                    if (line != null) File.AppendAllText("text\\adjective.txt", line);
                }
                editDictionary.DataSource = Helper.DataTableFromTextFile("text\\adjective.txt", "\n");

            }

        }

        private void NewScriptMenuItemClick2(object sender, RoutedEventArgs e)
        {
            InputPoem.Document.Blocks.Clear();
        }

        private void QuitMenuItemClick4(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenScriptMenuItemClick5(object sender, RoutedEventArgs e)
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
            InputPoem.SelectAll();
            var lines = InputPoem.Selection.Text.Split('\n');
            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        i = i + 1;
                        switch (line[i])
                        {
                            case 'N':
                            {
                                var r = _random.Next(_nouns.Count);

                                OutputPoem.AppendText((string) _nouns[r] + " ");
                            }
                                break;
                            case 'A':
                            {
                                var r = _random.Next(_adjectives.Count);
                                OutputPoem.AppendText((string) _adjectives[r] + " ");
                            }
                                break;
                            case 'V':
                            {
                                var r = _random.Next(_verbs.Count);
                                OutputPoem.AppendText((string) _verbs[r] + " ");
                            }
                                break;
                            case 'D':
                            {
                                var r = _random.Next(_adverbs.Count);
                                OutputPoem.AppendText((string) _adverbs[r] + " ");
                            }
                                break;
                            case 'P':
                            {
                                var r = _random.Next(_prepositions.Count);
                                OutputPoem.AppendText((string) _prepositions[r] + " ");
                            }
                                break;
                            case 'T':
                            {
                                var r = _random.Next(_tempnouns.Count);
                                OutputPoem.AppendText((string) _tempnouns[r] + " ");
                            }
                                break;
                            default:
                                if (line[i - 1] == '#' &&
                                    (line[i] == 'V' || line[i] == 'A' || line[i] == 'N' || line[i] == 'D' ||
                                     line[i] == 'P' ||
                                     line[i] == 'T'))
                                {
                                }
                                break;
                        }
                    }
                    else
                    {
                        OutputPoem.AppendText(line[i].ToString());
                    }
                }
            }
        }

        private void SaveScriptMenuItemClick6(object sender, RoutedEventArgs e)
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