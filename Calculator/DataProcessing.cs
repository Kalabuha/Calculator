using System.Collections.Generic;

namespace Calculator
{
    public class DataProcessing
    {
        private readonly IProcessing _processing;

        private readonly IComputation _computation;


        public DataProcessing(IProcessing processing, IComputation computation)
        {
            _processing = processing;
            _computation = computation;
        }

        public TotalResult GetResult(string inputLine)
        {
            TotalResult outputLine = new();
            outputLine.Line = inputLine.Trim();
            outputLine.Number = null;

            var lineSplit = _processing.ObjectsSplit(inputLine);
            if (lineSplit.Count == 0)
            {
                outputLine.State = State.Empty;
                return outputLine;
            }

            var elements = _processing.ObjectsConverter(lineSplit);
            var initial = InitialCheck(elements);
            if (!initial)
            {
                outputLine.State = State.Error;
                return outputLine;
            }

            var result = _computation.Operation(elements);
            if (result == null)
            {
                outputLine.State = State.DivisionByZero;
                return outputLine;
            }

            outputLine.State = State.Normal;
            outputLine.Number = result.Value;

            return outputLine;
        }

        private bool InitialCheck(List<Element> elements)
        {
            var conversion = _processing.ConversionCheck(elements);
            if (!conversion)
                return false;

            var integrity = _processing.IntegrityCheck(elements);
            if (!integrity)
                return false;

            var alternation = _processing.AlternationCheck(elements);
            if (!alternation)
                return false;

            return true;
        }
    }
}
