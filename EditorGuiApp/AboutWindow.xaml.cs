using System.Windows;

namespace EditorGuiApp
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void OnOKClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
