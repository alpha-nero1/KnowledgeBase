using System;
using System.Threading;

namespace question_15._7
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 100;
            BaseThread[] threads = new BaseThread[] {
                new FizzBuzzThread(i => i % 3 == 0 && i % 5 == 0, i => "FizzBuzz", n),
                new FizzBuzzThread(i => i % 3 == 0 && i % 5 != 0, i => "Fizz", n),
                new FizzBuzzThread(i => i % 3 != 0 && i % 5 == 0, i => "Buzz", n),
                new FizzBuzzThread(i => i % 3 != 0 && i % 5 != 0, i => $"{i}", n)
            };
            foreach (BaseThread thread in threads)
            {
                thread.Start();
            }
        }
    }

    // Because Thread class is sealed and you cannot inherit...
    public abstract class BaseThread
    {
        private Thread _thread;

        protected BaseThread()
        {
            _thread = new Thread(new ThreadStart(this.RunThread));
        }

        // Thread methods / properties
        public void Start() => _thread.Start();
        public void Join() => _thread.Join();
        public bool IsAlive => _thread.IsAlive;

        // Override in base class
        public abstract void RunThread();
    }

    public class FizzBuzzThread : BaseThread
    {
        private static object _lock = new object();
        protected static int _current = 1;
        private int _max;
        private Predicate<int> _validate;
        private Function<int, string> _printer;

        public FizzBuzzThread(Predicate<int> validate, Function<int, string> printer, int max)
        {
            _validate = validate;
            _printer = printer;
            _max = max;
        }

        public void RunThread()
        {
            while (true)
            {
                lock (true)
                {
                    if (_current > max) return;

                    if (_validate.test(current))
                    {
                        Console.WriteLine(_printer(_current));
                        _current++;
                    }
                }
            }
        }
    }
}
