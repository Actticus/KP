using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Polynoms
{
    ///Класс, отвечающий за токены и перевод строки в токены
    public abstract class Tokener
    {
        ///Набор состояний токенера
        private enum TokenerState
        {
            Start,
            Error,
            End,
            Operation,
            Operand,
            OperandAftDot,
            Polynom,
            Warning,
            Dot
        }

        ///Набор типов токена
        public enum TokenType
        {
            Operation = 0,
            Number = 1,
            Variable = 2
        }

        ///Значение и тип токена
        public struct Token
        {
            public string Value;
            public TokenType Type;
        }

        /**
         * Проверяет, является ли символ переменной
         * \param v Переменная, с которой необходимо сравнивать
         * \param z Символ, который необходимо проверять
         * \return True, если z является переменной и False, если нет
         */
        private static bool IsVar(char? v, char z)
        {
            return v == null || v == z;
        }

        /**
         * Проверяет, является ли символ операцией
         * \param z Символ, который необходимо проверять
         * \return True, если z является операцией и False, если нет
         */
        private static bool IsOp(char z)
        {
            return z == '+' || z == '-' || z == '*' || z == '/' || z == '^';
        }

        /**
         * Проверяет, является ли символ переменной
         * \param e Строка, которую необходимо перевести в токены
         * \return Список токенов
         */
        public static List<Token> TokenizeString(string e)
        {
            var result = new List<Token>();
            var state = TokenerState.Start;
            var errorMessage = string.Empty;
            char? pol = null;
            var operand = "";
            foreach (var z in e)
            {
                var oldstate = state;
                switch (state)
                {
                    case TokenerState.Start:
                    {
                        if (char.IsDigit(z) || z == '-')
                        {
                            state = TokenerState.Operand;
                        }
                        else if (z == '$')
                        {
                            errorMessage = "Введена пустая строка";
                            state = TokenerState.Warning;
                        }
                        else if (char.IsLetter(z))
                        {
                            pol = z;
                            state = TokenerState.Polynom;
                        }
                        else if (IsOp(z))
                        {
                            errorMessage = "В начале строки был обнаружен оператор или неизвестный символ";
                            state = TokenerState.Error;
                        }
                        else
                        {
                            errorMessage = "В начале строки был обнаружен неизвестный символ";
                            state = TokenerState.Error;
                        }

                        break;
                    }
                    case TokenerState.Operation:
                    {
                        if (char.IsDigit(z))
                        {
                            state = TokenerState.Operand;
                        }
                        else if (char.IsLetter(z) && IsVar(pol, z))
                        {
                            pol = z;
                            state = TokenerState.Polynom;
                        }
                        else if (IsOp(z))
                        {
                            errorMessage = "В строке обнаружены два оператора подряд";
                            state = TokenerState.Error;
                        }
                        else
                        {
                            errorMessage = "В строке обнаружен неизвестный символ после оператора";
                            state = TokenerState.Error;
                        }

                        break;
                    }
                    case TokenerState.Operand:
                    {
                        if (char.IsDigit(z))
                        {
                            //pass
                        }
                        else if (z == '.')
                        {
                            state = TokenerState.Dot;
                        }
                        else if (IsOp(z))
                        {
                            state = TokenerState.Operation;
                        }
                        else if (z == '$')
                        {
                            state = TokenerState.End;
                        }
                        else if (char.IsLetter(z) && IsVar(pol, z))
                        {
                            errorMessage = "После числа был обнаружен полином без операции между ними";
                            state = TokenerState.Error;
                        }
                        else
                        {
                            errorMessage = "В строке обнаружен неизвестный символ после числа";
                            state = TokenerState.Error;
                        }

                        break;
                    }
                    case TokenerState.OperandAftDot:
                    {
                        if (char.IsDigit(z))
                        {
                            //pass
                        }
                        else if (IsOp(z))
                        {
                            state = TokenerState.Operation;
                        }
                        else if (z == '$')
                        {
                            state = TokenerState.End;
                        }
                        else if (z == '.')
                        {
                            errorMessage = "В строке обнаружена точка после дробного числа";
                            state = TokenerState.Error;
                        }
                        else if (char.IsLetter(z) && IsVar(pol, z))
                        {
                            errorMessage = "В строке обнаружен полином после дробного числа";
                            state = TokenerState.Error;
                        }
                        else
                        {
                            errorMessage = "В строке обнаружен неизвестный символ после дробного числа";
                            state = TokenerState.Error;
                        }

                        break;
                    }
                    case TokenerState.Dot:
                    {
                        if (char.IsDigit(z))
                        {
                            state = TokenerState.OperandAftDot;
                        }
                        else
                        {
                            errorMessage = "После точки был обнаружен не числовой символ";
                            state = TokenerState.Error;
                        }

                        break;
                    }
                    case TokenerState.Polynom:
                    {
                        if (IsOp(z))
                        {
                            state = TokenerState.Operation;
                        }
                        else if (z == '$')
                        {
                            state = TokenerState.End;
                        }
                        else if (char.IsDigit(z))
                        {
                            errorMessage = "После полинома было обнаружено число без операции между ними";
                            state = TokenerState.Error;
                        }
                        else
                        {
                            errorMessage = "После полинома был обнаружен неизвестный символ";
                            state = TokenerState.Error;
                        }

                        break;
                    }
                }

                if (state == TokenerState.Error) break;

                if (oldstate == TokenerState.Operand && state == TokenerState.Operand ||
                    oldstate == TokenerState.OperandAftDot && state == TokenerState.OperandAftDot ||
                    oldstate == TokenerState.Operand && state == TokenerState.Dot ||
                    oldstate == TokenerState.Dot && state == TokenerState.OperandAftDot)
                {
                    operand += z;
                }
                else if (oldstate != TokenerState.Operand && state == TokenerState.Operand)
                {
                    operand = Convert.ToString(z);
                }
                else if (oldstate == TokenerState.Operand && state != TokenerState.Operand ||
                         oldstate == TokenerState.OperandAftDot && state != TokenerState.OperandAftDot)
                {
                    var t = new Token {Value = operand, Type = TokenType.Number};
                    result.Add(t);
                }

                if (state == TokenerState.Operation)
                {
                    var t = new Token
                    {
                        Value = Convert.ToString(z),
                        Type = TokenType.Operation
                    };
                    result.Add(t);
                }
                else if (state == TokenerState.Polynom)
                {
                    var t = new Token
                    {
                        Value = Convert.ToString(z),
                        Type = TokenType.Variable
                    };
                    result.Add(t);
                }
            }

            switch (state)
            {
                case TokenerState.Error:
                    throw new ArithmeticException(errorMessage);
                case TokenerState.Warning:
                    throw new WarningException(errorMessage);
            }

            return result;
        }
    }
}

