/*
    17.8 Circus Tower
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace question_17._8
{
    class Program
    {
        static void Main(string[] args)
        {
            var best = LongestSequence(new List<Person> 
            {
                new Person(180, 70),
                new Person(150, 50),
                new Person(170, 80),
                new Person(160, 70),
                new Person(140, 60),
                new Person(150, 72),
                new Person(190, 90),
                new Person(190, 90),
                new Person(182, 92),
                new Person(172, 84),
                new Person(190, 91),
            });

            for (int i = 0; i < best.Count; i++)
            {
                Console.WriteLine("Person ({0}, {1})", best[i].Height, best[i].Weight);
            }
        }

        #region Main algorithm

        // Returns a list where both h and w are greater than the last item.        
        static List<Person> LongestSequence(List<Person> people)
        {
            people.Sort((a, b) => a.CompareTo(b));

            List<List<Person>> solutions = new List<List<Person>>();
            List<Person> bestSequence = new List<Person>();

            for (int i = 0; i < people.Count; i++)
            {
                List<Person> bestAtIndex = BestSequenceAtIndex(people, solutions, i);
                solutions.Add(bestAtIndex);
                Console.WriteLine("New solution from {0}", i);
                PrintPeople(bestAtIndex);
                bestSequence = Max(bestSequence, bestAtIndex);
            }

            return bestSequence;
        }

        static List<Person> BestSequenceAtIndex(List<Person> people, List<List<Person>> solutions, int index)
        {
            var person = people[index];
            List<Person> bestSequence = new List<Person>();

            // Find the longest sequence that we can append this person to.
            for (int i = 0; i < index; i++)
            {
                // See if we can append ourself to this solution...
                var solution = solutions[i];
                if (CanAppend(solution, person)) bestSequence = Max(bestSequence, solution);
            }

            // Append ourself to the best solution...
            var best = new List<Person>(bestSequence);
            best.Add(person);
            return best;
        }

        #endregion

        #region Static helper functions.

        static void PrintPeople(List<Person> people)
        {
            Console.Write("[");
            Console.WriteLine(String.Join("", people.Select(x => $"\n  Person ({x.Height}, {x.Weight}),")));
            Console.WriteLine("]");
        }

        static bool CanAppend(List<Person> solution, Person person)
        {
            if (solution == null) return false;
            if (solution.Count < 1) return true;
            Person last = solution.Last();
            return last.IsBefore(person);
        }

        static List<Person> Max(List<Person> first, List<Person> second)
        {
            if (first == null) return second;
            if (second == null) return first;
            return (
                first.Count > second.Count ?
                first :
                second
            );
        }

        #endregion
    }
}
