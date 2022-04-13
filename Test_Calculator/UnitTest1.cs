using System;
using Xunit;
using Calculator;
using System.Globalization;

namespace Test_Calculator
{
    public class UnitTest1
    {
        [Theory]
        //GroupSeparator
        [InlineData("1.000,05 + 1.000,05", State.Normal, 2000.1)]
        [InlineData("1.0.0.0.,05 + 1.0.0.0.,05", State.Normal, 2000.1)]
        [InlineData("100,.05 + 100,.05", State.Error, null)]
        [InlineData("100.,05 + 100.,05", State.Normal, 200.1)]

        //Normal
        [InlineData("1,1 + 1,1", State.Normal, 2.2)]
        [InlineData("1 + 2 * 3 + - 3", State.Normal, 4.0)]
        [InlineData("8 * 4 + 7 / 2", State.Normal, 35.5)]
        [InlineData("14 - 20 * 2 - 13,87", State.Normal, -39.87)]
        [InlineData("14 + 20 * 2 + 13,87", State.Normal, 67.87)]
        [InlineData("-1.1,29 - - 45,26 * 3,9", State.Normal, 165.224)]
        [InlineData("((0/-24,5)+6--12)* + (-(-(12-16)* -4) --(-33 - - 40) * 2)", State.Normal, 540)]

        //Exponent
        [InlineData("2*2^2^2", State.Normal, 32)]
        [InlineData("5 - 4^3 * 2^5 * 1^0 + 100", State.Normal, -1943)]
        [InlineData("7 + 3 * 2^2^2 * 3 - - 0,2^-(1--2) * 5 - 6", State.Normal, 770)]

        //Division by zero
        [InlineData("55 / 0", State.DivisionByZero, null)]
        [InlineData("123,45 / ((- 28 - - 12) + 4 * (2 + 2))", State.DivisionByZero, null)]

        //Error
        [InlineData("1(1 + 1)", State.Error, null)]
        [InlineData("1 + 2 * (3 + 3) + ()", State.Error, null)]
        [InlineData("-24,48 - 33,6,17", State.Error, null)]
        [InlineData("24 * 81 + 13x", State.Error, null)]
        [InlineData("17^^(27,5 * 2 - 16 * 0,5)", State.Error, null)]
        [InlineData("3 + (2 - (6 * 7 + 8)", State.Error, null)]
        [InlineData("(1+5)-6)*3)", State.Error, null)]
        [InlineData("(1+5-6**3)", State.Error, null)]
        [InlineData("4 + + + 8", State.Error, null)]

        public void Computation_Test(string inputLine, State theoryCondition, decimal theoryResult)
        {
            NumberFormatInfo formatter = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = Convert.ToChar(formatter.CurrencyDecimalSeparator);
            var groupSeparator = Convert.ToChar(formatter.NumberGroupSeparator);

            string modifiedLine;
            modifiedLine = inputLine.Replace(',', decimalSeparator);
            modifiedLine = modifiedLine.Replace('.', groupSeparator);

            var calculator = new DataProcessing(new Processing(), new Computation());
            var result = calculator.GetResult(modifiedLine);

            Assert.Equal(theoryCondition, result.State);

            if (theoryCondition == State.Normal)
                Assert.Equal(theoryResult, result.Number);
        }
    }
}

