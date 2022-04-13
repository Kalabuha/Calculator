using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;


namespace Calculator
{
    public class Processing : IProcessing
    {
        private readonly NumberFormatInfo _formatter;

        public Processing()
        {
            _formatter = CultureInfo.CurrentCulture.NumberFormat;
        }

        public List<string> ObjectsSplit(string line)
        {
            if (String.IsNullOrWhiteSpace(line)) return new List<string>();

            string mask = @"([+\-*/^()])";
            var packets = Regex.Split(line, mask).Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
            return packets;
        }

        public List<Element> ObjectsConverter(List<string> packets)
        {
            List<Element> elements = new();
            if (packets == null) return elements;
            for (int i = 0; i < packets.Count; i++)
            {
                if (Decimal.TryParse(packets[i], NumberStyles.Number, _formatter, out decimal value))
                    elements.Add(new Element { Object = Type.Digit, Digit = value, Priority = 0 });
                else
                {
                    Type obj;
                    int pri = 0;
                    switch (packets[i])
                    {
                        case "+":
                            obj = Type.Plus;
                            pri = 1; break;
                        case "-":
                            obj = Type.Minus;
                            pri = 1; break;
                        case "*":
                            obj = Type.Multiply;
                            pri = 2; break;
                        case "/":
                            obj = Type.Divide;
                            pri = 2; break;
                        case "^":
                            obj = Type.Exponent;
                            pri = 3; break;
                        case "(": obj = Type.Open; break;
                        case ")": obj = Type.Close; break;
                        default: return null;
                    }
                    elements.Add(new Element { Object = obj, Priority = pri });
                }
            }
            return elements;
        }

        public bool ConversionCheck(List<Element> elements)
        {
            if (elements == null)
                return false;

            return true;
        }

        public bool IntegrityCheck(List<Element> elements)
        {
            int integrity = 0;
            foreach (var element in elements)
            {
                if (element.Object == Type.Open) integrity++;
                else if (element.Object == Type.Close) integrity--;

                if (integrity < 0) return false;
            }
            return integrity == 0;
        }

        public bool AlternationCheck(List<Element> formula)
        {
            int count = 0;
            for (int i = 0; i < formula.Count; i++)
            {
                var current = formula[i];
                if (count == 0)
                    count = FirstElement(current);

                else if (count == 1)
                    count = SacondElement(current);

                else if (count == 2)
                    count = ThirdElement(current);

                if (count == -2)
                    return false;

                count++;

                if (i == formula.Count - 1 && !(formula[i].Object == Type.Digit || formula[i].Object == Type.Close))
                    return false;
            }
            return true;
        }

        private int FirstElement(Element current)
        {
            switch (current.Object)
            {
                case Type.Digit:
                    return 1;
                case Type.Open:
                    return -1;
                case Type.Plus:
                case Type.Minus:
                    return 0;
                default:
                    return -2;
            }
        }

        private int SacondElement(Element current)
        {
            switch (current.Object)
            {
                case Type.Open:
                    return -1;
                case Type.Digit:
                    return 1;
                default:
                    return -2;
            }
        }

        private int ThirdElement(Element current)
        {
            switch (current.Object)
            {
                case Type.Plus:
                case Type.Minus:
                case Type.Multiply:
                case Type.Divide:
                case Type.Exponent:
                    return -1;
                case Type.Close:
                    return 1;
                default:
                    return -2;
            }
        }
    }
}
