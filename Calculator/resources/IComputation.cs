using System.Collections.Generic;

namespace Calculator
{
    public interface IComputation
    {
        Result Operation(List<Element> formula, int index = 0, int previousOperationPriority = 0, bool isNegative = false);
    }
}