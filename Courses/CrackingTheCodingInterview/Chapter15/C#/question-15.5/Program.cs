using System;
using System.Threading;

namespace question_15._5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Foo
    {
        public Semaphore semOne, semTwo;

        public Foo()
        {
            try
            {
                semOne = new Semaphore(1);
                semTwo = new Semaphore(1);
                semOne.WaitOne();
                semTwo.WaitOne();
            }
            catch (Exception e)
            {

            }
        }

        public void First()
        {
            try
            {
                Console.WriteLine("Hello First!");
                semOne.Release();
            }
            catch (Exception e) {}
        }

        public void Second()
        {
            try
            {
                semOne.WaitOne();
                semOne.Release();

                Console.WriteLine("Hello Second!");
                semTwo.Release();
            }
            catch (Exception e) {}
        }

        public void Third()
        {
            try
            {
                semTwo.WaitOne();
                semTwo.Release();

                Console.WriteLine("Hello Third!");
            }
            catch (Exception e) {}
        }
    }
}
