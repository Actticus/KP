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
        private readonly Button[] _buttons;
        private readonly Grid[] _grids;
        private Button _previousButton;

        public MainWindow()
        {
            InitializeComponent();
            _previousButton = Button0;
            _buttons = new[] { Button0, Button1, Button2, Button3 };
            _grids = new[] { Grid0, Grid1, Grid2, Grid3 };
        }

        private void StringToPolynomCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            var answerString = string.Empty;
            var answer = Calculator.StringToPolynomConvert(StrToPolTextBox.Text);
            switch (answer.Code)
            {
                case Calculator.AnswerCode.Error:
                    AnswerLabel.Foreground = new SolidColorBrush(Colors.Red);
                    answerString = "Ошибка: ";
                    break;
                case Calculator.AnswerCode.Ok:
                    AnswerLabel.Foreground = new SolidColorBrush(Colors.Black);
                    answerString = "Ответ: ";
                    break;
                case Calculator.AnswerCode.Warning:
                    AnswerLabel.Foreground = new SolidColorBrush(Colors.Yellow);
                    answerString = "Предупреждение: ";
                    break;
            }
            answerString += answer.Ans;
            AnswerLabel.Content = answerString;
        }

        private void DifferentialStringButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PolynomToStringCalculateButtonClick(object sender, RoutedEventArgs e)
        {
            var answerString = string.Empty;
            var answer = Calculator.PolynomToStringConvert(PolToStrTextBox.Text);
            switch (answer.Code)
            {
                case Calculator.AnswerCode.Error:
                    AnswerLabel.Foreground = new SolidColorBrush(Colors.Red);
                    answerString = "Ошибка: ";
                    break;
                case Calculator.AnswerCode.Ok:
                    AnswerLabel.Foreground = new SolidColorBrush(Colors.Black);
                    answerString = "Ответ: ";
                    break;
                case Calculator.AnswerCode.Warning:
                    AnswerLabel.Foreground = new SolidColorBrush(Colors.Yellow);
                    answerString = "Предупреждение: ";
                    break;
            }

            answerString += answer.Ans;
            AnswerLabel.Content = answerString;
        }

        private void CalcStringButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TabButtonClick(object sender, RoutedEventArgs e)
        {
            var currentButton = (Button)e.Source;
            var blueBrush = new SolidColorBrush(Color.FromRgb(7, 78, 232));
            var blackBrush = new SolidColorBrush(Colors.Black);

            AnswerLabel.Content = string.Empty;

            _previousButton.Foreground = blackBrush;
            currentButton.Foreground = blueBrush;

            GridCursor.Width = currentButton.Width;
            var margin = _buttons.TakeWhile(butt => butt != currentButton).Sum(butt => butt.Width);
            GridCursor.Margin = new Thickness(10 + margin, 0, 0, 0);

            _grids[int.Parse(_previousButton.Uid)].Visibility = Visibility.Collapsed;
            _grids[int.Parse(currentButton.Uid)].Visibility = Visibility.Visible;

            _previousButton = currentButton;
            AnswerLabel.Content = string.Empty;
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
