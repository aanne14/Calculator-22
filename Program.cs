using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeagenAssessment
{
    class Calculator
    {
        public static double Calculate(string sum)
        {
            //Your code starts here
            double result = double.NaN;
            string numInput1 = "";
            string numInput2 = "";
            string numInput3 = "";
            string op = "";
            string op2 = "";

            sum = sum.Trim();

            string[] phrases = sum.Split(' ');

            List<String> list = new List<String>();
            int i = 0;
            foreach (var phrase in phrases)
            {
                list.Add(phrase);
                i++;
            }

            //first number
            numInput1 = list[0].ToString();
            double num1 = 0;
            double.TryParse(numInput1, out num1);


            //second number
            numInput2 = list[2].ToString();
            double num2 = 0;
            double.TryParse(numInput2, out num2);

            op = list[1].ToString();

            switch (op)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    // If user enter non-zero divisor. - If not break and return error
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    break;
                default:
                    break;
            }

            if (list.Count > 3)
            {
                //third number number
                numInput3 = list[4].ToString();
                double num3 = 0;
                double.TryParse(numInput3, out num3);

                op2 = list[3].ToString();

                if (op2 != "")
                {
                    switch (op2)
                    {
                        case "+":
                            result = result + num3;
                            break;
                        case "-":
                            result = result - num3;
                            break;
                        case "*":
                            result = result * num3;
                            break;
                        case "/":
                            // If user enter non-zero divisor. - If not break and return error
                            if (num3 != 0)
                            {
                                result = result / num3;
                            }
                            break;
                        default:
                            break;
                    }
                }

                //special case with more than 1 value in equation - following BODMAS
                if (op == "+" && op2 == "*")
                {
                    result = num1 + (num2 * num3);

                }
                else if (op == "-" && op2 == "*")
                {
                    result = num1 - (num2 * num3);

                }
                else if (op == "+" && op2 == "/")
                {
                    result = num1 + (num2 / num3);
                }
                else if (op == "-" && op2 == "/")
                {
                    result = num1 - (num2 / num3); //do some changes
                }
            }

            return result;
        }

        //If equation has brackets - solve first
        public static string RemoveBrackets(string text)
        {
            while (text.Contains('(') && text.Contains(')'))
            {
                int openIndex = 0;
                int closeIndex = 0;
                int j = 0;

                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '(')
                    {
                        openIndex = i;
                    }
                    if (text[i] == ')')
                    {
                        closeIndex = i;

                        text = text.Remove(openIndex, closeIndex - openIndex + 1).Insert(openIndex, ResolveBrackets(openIndex, closeIndex, text).ToString());

                        break;
                    }
                }

            }

            return Calculate(text).ToString();
        }

        public static double ResolveBrackets(int openIndex, int closeIndex, string text)
        {
            double bracketAnswer = 0;

            bracketAnswer = Calculate(text.Substring(openIndex + 2, closeIndex - openIndex - 3));

            return bracketAnswer;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            while (!endApp)
            {
                double result = 0;
                string sum = "";

                Console.WriteLine("Please enter an equation here:");
                sum = Console.ReadLine();

                try
                {

                    if (sum.Contains('(') && sum.Contains(')'))
                    {
                        double.TryParse(Calculator.RemoveBrackets(sum), out result);
                    }
                    else
                    {
                        result = Calculator.Calculate(sum);
                    }

                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                // Wait for the user to respond before closing - or continuing
                Console.Write("Press 'n' and Enter to close the app, or just press Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
            return;
        }
    }

}





