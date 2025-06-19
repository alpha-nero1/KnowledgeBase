namespace question_17._8
{
    public class Person
    {
        public decimal Height { get; }
        public decimal Weight { get; }

        public Person(decimal height, decimal weight)
        {
            Height = height;
            Weight = weight;
        }

        // This function is used to sort a list of people,
        // returning 1 is higher in the list and -1 is lower.
        public int CompareTo(Person other)
        {
            if (Height != other.Height) return (
                Height > other.Height ?
                1 :
                -1
            );
            return (
                Weight > other.Weight ?
                1 :
                -1
            );
        }

        public bool IsBefore(Person other)
        {
            return Height < other.Height && Weight < other.Weight;
        }
    }
}