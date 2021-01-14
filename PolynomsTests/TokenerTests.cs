using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polynoms;

namespace PolynomsTests
{
    [TestClass]
    public class TokenerTests
    {
        [TestMethod]
        public void SimpleString()
        {
            var tokens = Tokener.TokenizeString("1+2*x^2-2*x$");
            var tokens1 = new List<Tokener.Token>
            {
                new Tokener.Token {Value = "1", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "+", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "^", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
            };
            Assert.AreEqual(tokens1.Count, tokens.Count);
            for (var i = 0; i < tokens.Count; i++)
            {
                Assert.AreEqual(tokens1[i], tokens[i]);
            }
        }

        [TestMethod]
        public void SimpleStringWithFirstUnaryMinus()
        {
            var tokens = Tokener.TokenizeString("-1+2*x^2-2*x-2$");
            var tokens1 = new List<Tokener.Token>
            {
                new Tokener.Token {Value = "-1", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "+", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "^", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
            };
            Assert.AreEqual(tokens1.Count, tokens.Count);
            for (var i = 0; i < tokens.Count; i++)
            {
                Assert.AreEqual(tokens1[i], tokens[i]);
            }
        }

        [TestMethod]
        public void SimpleStringWithVariableStart()
        {
            var tokens = Tokener.TokenizeString("x+2*x^2-2*x-2$");
            var tokens1 = new List<Tokener.Token>
            {
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "+", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "^", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
            };
            Assert.AreEqual(tokens1.Count, tokens.Count);
            for (var i = 0; i < tokens.Count; i++)
            {
                Assert.AreEqual(tokens1[i], tokens[i]);
            }
        }

        [TestMethod]
        public void SimpleStringWithFractionalNumber()
        {
            var tokens = Tokener.TokenizeString("1+2.87*x^2-2*x-2.87$");
            var tokens1 = new List<Tokener.Token>
            {
                new Tokener.Token {Value = "1", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "+", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2.87", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "^", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2", Type = Tokener.TokenType.Number},
                new Tokener.Token {Value = "*", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "x", Type = Tokener.TokenType.Variable},
                new Tokener.Token {Value = "-", Type = Tokener.TokenType.Operation},
                new Tokener.Token {Value = "2.87", Type = Tokener.TokenType.Number},
            };
            Assert.AreEqual(tokens1.Count, tokens.Count);
            for (var i = 0; i < tokens.Count; i++)
            {
                Assert.AreEqual(tokens1[i], tokens[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void DoubleOperationInRowError()
        {
            Tokener.TokenizeString("1+2**x^2-2*x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void DoubleVariableInRowError()
        {
            Tokener.TokenizeString("1+2*xx^2-2*x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void DoubleDifferentVariableError()
        {
            Tokener.TokenizeString("1+2*x^2-2*y$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void OperationInEndError()
        {
            Tokener.TokenizeString("1+2*x^2-2*x+$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void VariableAfterNumberError()
        {
            Tokener.TokenizeString("1+2*x^2-2x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void StartWithOperationError()
        {
            Tokener.TokenizeString("*1+2*x^2-2x$");
        }

        [TestMethod]
        [ExpectedException(typeof(WarningException))]
        public void EmptyStringWarning()
        {
            Tokener.TokenizeString("$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void VariableAfterFractNumberError()
        {
            Tokener.TokenizeString("1+2*x^2-2.5x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void VariableAfterDotError()
        {
            Tokener.TokenizeString("1+2*x^2-2.x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void BeginStringUnexpectedSymbolError()
        {
            Tokener.TokenizeString(".1+2*x^2-2.x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void UnexpectedSymbolAfterOperationError()
        {
            Tokener.TokenizeString("1+2*(x^2-2x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void UnexpectedSymbolAfterNumberError()
        {
            Tokener.TokenizeString("1+2(*x^2-2x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void DoubleDotNumberError()
        {
            Tokener.TokenizeString("1+2.2.45*x^2-2x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void UnexpectedSymbolAfterFractNumberError()
        {
            Tokener.TokenizeString("1+2.2(*x^2-2x$");
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void NumberAfterVariableError()
        {
            Tokener.TokenizeString("1+2*x3^2-2x$");
        }
    }
}
