using System;
using System.Collections;

namespace question_6._10
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList bottles = new ArrayList();
            ArrayList strips = new ArrayList();
            for (int i = 0; i < 1000; i++) 
            {
                Bottle b = new Bottle(i);
                if (i == 871) {
                    b.IsPoisoned = true;
                }
                bottles.Add(b);
            }
            for (int i = 0; i < 10; i++)
            {
                TestStrip strip = new TestStrip(i);
                strips.Add(strip);
            }

            int Bottle = FindPoisonedBottle(bottles, strips);
            Console.WriteLine($"FindPoisonedBottle (871) out of {bottles.Count} = {Bottle}");
        }

        static int FindPoisonedBottle(ArrayList bottles, ArrayList strips)
        {
            RunTests(bottles, strips);
            ArrayList positive = GetPositiveOnDay(strips, 7);
            return SetBits(positive);
        }

        static void RunTests(ArrayList bottles, ArrayList strips)
        {
            foreach (Bottle bottle in bottles)
            {
                int Id = bottle.GetId();
                int BitIndex = 0;
                // Really good algorithm for checking the individual
                // bits in an int and also considering the bit index!!!
                while (Id > 0)
                {
                    if ((Id & 1) == 1)
                    {
                        ((TestStrip)strips[BitIndex]).AddDropOnDay(0, bottle);
                    }
                    BitIndex++; // Increment bit index so we know what strip to put the
                    // next drop on if a 1 is present in its' position.
                    // Condition breaker...
                    Id >>= 1;
                }
            }
        }

        static ArrayList GetPositiveOnDay(ArrayList strips, int Day)
        {
            ArrayList Positive = new ArrayList();
            foreach (TestStrip strip in strips)
            {
                int Id = strip.GetId();
                if (strip.IsPositiveOnDay(Day))
                {
                    Positive.Add(Id);
                }
            }
            return Positive;
        }

        static int SetBits(ArrayList Positive)
        {
            int Id = 0;
            foreach (int BitIndex in Positive)
            {
                // Build the int using the indexes of the positive strips.
                Id |= 1 << BitIndex;
            }
            return Id;
        }
    }
}
