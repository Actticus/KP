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