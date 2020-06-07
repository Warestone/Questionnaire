using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;
namespace Questionnaire
{
    /// <summary>
    /// Логика взаимодействия для About.xaml
    /// </summary>
    public partial class About : Window
    {       
        public About()
        {
            InitializeComponent();
        }        
        private void HyperlinkOne(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)); // Открытие ссылки в браузере
            e.Handled = true;
        }
        private void HyperlinkTwo(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)); // Открытие ссылки в браузере
            e.Handled = true;
        }       
    }
}
