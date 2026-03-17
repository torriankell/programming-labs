using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace Project2
{
    public partial class Window1 : Window
    {
        public ObservableCollection<Topic> Topics { get; set; }

        public Window1()
        {
            InitializeComponent();
            Topics = new ObservableCollection<Topic>();
            topicList.ItemsSource = Topics;
        }

        private void fileDataBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\User\\source\\repos\\Project2\\Project2\\data.txt";
            if (Topics.Count > 0)
                Topics.Clear();

            try
            {
                using (StreamReader sReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = sReader.ReadLine()) != null)
                    {
                        Topic t = Topic.CreateObjectFromString(line);
                        if (t != null)
                            Topics.Add(t);
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
            {
                MessageBox.Show("Выберите тему для удаления", "Предупреждение",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.workWindow = null;
            Close();
        }
    }
}