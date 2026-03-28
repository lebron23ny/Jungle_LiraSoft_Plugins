using Jungle_LS_SetDescriptionNode.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jungle_LS_SetDescriptionNode.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _viewModel = new MainViewModel();

        Action<string> _action;
        public MainWindow(Action<string> action)
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
            this._action = action;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _action(_viewModel.Description);
            this.Close();
        }
    }
}
