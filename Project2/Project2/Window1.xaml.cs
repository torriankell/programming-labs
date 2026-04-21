using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Project2
{
    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
          
            DataContext = new TopicViewModel();

            this.Closing += Window1_Closing;
        }

        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
            MainWindow.workWindow = null;
    }
}