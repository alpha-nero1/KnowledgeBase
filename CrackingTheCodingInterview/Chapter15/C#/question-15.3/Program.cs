using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace question_15._3
{
    class Program
    {
        const int numberOfPhilosophers = 5;

        static void Main(string[] args)
        {
            var chopsticks = new Dictionary<int, object>(numberOfPhilosophers);
            for (int i = 0; i < numberOfPhilosophers; i++)
            {
                // Bootstrap the chopsticks...
                chopsticks.Add(i, new object());
            }

            // Allocate one task for each philosopher...
            Task[] tasks = new Task[numberOfPhilosophers];
            
            for (int i = 0; i < chopsticks.Count; i++)
            {
                // If we tried to use i raw without a local copy,
                // the lambda would evaluate the last value of i + 1
                // which would cause an out of bounds exception.
                // Weird, I know, but more on it here: https://stackoverflow.com/questions/271440/captured-variable-in-a-loop-in-c-sharp
                int ix = i;
                // Using a circular array implementation here to assign the left and right.
                int next = ((ix + 1) % numberOfPhilosophers);
                Console.WriteLine("Hello: {0}, {1}, {2}", ix, next, chopsticks.Count);
                tasks[i] = new Task(() => Eat(chopsticks[ix], chopsticks[next], ix + 1));
            }

            // Start all tasks in parrallel.
            Parallel.ForEach(tasks, t => 
            {
                t.Start();
            });

            // You will notice that each time we will get a random order of execution and no deadlock will happen because
            // of the lock () {} we specified, the next thread will just wait untill the resouce is unlocked before it goes ahead
            // so that not everyone tries to eat at once.
            Task.WaitAll(tasks);
        } 

        public static void Eat(
            object leftChopstick,
            object rightChopstick,
            int philosopherNumber
        )
        {
            lock (leftChopstick) // grab utensil on the left.
            {
                lock (rightChopstick) // grab utensil on the right.
                {
                    // We are eating!
                    Console.WriteLine($"Philosopher {philosopherNumber} is eating.");
                }
            }
        }
    }

}
