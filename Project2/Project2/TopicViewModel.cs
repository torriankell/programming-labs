using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Project2
{
    public class TopicViewModel : INotifyPropertyChanged
    {
        private const string LOG_PATH = "C:\\Users\\Man\\Desktop\\labs\\programming-labs\\Project2\\errors.log";

        private RelayCommand _loadTopicCommand;
        private RelayCommand _removeTopicCommand;
        private RelayCommand _openAddPopupCommand;
        private RelayCommand _saveNewTopicCommand;
        private RelayCommand _cancelAddCommand;
        private RelayCommand _handleFileWCommandsCommand;

        private Topic _topic;

        private bool _isAddPopupOpen;
        public bool IsAddPopupOpen
        {
            get => _isAddPopupOpen;
            set
            {
                _isAddPopupOpen = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OverlayVisibility));
            }
        }

        private Logger logger;

        private string _newStudentName;
        public string NewStudentName
        {
            get => _newStudentName;
            set { _newStudentName = value; OnPropertyChanged(); }
        }

        private string _newTopicName;
        public string NewTopicName
        {
            get => _newTopicName;
            set { _newTopicName = value; OnPropertyChanged(); }
        }

        private DateTime? _newDate;
        public DateTime? NewDate
        {
            get => _newDate;
            set { _newDate = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> LogEntries { get; set; }
        public ObservableCollection<Topic> Topics { get; set; }

        public Visibility OverlayVisibility => IsAddPopupOpen ? Visibility.Visible : Visibility.Collapsed;

        public RelayCommand CloseMainWindowCommand { get; set; }

        public RelayCommand HandleFileWCommandsCommand
        {
            get => _handleFileWCommandsCommand ?? (_handleFileWCommandsCommand = new RelayCommand(ExecuteHandleCommands));
        }
        public RelayCommand SaveNewTopicCommand
        {
            get => _saveNewTopicCommand ?? (_saveNewTopicCommand = new RelayCommand(ExecuteSaveNewTopic));
        }
        public RelayCommand CancelAddCommand
        {
            get => _cancelAddCommand ?? (_cancelAddCommand = new RelayCommand(ExecuteCancelAdd));
        }
        public RelayCommand OpenAddPopupCommand
        {
            get => _openAddPopupCommand ?? (_openAddPopupCommand = new RelayCommand(ExecuteOpenPopup));
        }

        public RelayCommand LoadTopicCommand
        {
            get => _loadTopicCommand ?? (_loadTopicCommand = new RelayCommand(ExecuteLoad));
        }

        public RelayCommand RemoveTopicCommand
        {
            get => _removeTopicCommand ?? (_removeTopicCommand = new RelayCommand(ExecuteDelete));
        }

        public Topic SelectedTopic
        {
            get
            {
                return _topic;
            }
            set
            {
                _topic = value;
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }

        private void ExecuteLoad(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы|*.txt";
            if (openFileDialog.ShowDialog() == false)
                return;

            string filePath = openFileDialog.FileName;
            if (Topics.Count > 0)
            {
                Topics.Clear();
                SelectedTopic = null;
            }

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
                        catch (Exception ex)
                        {
                            string error = $"Ошибка в строке {c}: {ex.Message}. Строка: {line}";
                            logger.WriteLogLine(error);
                            LogEntries.Add(error + '\n');
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

        private void ExecuteDelete(object parameter)
        {
            if (SelectedTopic != null)
            {
                Topics.Remove(SelectedTopic);
                SelectedTopic = null;
            }
            else
                MessageBox.Show("Выберите тему для удаления", "Предупреждение",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ExecuteOpenPopup(object parameter)
        {
            NewStudentName = "";
            NewTopicName = "";
            NewDate = DateTime.Today;
            IsAddPopupOpen = true;
        }

        private void ExecuteHandleCommands(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы команд|*.txt";

            if (openFileDialog.ShowDialog() == false)
                return;

            try
            {
                InstructionParser parser = new();
                List<IInstruction> insts = parser.ParseCommands(openFileDialog.FileName);

                foreach (IInstruction inst in insts)
                {
                    try
                    {
                        inst.Execute(this);
                    }
                    catch (Exception ex)
                    {
                        LogEntries.Add($"Ошибка при выполнении команды: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogEntries.Add($"Ошибка при парсинге файла команд: {ex.Message}");
                MessageBox.Show("Не удалось разобрать файл команд.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteSaveNewTopic(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewStudentName) ||
                string.IsNullOrWhiteSpace(NewTopicName) ||
                NewDate == null)
                {
                    MessageBox.Show("Заполните все поля", "Ошибка",
                                   MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


            Topic newTopic = new Topic(
                NewStudentName.Trim(),
                NewTopicName.Trim(),
                DateOnly.FromDateTime(NewDate.Value)
            );

            Topics.Add(newTopic);

            IsAddPopupOpen = false;

            MessageBox.Show("Тема успешно добавлена!", "Успех",
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteCloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                MainWindow.workWindow = null;
                window.Close();
            }
        }

        private void ExecuteCancelAdd(object parameter)
        {
            IsAddPopupOpen = false;
        }
        public TopicViewModel()
        {
            Topics = new ObservableCollection<Topic>();
            LogEntries = new ObservableCollection<string>();
            CloseMainWindowCommand = new RelayCommand(ExecuteCloseWindow);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
