using System.Windows;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Code39Alg code39Alg = new Code39Alg(symbolsTBox.Text);
            try
            {
                code39Alg.GetCode(BarcodeCanvas);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}