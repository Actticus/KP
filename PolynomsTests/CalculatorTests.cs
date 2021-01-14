using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polynoms;

namespace PolynomsTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void CalculatePolynomValue()
        {
            var answer = Calculator.CalculatePolynomValue("1+2*x^2-2*x+2/2", "3");
            Assert.AreEqual(new Calculator.Answer {Ans = "14", Code = Calculator.AnswerCode.Ok}, answer);
        }

        [TestMethod]
        public void StringToPolynomConvert()
        {
            var answer = Calculator.StringToPolynomConvert("1, 0, 3, -5");
            Assert.AreEqual(new Calculator.Answer { Ans = "1+3*x^2-5*x^3", Code = Calculator.AnswerCode.Ok }, answer);
        }

        [TestMethod]
        public void DiffPolCalculate()
        {
            var answer = Calculator.DifferentialStringCalculate("1, 0, 3, -5");
            Assert.AreEqual(new Calculator.Answer { Ans = "0, 6, -15", Code = Calculator.AnswerCode.Ok }, answer);
        }

        [TestMethod]
        public void LongPolynomToStringConvert()
        {
            var answer = Calculator.PolynomToStringConvert("2^2*2/2+2*3/x-1/2*x^2*8/2^2");
            Assert.AreEqual(new Calculator.Answer { Ans = "6, 4, 0, -1", Code = Calculator.AnswerCode.Ok }, answer);
        }

        [TestMethod]
        public void PolynomToStringConvert()
        {
            var answer = Calculator.PolynomToStringConvert("2^2+x^2*2^2/2+7^2/2+1*2+1-1");
            Assert.AreEqual(new Calculator.Answer { Ans = "30.5, 0, 2", Code = Calculator.AnswerCode.Ok }, answer);
        }

        [TestMethod]
        public void ShortPolynomToStringConvert()
        {
            var answer = Calculator.PolynomToStringConvert("x^2*2^2/2");
            Assert.AreEqual(new Calculator.Answer { Ans = "0, 0, 2", Code = Calculator.AnswerCode.Ok }, answer);
        }

        [TestMethod]
        public void DoubleOperationInRowValueError()
        {
            var answer = Calculator.CalculatePolynomValue("1+2*x^2-2**x+2/2", "3");
            Assert.AreEqual(new Calculator.Answer { Ans = "В строке обнаружены два оператора подряд", Code = Calculator.AnswerCode.Error }, answer);
        }

        [TestMethod]
        public void StringToPolynomConvertError()
        {
            var answer = Calculator.StringToPolynomConvert("1, 0, 3x, 5");
            Assert.AreEqual(new Calculator.Answer { Ans = "Неверный формат строки", Code = Calculator.AnswerCode.Error }, answer);
        }

        [TestMethod]
        public void DiffPolCalculateError()
        {
            var answer = Calculator.DifferentialStringCalculate("1, 0, 3x, 5");
            Assert.AreEqual(new Calculator.Answer { Ans = "Неверный формат строки", Code = Calculator.AnswerCode.Error }, answer);
        }

        [TestMethod]
        public void PolynomToStringConvertError()
        {
            var answer = Calculator.PolynomToStringConvert("1+2*x^2-2**x+2/2");
            Assert.AreEqual(new Calculator.Answer { Ans = "В строке обнаружены два оператора подряд", Code = Calculator.AnswerCode.Error }, answer);
        }

        [TestMethod]
        public void PolynomPowerPolynomError()
        {
            var answer = Calculator.PolynomToStringConvert("x^x");
            Assert.AreEqual(new Calculator.Answer { Ans = "Неверный формат данных", Code = Calculator.AnswerCode.Error }, answer);
        }

        [TestMethod]
        public void NumPowerPolynomError()
        {
            var answer = Calculator.PolynomToStringConvert("2^x");
            Assert.AreEqual(new Calculator.Answer { Ans = "Неверный формат данных", Code = Calculator.AnswerCode.Error }, answer);
        }

        [TestMethod]
        public void EmptyStringCalculatePolynomValueWarning()
        {
            var answer = Calculator.PolynomToStringConvert("");
            Assert.AreEqual(new Calculator.Answer { Ans = "Введена пустая строка", Code = Calculator.AnswerCode.Warning }, answer);
        }
    }
}