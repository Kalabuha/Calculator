using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Computation : IComputation
    {
        public Result Operation(List<Element> formula, int i, int previousOperationPriority, bool isNegative)
        {
            decimal? left = null, rigth = null;
            bool isUnary = true;
            Type operation = Type.Plus;
            for (; i < formula.Count; i++)
            {
                switch (formula[i].Object)
                {
                    case Type.Digit:
                        (left, rigth, isNegative, isUnary) = GetNumbers(left, rigth, formula[i].Digit, isNegative);
                        break;
                    case Type.Minus:
                        isNegative = !isNegative;
                        goto case Type.Plus;
                    case Type.Plus:
                        if (isUnary) break;
                        goto default;
                    case Type.Open:
                        var subFormula = GetSubFormula(formula, i);
                        i += subFormula.Count + 1;
                        IComputation newComp = new Computation();
                        (left, rigth, isNegative, isUnary) = GetNumbers(left, rigth, newComp.Operation(subFormula).Value, isNegative);
                        break;
                    case Type.Close:
                        return new Result { Value = left, Index = i };
                    default:
                        isUnary = true;
                        operation = formula[i].Object;
                        if (formula[i].Priority <= previousOperationPriority)
                            return new Result { Value = left, Index = i };
                        else if (formula[i].Priority < 3) // where 3 is the highest priority
                        {
                            var resultOp = Operation(formula, i + 1, formula[i].Priority, isNegative);
                            rigth = resultOp.Value;
                            i = resultOp.Index - 1;
                            isNegative = false;
                        }
                        break;
                }
                if (left.HasValue && rigth.HasValue)
                {
                    left = Comput(left, ref rigth, operation, ref isUnary);
                    if (left == null) return null;
                }
            }
            return new Result { Value = left, Index = i };
        }

        private (decimal?, decimal?, bool, bool) GetNumbers(decimal? left, decimal? rigth, decimal? number, bool isNegative)
        {
            if (left == null)
                left = isNegative ? -number : number;
            else
                rigth = isNegative ? -number : number;

            return (left, rigth, false, false);
        }

        private decimal? Comput(decimal? left, ref decimal? rigth, Type operation, ref bool isUnary)
        {
            Func<decimal, decimal, decimal> func;

            switch (operation)
            {
                case Type.Plus or Type.Minus:
                    func = Operations.Sum;
                    break;
                case Type.Multiply:
                    func = Operations.Mul;
                    break;
                case Type.Divide:
                    if (rigth == 0)
                        return null;
                    func = Operations.Div;
                    break;
                case Type.Exponent:
                    func = Operations.Exp;
                    break;
                default:
                    throw new ApplicationException("Invalid operation");
            }
            var result = func(left.Value, rigth.Value);
            rigth = null;
            isUnary = false;
            return result;
        }

        private List<Element> GetSubFormula(List<Element> formula, int index)
        {
            index++;
            int fromRange = index;
            int numberElements = 0;
            int numberBrackets = 0;

            for (; index < formula.Count; index++)
            {
                if (formula[index].Object == Type.Open)
                    numberBrackets++;
                else if (formula[index].Object == Type.Close)
                {
                    if (numberBrackets == 0)
                    {
                        break;
                    }
                    else
                    {
                        numberBrackets--;
                    }
                }
                numberElements++;
            }

            return formula.GetRange(fromRange, numberElements);
        }
    }
}
