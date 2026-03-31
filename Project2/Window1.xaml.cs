using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Project2
{
    public partial class Window1 : Window
    {
        private const string LOG_PATH = "C:\\Users\\User\\source\\repos\\Project2\\Project2\\WriterLog.txt";

        public ObservableCollection<Topic> Topics { get; set; }
        private Logger logger;

        public Window1()
        {
            InitializeComponent();
           
            Topics = new ObservableCollection<Topic>();
            topicList.ItemsSource = Topics;

            this.Closing += Window1_Closing;
        }

        private void fileDataBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы|*.txt";
            if (openFileDialog.ShowDialog() == false)
                return;

            string filePath = openFileDialog.FileName;
            if (Topics.Count > 0)
                Topics.Clear();

            try
            {
                using (StreamReader sReader = new(filePath, Encoding.UTF8))
                {
                    int c = 0;
                    logger = new(LOG_PATH);
                    string? line;
                    while ((line = sReader.ReadLine()) != null)
                    {
                        c++;
                        try
                        {
                            Topic? t = TopicParser.BuildTopicFromStrings(line);
                            Topics.Add(t);
                        }
                        catch(Exception ex) 
                        {
                            logger.WriteLogLine($"[{logger.Counter}] Ошибка в строке {c}: {ex.Message}. Строка: {line}");
                        }
                    }
                }

                MessageBox.Show($"Данные загружены! Добавлено элементов: {Topics.Count}",
                               "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Файл не найден: {filePath}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                logger?.Dispose();
            }
        }

        private void addDataBtn_Click(object sender, RoutedEventArgs e)
        {
            PopupStudentName.Text = "";
            PopupTopicName.Text = "";
            PopupDate.SelectedDate = DateTime.Today;

            Overlay.Visibility = Visibility.Visible;
            AddPopup.IsOpen = true;
        }

        private void PopupSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PopupStudentName.Text) ||
                string.IsNullOrWhiteSpace(PopupTopicName.Text) ||
                PopupDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            var newTopic = new Topic(
                PopupStudentName.Text.Trim(),
                PopupTopicName.Text.Trim(),
                DateOnly.FromDateTime(PopupDate.SelectedDate.Value)
            );

            Topics.Add(newTopic);

            
            AddPopup.IsOpen = false;
            Overlay.Visibility = Visibility.Collapsed;

            MessageBox.Show("Тема успешно добавлена!", "Успех",
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PopupCancel_Click(object sender, RoutedEventArgs e)
        {
            
            AddPopup.IsOpen = false;
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void deleteTopicBtn_Click(object sender, RoutedEventArgs e)
        {
            if (topicList.SelectedItem != null)
            {
               
                Topic selectedTopic = (Topic)topicList.SelectedItem;
                Topics.Remove(selectedTopic);
            }
            else
                MessageBox.Show("Выберите тему для удаления", "Предупреждение",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.workWindow = null;
            Close();
        }

        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
            MainWindow.workWindow = null;
    }
}