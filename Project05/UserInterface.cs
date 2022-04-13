using Calculator;
using System;

namespace Project05
{
    public class UserInterface
    {
        public void FileFoundOrNot(bool exist)
        {
            ConsoleColor color;
            string message;
            if (exist)
            {
                message = "\nFile was found.\n";
                color = ConsoleColor.Green;
            }
            else
            {
                message = "\nFile not found.\n";
                color = ConsoleColor.Red;
            }
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Greetings()
        {
            Console.WriteLine("Greetings! This is a calculator.");
            Console.WriteLine("Enter the expression you want to calculate and press enter.\nfor example: (2 + 3) * 4\n");
        }

        public void ResultMessageForUser(TotalResult userString)
        {
            switch (userString.State)
            {
                case State.Empty:
                    Console.WriteLine("");
                    break;
                case State.Error:
                    Console.WriteLine("Expression contains errors.\n");
                    break;
                case State.DivisionByZero:
                    Console.WriteLine("Expression contains division by zero.\n");
                    break;
                case State.Normal:
                    Console.WriteLine("Result: " + userString.Number + "\n");
                    break;
            }
        }
    }
}
