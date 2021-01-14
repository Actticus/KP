using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Polynoms
{
    public abstract class Calculator
    {
        public enum AnswerCode
        {
            Ok = 0,  
            Error = 1,  
            Warning = 2  
        }

        private static void ReplaceNear(IList<Tokener.Token> tokens, int index, string value)
        {
            if (tokens[index - 1].Type == Tokener.TokenType.Variable)
                tokens[index - 1] = new Tokener.Token { Type = Tokener.TokenType.Number, Value = value };
            if (tokens[index + 1].Type == Tokener.TokenType.Variable)
                tokens[index + 1] = new Tokener.Token { Type = Tokener.TokenType.Number, Value = value };
        }

        private static void Calculate(IList<Tokener.Token> tokens, int index, CalcDelegate mathAction)
        {
            tokens[index] = mathAction(tokens[index - 1], tokens[index + 1]);
            tokens.RemoveAt(index + 1);
            tokens.RemoveAt(index - 1);
        }

        private static Tokener.Token Add(Tokener.Token x, Tokener.Token y)
        {
            return new Tokener.Token
            {
                Value = Math.Round(double.Parse(x.Value) + double.Parse(y.Value), 3).ToString(),
                Type = Tokener.TokenType.Number
            };
        }

        private static Tokener.Token Substract(Tokener.Token x, Tokener.Token y)
        {
            return new Tokener.Token
            {
                Value = Math.Round(double.Parse(x.Value) - double.Parse(y.Value), 3).ToString(),
                Type = Tokener.TokenType.Number
            };
        }

        private static Tokener.Token Multiply(Tokener.Token x, Tokener.Token y)
        {
            return new Tokener.Token
            {
                Value = Math.Round(double.Parse(x.Value) * double.Parse(y.Value), 3).ToString(),
                Type = Tokener.TokenType.Number
            };
        }

        private static Tokener.Token Divide(Tokener.Token x, Tokener.Token y)
        {
            return new Tokener.Token
            {
                Value = Math.Round(double.Parse(x.Value) / double.Parse(y.Value), 3).ToString(),
                Type = Tokener.TokenType.Number
            };
        }

        private static Tokener.Token Pow(Tokener.Token x, Tokener.Token y)
        {
            return new Tokener.Token
            {
                Value = Math.Round(Math.Pow(double.Parse(x.Value), double.Parse(y.Value)), 3).ToString(),
                Type = Tokener.TokenType.Number
            };
        }

        private static double Pow(double x, double y)
        {
            return Math.Round(Math.Pow(x, y), 3);
        }

        private delegate Tokener.Token CalcDelegate(Tokener.Token x, Tokener.Token y);

         
        private struct PolynomPow
        {
            public double Power;
            public double Value;
        }

         
        public struct Answer
        {
            public string Ans;
            public AnswerCode Code;
        }
    }
}