using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InterfacePractice
{
    interface IDoMath
    {
        double Calc(double x, double y);
    }

    class Addition : IDoMath
    {
        public double Calc(double x, double y)
        {
            return x + y;
        }
    }

    class Subtraction : IDoMath
    {
        public double Calc(double x, double y)
        {
            return x - y;
        }
    }

    class Division : IDoMath
    {
        public double Calc(double x, double y)
        {
            return x / y;
        }
    }

    class Multiplication : IDoMath
    {
        public double Calc(double x, double y)
        {
            return x * y;
        }
    }

    internal class Program
    {
        private static void KeepConsole()
        {
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void HandleException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void DifferentOptions()
        {
            Console.WriteLine("Type 1 to use: Addition");
            Console.WriteLine("Type 2 to use: Subtraction");
            Console.WriteLine("Type 3 to use: Division");
            Console.WriteLine("Type 4 to use: Multiplication");
        }

        private static void CalculateResult(IDoMath calc, List<double> ResultList)
        {
            double result = Calculate(calc);
            ResultList.Add(result);
            Console.WriteLine($"Result: {result}");
        }

        private static void CalculateWithPrevious(IDoMath calc, double previousResult, List<double> ResultList)
        {
            Console.WriteLine("What number to add?");
            double num2 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            double result = calc.Calc(previousResult, num2);
            ResultList.Add(result);
            Console.WriteLine($"Result: {result}");
        }

        static void Main(string[] args)
        {
            bool isRunning = true;
            List<double> ResultList = new List<double>();

            while (isRunning)
            {
                IDoMath calc;

                CultureInfo culture = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentCulture = culture;

                Console.WriteLine("Calculator. Made by: Martin Due. You may use decimals, just be sure to use '.'\n");
                DifferentOptions();
                Console.WriteLine("Type 5 to continue with the previous calculated number");
                Console.WriteLine("Type 6 to view the last calculated numbers");
                Console.WriteLine("Type anything else to leave the program");

                try
                {
                    int userInput = int.Parse(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1:
                            calc = new Addition();
                            CalculateResult(calc, ResultList);
                            KeepConsole();
                            break;
                        case 2:
                            calc = new Subtraction();
                            CalculateResult(calc, ResultList);
                            KeepConsole();
                            break;
                        case 3:
                            calc = new Division();
                            CalculateResult(calc, ResultList);
                            KeepConsole();
                            break;
                        case 4:
                            calc = new Multiplication();
                            CalculateResult(calc, ResultList);
                            KeepConsole();
                            break;
                        case 5:
                            double previousResult = ResultList.Last<double>();
                            try
                            {
                                Console.WriteLine($"Your previous number is: {previousResult}. What do you want to do with it?\n");
                                DifferentOptions();
                                int userChoice = int.Parse(Console.ReadLine());
                                double num2;
                                switch (userChoice)
                                {
                                    case 1:
                                        calc = new Addition();
                                        CalculateWithPrevious(calc, previousResult, ResultList);
                                        break;
                                    case 2:
                                        calc = new Subtraction();
                                        CalculateWithPrevious(calc, previousResult, ResultList);
                                        break;
                                    case 3:
                                        calc = new Division();
                                        CalculateWithPrevious(calc, previousResult, ResultList);
                                        break;
                                    case 4:
                                        calc = new Multiplication();
                                        CalculateWithPrevious(calc, previousResult, ResultList);
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                HandleException(e);
                            }
                            break;
                        case 6:
                            for (int i = 0; i < ResultList.Count; i++)
                            {
                                Console.WriteLine($"{i}: {ResultList[i]}");
                            }
                            KeepConsole();
                            break;
                        default:
                            isRunning = false;
                            ResultList.Clear();
                            break;
                    }
                }
                catch (Exception e)
                {
                    HandleException(e);
                }
            }
        }

        public static double[] GetUserInputs()
        {
            try
            {
                Console.WriteLine("First number:");
                double userInput1 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Second number:");
                double userInput2 = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

                double[] inputs = { userInput1, userInput2 };

                return inputs;
            }
            catch (Exception e)
            {
                HandleException(e);
                return null;
            }
        }

        public static double Calculate(IDoMath calc)
        {
            double[] inputs = GetUserInputs();
            if(inputs == null)
            {
                throw new Exception();
            }
            else
            {
                return calc.Calc(inputs[0], inputs[1]);
            }
        }
    }
}
