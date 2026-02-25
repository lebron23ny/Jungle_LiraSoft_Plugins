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

namespace Jungle_LS_ColumnarFoundation.Windows
{
    /// <summary>
    /// Логика взаимодействия для FoundationWindow.xaml
    /// </summary>
    public partial class FoundationWindow : Window
    {
        ViewModel.ViewModelFoundation viewModel;

        Action<double, double, double, double, double, double, string, bool> _calculate;
        Action<double, double, double, double> _elasticBasicParam;
        public FoundationWindow(
            Action<double, double, double, double, double, double, string, bool> calculate,
            Action<double, double, double, double> elasticBasicParam)
        {
            InitializeComponent();
            viewModel = new ViewModel.ViewModelFoundation();
            this.DataContext = viewModel;
            this._calculate = calculate;
            this._elasticBasicParam = elasticBasicParam;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _calculate(viewModel.Width1, viewModel.Width2, viewModel.ThickPlate, viewModel.Height, viewModel.B1, viewModel.B2, viewModel.ClassConcrete, true);
            _elasticBasicParam(viewModel.C1, viewModel.C2, viewModel.Cx, viewModel.Cy);
            this.Close();
        }

        
    }
}
