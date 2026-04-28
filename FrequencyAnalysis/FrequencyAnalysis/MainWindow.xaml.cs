using System.Windows;
using Microsoft.Win32;

namespace FrequencyAnalysis
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {   
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string t = textForm.Text;

            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Выберите файл";
            opf.Filter = "Текстовые файлы (*.txt)|*.txt";
            opf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool? result = opf.ShowDialog();
            if (result == false)
            {
                MessageBox.Show("Выберите текстовый файл");
                return;
            }

            FileTextHanlder fth = new FileTextHanlder(opf.FileName);
            MessageBox.Show(opf.FileName);

            TextHandler th = new TextHandler(t);
            var freqs = th.GetFreqs();

            var histogramWindow = new Diagram(freqs);
            histogramWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string t = textForm.Text;
            if (string.IsNullOrEmpty(t))
            {
                MessageBox.Show("Передана пустая строка");
                return;
            }
            TextHandler th = new TextHandler(t);

            var freqs = th.GetFreqs();
            var histogramWindow = new Diagram(freqs);
            histogramWindow.ShowDialog();
        }
    }
}