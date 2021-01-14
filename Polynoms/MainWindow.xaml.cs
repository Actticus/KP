using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Polynoms
{
    ///Класс пользовательского интерфейса
    public partial class MainWindow
    {
        private readonly Button[] _buttons;
        private readonly Grid[] _grids;
        private Button _previousButton;

        /**
         * Создает пользовательское окно и инициализирует переменные
         * \return Пользовательское окно
         */
        public MainWindow()
        {
            InitializeComponent();
            _previousButton = Button0;
            _buttons = new[] {Button0, Button1, Button2, Button3};
            _grids = new[] {Grid0, Grid1, Grid2, Grid3};
        }

        ///Обрабатывает нажатие на одну из вкладок
        private void TabButtonClick(object sender, RoutedEventArgs e)
        {
            var currentButton = (Button) e.Source;
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

        ///Обрабатывает нажатие на кнопку закрытие приложения
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        ///Обрабатывает нажатие на кнопку сворачивания приложения
        private void TurnDown(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        ///Обрабатывает перенос окна
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        ///Обрабатывает нажатие на кнопку перевода полинома в строку
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

        ///Обрабатывает нажатие на кнопку перевода строки в полином
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

        ///Обрабатывает нажатие на кнопку вычисления значения полинома
        private void CalcStringButtonClick(object sender, RoutedEventArgs e)
        {
            var answerString = string.Empty;
            var answer = Calculator.CalculatePolynomValue(CalcPolStringTextBox.Text, CalcPolValueTextBox.Text);
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

        ///Обрабатывает нажатие на кнопку дифференцирования полинома
        private void DifferentialStringButtonClick(object sender, RoutedEventArgs e)
        {
            var answerString = string.Empty;
            var answer = Calculator.DifferentialStringCalculate(DiffPolTextBox.Text);
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
    }
}