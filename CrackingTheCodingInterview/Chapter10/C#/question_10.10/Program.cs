using System;

namespace C_
{
    class Program
    {
        static void Main(string[] args)
        {
            var rankedStream = new RankedStream();
            rankedStream.Add(1);
            rankedStream.Add(2);
            rankedStream.Add(10);
            rankedStream.Add(3);
            rankedStream.Add(5);
            rankedStream.Add(7);
            Console.WriteLine("Rank of 5 is: {0}", "" + rankedStream.GetRankOf(5));
        }
    }

    public class RankedNode
    {
        public int LeftSize { get; set; }
        public RankedNode Left, Right;
        public int value { get; set; }

        public RankedNode(int v)
        {
            value = v;
        }

        // Insertion that recursively follows left and right chain,
        // Each node records how many elements are on the left of it
        // (how many are smaller)
        // Goodness this is brilliant right here!
        public void Insert(int newValue)
        {
            if (newValue <= value)
            {
                if (Left != null) Left.Insert(newValue);
                else Left = new RankedNode(newValue);
                LeftSize += 1;
                return;
            }
            if (Right != null) Right.Insert(newValue);
            else Right = new RankedNode(newValue);
        }

        public int GetRank(int v)
        {
            if (value == v) return LeftSize;
            if (v < value)
            {
                if (Left == null) return -1;
                return Left.GetRank(v);
            }
            // Else if the value was on the right side, it acts slightly differently.
            if (Right == null) return -1;
            int rightRank = Right.GetRank(v);
            if (rightRank == -1) return -1;
            // We need to consume whatever Get rank on the right returned, adding our
            // current size to it + 1.
            return LeftSize + 1 + rightRank;
        }
    }

    /// <summary>
    /// Main tree-like class.
    /// </summary>
    public class RankedStream
    {
        RankedNode _root = null;
        public void Add(int value)
        {
            if (_root == null)
            {
                _root = new RankedNode(value);
                return;
            }
            _root.Insert(value);
        }

        public int GetRankOf(int value)
        {
            if (_root == null) return -1;
            return _root.GetRank(value);
        }
    }
}
