using System.Windows;

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
                </h1>
                <img src='C:\Users\User\source\repos\Project2\Project2\image.jpg'/>
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
