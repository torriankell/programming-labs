using System.Windows;

namespace Project2
{
    public partial class MainWindow : Window
    {

        public static Window1 workWindow = null;
        public static Window2 helpWindow = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void workBtn_Click(object sender, RoutedEventArgs e)
        {
            if (workWindow == null)
            {
                workWindow = new Window1();
                workWindow.Show();
            }
        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (helpWindow == null)
            {
                helpWindow = new Window2();
                helpWindow.Show();
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}