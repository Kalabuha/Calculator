using System.Collections.Generic;

namespace Calculator
{
    public interface IProcessing
    {
        bool ConversionCheck(List<Element> formula);
        bool AlternationCheck(List<Element> formula);
        bool IntegrityCheck(List<Element> elements);
        List<Element> ObjectsConverter(List<string> packets);
        List<string> ObjectsSplit(string line);
    }
}