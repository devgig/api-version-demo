using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Demo.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

     
    }
}
