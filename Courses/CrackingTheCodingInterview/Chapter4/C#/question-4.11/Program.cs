using System;

namespace question_4._11
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryNode root = new BinaryNode(10);
            root.insert(2);
            root.insert(3);
            root.insert(4);
            root.insert(5);
            root.insert(6);
            BinaryNode rand = root.getRandomNode();
            Console.WriteLine($"found rand: {rand.value}");
        }
    }
}
