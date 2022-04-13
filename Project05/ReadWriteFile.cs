using Calculator;
using System.Collections.Generic;
using System.IO;

namespace Project05
{
    public class ReadWriteFile
    {
        public string[] ReadFile(string inputPath)
        {
            return File.ReadAllLines(inputPath);
        }

        public void WriteFile(List<TotalResult> inputData, string outputParh)
        {
            List<string> outputData = new();
            foreach (var one in inputData)
            {
                var line = ResultStringForFile(one);
                outputData.Add(line);
            }

            File.WriteAllLines(outputParh, outputData);
        }

        private string ResultStringForFile(TotalResult inputLine)
        {
            string line = "";
            switch (inputLine.State)
            {
                case State.Error:
                    line = $"{inputLine.Line} = Error";
                    break;
                case State.DivisionByZero:
                    line = $"{inputLine.Line} = Division by zero";
                    break;
                case State.Normal:
                    line = $"{inputLine.Line} = {inputLine.Number}";
                    break;
                case State.Empty:
                    line = inputLine.Line;
                    break;
            }
            return line;
        }
    }
}
