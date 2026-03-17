using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project2
{
    public partial class Window2 : Window
    {
 
        public Window2()
        {
            InitializeComponent();
            string htmlString = """
                <html>
                <body>
                <h1>
                    Test html-page
                    <img src='C:\Users\User\source\repos\Project2\Project2\image.jpg'/>
                </h1>
                </body>
                </html>
                """;
            MyWebBrowser.NavigateToString(htmlString);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.helpWindow = null;
            Close();
        }
    }
}
