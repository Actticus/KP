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
    }
}

