/*
    16.26. Calculator.
*/

using System;

namespace question_16._26
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            Console.WriteLine("1*3*2*2/6+4 = {0}", calculator.Calc("1*3*2*2/6+4"));
            // Console.WriteLine("4+4 = {0}", calculator.Calc("4+4"));
            // Console.WriteLine("4+4*4 = {0}", calculator.Calc("4+4*4"));
            // Console.WriteLine("12/3+14 = {0}", calculator.Calc("12/3+14"));
            // Console.WriteLine("4*4*4 = {0}", calculator.Calc("4*4*4"));
        }
    }
}
