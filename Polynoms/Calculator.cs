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

        public static Answer CalculatePolynomValue(string pol, string value)
        {
            pol = pol.Replace(" ", string.Empty);
            value = value.Replace(" ", string.Empty);
            List<Tokener.Token> tokens;
            try
            {
                tokens = Tokener.TokenizeString(pol + '$');
                double.Parse(value.Trim());
            }
            catch (WarningException e)
            {
                return new Answer { Ans = e.Message, Code = AnswerCode.Warning };
            }
            catch (ArithmeticException e)
            {
                return new Answer { Ans = e.Message, Code = AnswerCode.Error };
            }
            catch (FormatException)
            {
                return new Answer { Ans = "Введен не числовой формат", Code = AnswerCode.Error };
            }

            while (tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "^" }))
            {
                var index = tokens.FindIndex(x => x.Value == "^");
                ReplaceNear(tokens, index, value);
                Calculate(tokens, index, Pow);
            }

            while (tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "*" }) ||
                   tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "/" }))
            {
                var indexDiv = tokens.FindIndex(x => x.Value == "/");
                var indexMul = tokens.FindIndex(x => x.Value == "*");
                int index;
                CalcDelegate del;
                if (indexMul < indexDiv && indexMul != -1 || indexDiv == -1)
                {
                    index = indexMul;
                    del = Multiply;
                }
                else
                {
                    index = indexDiv;
                    del = Divide;
                }

                ReplaceNear(tokens, index, value);
                Calculate(tokens, index, del);
            }

            while (tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "+" }) ||
                   tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "-" }))
            {
                var indexAdd = tokens.FindIndex(x => x.Value == "+");
                var indexSub = tokens.FindIndex(x => x.Value == "-");
                int index;
                CalcDelegate del;
                if (indexSub < indexAdd && indexSub != -1)
                {
                    index = indexSub;
                    del = Substract;
                }
                else
                {
                    index = indexAdd;
                    del = Add;
                }

                ReplaceNear(tokens, index, value);
                Calculate(tokens, index, del);
            }

            return new Answer { Ans = tokens[0].Value, Code = AnswerCode.Ok };
        }

        public static Answer StringToPolynomConvert(string str)
        {
            List<double> nums;
            try
            {
                nums = str.Trim().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse)
                    .ToList();
            }
            catch (FormatException)
            {
                return new Answer { Ans = "Неверный формат строки", Code = AnswerCode.Error };
            }

            var answer = new StringBuilder();
            for (var i = 0; i < nums.Count; i++)
            {
                if (answer.ToString() != string.Empty && nums[i] > 0)
                    answer.Append('+');
                if (i == 0)
                    answer.Append(nums[0]);
                else if (nums[i] > 0)
                    answer.Append(nums[i] + "*x^" + i);
                else if (nums[i] < 0)
                    answer.Append(nums[i] + "*x^" + i);
            }

            return new Answer { Ans = answer.ToString(), Code = AnswerCode.Ok };
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