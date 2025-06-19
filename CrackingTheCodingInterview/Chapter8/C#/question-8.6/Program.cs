using System.Security.AccessControl;
using System;
using System.Collections.Generic;

namespace question_8._6
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Tower(0);
            var t2 = new Tower(0);
            var t3 = new Tower(0);
            t1.Add(4);
            t1.Add(3);
            t1.Add(2);
            t1.Add(1);
            t1.MoveDisks(4, t3, t2);
            Console.WriteLine("tower one: {0}", t1);
            Console.WriteLine("tower two: {0}", t2);
            Console.WriteLine("tower three: {0}", t3);
        }
    }

    public class Tower
    {
        private Stack<int> _disks;
        private int _index;

        public int index {
            get {
                return _index;
            }
        }
        
        public Tower(int i)
        {
            _disks = new Stack<int>();
            _index = i;
        }

        public void MoveDisks(int n, Tower dest, Tower buffer)
        {
            if (n <= 0) return; // We are moving no disks, so return.
            MoveDisks(n - 1, buffer, dest); // But all disks but the last one on the buffer.
            MoveTopTo(dest); // Move the biggest one to the destination.
            buffer.MoveDisks(n - 1, dest, this); // Move all disks from the buffer to the destination.
        }

        public void Add(int d)
        {
            if (_disks.Count > 0 && _disks.Peek() <= d) {
                Console.WriteLine("Err placing disk {0}", d);
            } else {
                // The disk we are placing is of a lower value than the current highest one...
                _disks.Push(d);
            }
        }

        public void MoveTopTo(Tower t)
        {
            int top = _disks.Pop();
            t.Add(top);
        }

        public override string ToString()
        {
            var str = "";
            foreach (int d in _disks)
            {
                str += $"{d},";
            }
            return str;
        }
    }
}
