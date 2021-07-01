using System.Windows;
using WPFUsingMVVM_Framework.ViewModel;

namespace WPFUsingMVVM_Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelUser();
        }
    }
}
