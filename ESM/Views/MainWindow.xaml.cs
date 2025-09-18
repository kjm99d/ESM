using System.Windows;
using ESM.ViewModels;

namespace ESM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();  // 연결 완료
        }
    }
}
