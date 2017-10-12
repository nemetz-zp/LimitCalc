using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LimitCalc
{
    public static class DoubleExtensions
    {
        private static double _value = 1E-3;

        public struct Epsilon
        {
            public Epsilon(double value) { _value = value; }
            internal static bool IsEqual(double a, double b) { return (a == b) || (Math.Abs(a - b) < _value); }
            internal static bool IsNotEqual(double a, double b) { return (a != b) && !(Math.Abs(a - b) < _value); }
        }
        public static bool EQ(this double a, double b) { return Epsilon.IsEqual(a, b); }
        public static bool LE(this double a, double b) { return Epsilon.IsEqual(a, b) || (a < b); }
        public static bool GE(this double a, double b) { return Epsilon.IsEqual(a, b) || (a > b); }

        public static bool NE(this double a, double b) { return Epsilon.IsNotEqual(a, b); }
        public static bool LT(this double a, double b) { return Epsilon.IsNotEqual(a, b) && (a < b); }
        public static bool GT(this double a, double b) { return Epsilon.IsNotEqual(a, b) && (a > b); }
    }
}
