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

namespace Polynoms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StringToPolynomCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DifferentialStringButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PolynomToStringCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CalcStringButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TabButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TurnDown(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
