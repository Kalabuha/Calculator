using Calculator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace Project05
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerBuild.Build();

            if (args.Length == 0)
            {
                WithoutArgs(container);
            }

            WithArgs(container, args);
        }

        static void WithoutArgs(IServiceProvider container)
        {
            var userInterface = container.GetService<UserInterface>();
            var calculator = container.GetService<DataProcessing>();

            userInterface.Greetings();
            while (true)
            {
                string userString = Console.ReadLine();
                var result = calculator.GetResult(userString);
                userInterface.ResultMessageForUser(result);
            }
        }

        static void WithArgs(IServiceProvider container, string[] args)
        {
            var userInterface = container.GetService<UserInterface>();
            var calculator = container.GetService<DataProcessing>();
            var readWrite = container.GetService<ReadWriteFile>();

            string inputPath = args[0];
            string outputPath;

            if (args.Length > 1)
                outputPath = args[1];
            else
                outputPath = Path.GetFileNameWithoutExtension(args[0]) + "_result.txt";

            FileInfo fileInfo = new FileInfo(inputPath);

            userInterface.FileFoundOrNot(fileInfo.Exists);
            if (!fileInfo.Exists) return;

            var inputData = readWrite.ReadFile(inputPath);
            List<TotalResult> outputData = new();
            foreach (var line in inputData)
            {
                var result = calculator.GetResult(line);
                outputData.Add(result);
            }

            readWrite.WriteFile(outputData, outputPath);
        }
    }
}
