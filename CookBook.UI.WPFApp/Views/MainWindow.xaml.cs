using System.Globalization;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace CookBook.UI.WPFApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.CurrentUICulture;
            InitializeComponent();
        }
    }
}
