/*
    16.9. Operations. Implementing add, subtract, multiply and divide only with the + operator.
*/

using System;

namespace question_16._9
{
    class Program
    {
        static void Main(string[] args)
        {
            int add = Add(23, 22);
            int multi = Multiply(12, 12);
            int divi = Divide(36, 6);
            int sub = Subtract(25, 5);

            Console.WriteLine("Results {0}, {1}, {2}, {3}", add, multi, divi, sub);
        }

        static int Negate(int num)
        {
            int neg = 0;
            int newSign = num < 0 ? 1 : -1;
            while (num != 0)
            {
                // Yep, really...
                neg += newSign;
                num += newSign;
            }
            return neg;
        }

        static int Add(int a, int b)
        {
            return a + b;
        }

        static int Subtract(int a, int b)
        {
            return a + Negate(b);
        }

        static int Multiply(int a, int b)
        {
            int count = 0;
            int res = 0;
            while (count < a)
            {
                res += b;
                count += 1;
            }
            return res;
        }

        static int Divide(int a, int b)
        {
            int count = 0;
            int res = a;
            while (count < b)
            {
                res += Negate(b);
                count += 1;
            }
            return count;
        }
    }
}
