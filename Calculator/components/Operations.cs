using System;

namespace Calculator
{
    internal static class Operations
    {
        internal static decimal Sum(decimal x, decimal y)
        {
            return x + y;
        }

        internal static decimal Mul(decimal x, decimal y)
        {
            return x * y;
        }

        internal static decimal Div(decimal x, decimal y)
        {
            return x / y;
        }
        internal static decimal Exp(decimal x, decimal y)
        {
            return (decimal)Math.Pow((double)x, (double)y);
        }
    }
}
