﻿using System;
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

        public static Answer PolynomToStringConvert(string pol)
        {
            pol = pol.Replace(" ", string.Empty);
            List<Tokener.Token> tokens;
            try
            {
                tokens = Tokener.TokenizeString(pol + '$');
            }
            catch (WarningException e)
            {
                return new Answer { Ans = e.Message, Code = AnswerCode.Warning };
            }
            catch (ArithmeticException e)
            {
                return new Answer { Ans = e.Message, Code = AnswerCode.Error };
            }

            var polynomPows = new List<PolynomPow>();
            tokens.Insert(0, new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "+" });

            while (tokens.FindIndex(x => x.Type == Tokener.TokenType.Variable) != -1)
            {
                double power = 0;
                double value = 1;
                var i = tokens.FindIndex(x => x.Type == Tokener.TokenType.Variable);
                var indexPow = tokens.FindIndex(x => x.Value == "^");
                while (indexPow != -1 && indexPow < i - 1)
                {
                    Calculate(tokens, indexPow, Pow);
                    indexPow = tokens.FindIndex(x => x.Value == "^");
                    i = tokens.FindIndex(x => x.Type == Tokener.TokenType.Variable);
                }

                var indexDiv = tokens.FindIndex(x => x.Value == "/");
                var indexMul = tokens.FindIndex(x => x.Value == "*");
                int index;
                while (indexDiv != -1 && indexDiv < i - 1 || indexMul != -1 && indexMul < i - 1)
                {
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

                    Calculate(tokens, index, del);
                    indexDiv = tokens.FindIndex(x => x.Value == "/");
                    indexMul = tokens.FindIndex(x => x.Value == "*");
                    i = tokens.FindIndex(x => x.Type == Tokener.TokenType.Variable);
                }

                var powPos = true;
                switch (tokens[i - 1].Value)
                {
                    case "*":
                        if (tokens[i - 3].Value == "-")
                            value = double.Parse(tokens[i - 2].Value) * -1;
                        else
                            value = double.Parse(tokens[i - 2].Value);
                        tokens.RemoveAt(i - 3);
                        tokens.RemoveAt(i - 3);
                        tokens.RemoveAt(i - 3);
                        break;
                    case "/":
                        powPos = false;
                        if (tokens[i - 3].Value == "-")
                            value = double.Parse(tokens[i - 2].Value) * -1;
                        else
                            value = double.Parse(tokens[i - 2].Value);
                        tokens.RemoveAt(i - 3);
                        tokens.RemoveAt(i - 3);
                        tokens.RemoveAt(i - 3);
                        break;
                    case "+":
                        value = 1;
                        tokens.RemoveAt(i - 1);
                        break;
                    case "-":
                        value = -1;
                        tokens.RemoveAt(i - 1);
                        break;
                    default:
                        return new Answer { Ans = "Неверный формат данных", Code = AnswerCode.Error };
                }

                i = tokens.FindIndex(x => x.Type == Tokener.TokenType.Variable);

                var indexAdd = tokens.FindIndex(1, x => x.Type == Tokener.TokenType.Operation && x.Value == "+");
                var indexSub = tokens.FindIndex(1, x => x.Type == Tokener.TokenType.Operation && x.Value == "-");
                string action = null;
                if (indexSub < indexAdd && indexSub != -1 || indexAdd == -1 && indexSub != -1)
                {
                    index = indexSub;
                    action = "-";
                }
                else if (indexSub > indexAdd && indexAdd != -1 || indexAdd != -1 && indexSub == -1)
                {
                    index = indexAdd;
                    action = "+";
                }
                else
                {
                    index = tokens.Count;
                }

                indexPow = tokens.FindIndex(i, x => x.Value == "^");
                while (indexPow != -1 && indexPow < index)
                {
                    if (tokens[indexPow + 1].Type == Tokener.TokenType.Variable)
                        return new Answer { Ans = "Неверный формат данных", Code = AnswerCode.Error };
                    if (indexPow < i + 3)
                    {
                        indexPow = tokens.FindIndex(indexPow + 1, x => x.Value == "^");
                        continue;
                    }

                    Calculate(tokens, indexPow, Pow);
                    indexPow = tokens.FindIndex(x => x.Value == "^");
                    if (index != tokens.Count)
                        index = tokens.FindIndex(x => x.Value == action);
                }

                if (i + 1 < tokens.Count)
                    while (tokens[i + 1].Value == "^")
                    {
                        power = power == 0
                            ? double.Parse(tokens[i + 2].Value)
                            : Pow(power, double.Parse(tokens[i + 2].Value));

                        tokens.RemoveAt(i + 1);
                        tokens.RemoveAt(i + 1);
                        if (i + 1 >= tokens.Count)
                            break;
                    }

                if (i + 1 < tokens.Count)
                    while ((tokens[i + 1].Value == "*" || tokens[i + 1].Value == "/") && i + 1 < tokens.Count)
                    {
                        switch (tokens[i + 1].Value)
                        {
                            case "*" when tokens[i + 2].Type == Tokener.TokenType.Variable:
                                power += 1;
                                break;
                            case "*" when tokens[i + 2].Type == Tokener.TokenType.Number:
                                value *= double.Parse(tokens[i + 2].Value);
                                break;
                            case "/" when tokens[i + 2].Type == Tokener.TokenType.Variable:
                                power -= 1;
                                break;
                            case "/" when tokens[i + 2].Type == Tokener.TokenType.Number:
                                value /= double.Parse(tokens[i + 2].Value);
                                break;
                        }

                        tokens.RemoveAt(i + 1);
                        tokens.RemoveAt(i + 1);
                        if (i + 1 >= tokens.Count)
                            break;
                    }

                i = tokens.FindIndex(x => x.Type == Tokener.TokenType.Variable);
                tokens.RemoveAt(i);
                if (power.Equals(0))
                    power = 1;
                if (!powPos) power *= -1;
                polynomPows.Add(new PolynomPow { Value = value, Power = power });
            }

            if (tokens.Count > 0)
            {
                tokens.RemoveAt(0);

                while (tokens.FindIndex(x => x.Value == "^") != -1)
                {
                    var index = tokens.FindIndex(x => x.Value == "^");
                    Calculate(tokens, index, Pow);
                }

                while (tokens.FindIndex(x => x.Value == "*") != -1 ||
                       tokens.FindIndex(x => x.Value == "/") != -1)
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

                    Calculate(tokens, index, del);
                }

                while (tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "+" }) ||
                       tokens.Contains(new Tokener.Token { Type = Tokener.TokenType.Operation, Value = "-" }))
                {
                    var indexAdd = tokens.FindIndex(x => x.Value == "+");
                    var indexSub = tokens.FindIndex(x => x.Value == "-");
                    int index;
                    CalcDelegate del;
                    if (indexSub < indexAdd && indexSub != -1 || indexAdd == -1)
                    {
                        index = indexSub;
                        del = Substract;
                    }
                    else
                    {
                        index = indexAdd;
                        del = Add;
                    }

                    Calculate(tokens, index, del);
                }

                polynomPows.Add(new PolynomPow { Value = double.Parse(tokens[0].Value), Power = 0 });
            }

            for (var i = 0; i < polynomPows.Count; i++)
            {
                var power = polynomPows[i].Power;
                var list = polynomPows.FindAll(x => x.Power == power);
                var value = list.Sum(element => element.Value);
                polynomPows.RemoveAll(x => x.Power == power);
                polynomPows.Add(new PolynomPow { Power = power, Value = value });
            }

            polynomPows = polynomPows.OrderByDescending(x => x.Power).ToList();
            for (var i = 0; i < polynomPows[0].Power; i++)
                if (polynomPows.FindIndex(x => x.Power.Equals(i)) == -1)
                    polynomPows.Add(new PolynomPow { Power = i, Value = 0 });

            return new Answer
            {
                Ans = string.Join(", ",
                    polynomPows.OrderBy(x => x.Power).Select(x => x.Value.ToString().Replace(',', '.'))),
                Code = AnswerCode.Ok
            };
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

        public static Answer DifferentialStringCalculate(string str)
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
            answer.Append(nums[1]);
            for (var i = 2; i < nums.Count; i++) answer.Append(", " + nums[i] * i);
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