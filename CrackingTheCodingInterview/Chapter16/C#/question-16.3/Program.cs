using System;

namespace question_16._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Point sa = new Point(0, 0);
            Point ea = new Point(8, 8);
            Point sb = new Point(1, 0);
            Point eb = new Point(8, 12);
            Point inter = GetIntersection(sa, ea, sb, eb);
            if (inter != null)
            {
                Console.WriteLine("Found intersection {0}, {1}", inter.x, inter.y);
            }
            else
            {
                Console.WriteLine("No intersection found :(");
            }
        }

        static Point GetIntersection(Point startA, Point endA, Point startB, Point endB)
        {
            // This first section ensures that an intersection indeed can happen
            // even if input data is bad.
            if (startA.x > endA.x) Swap(startA, endA);
            if (startB.x > endB.x) Swap(startB, endB);
            if (startA.x > startB.x)
            {
                Swap(startA, startB);
                Swap(endA, endB);
            }

            Line lineA = new Line(startA, endA);
            Line lineB = new Line(startB, endB);

            if (lineA.slope == lineB.slope)
            {
                // If the two lines are parralell (same slope) they intercept
                // only if they have the same y intercepts and start b is on line a
                if (
                    lineA.yintercept == lineB.yintercept &&
                    IsBetween(startA, startB, endA)
                ) return startB;
                return null;
            }

            // Get intersection...
            double x = (lineB.yintercept - lineA.yintercept) / (lineA.slope - lineB.slope);
            double y = x * lineA.slope + lineA.yintercept;
            Point intersection = new Point(x, y);

            // Check if in segment range.
            if (
                IsBetween(startA, intersection, endA) &&
                IsBetween(startB, intersection, endB)
            ) return intersection;

            return null;
        }

        // Check if middle is between start and end.
        static bool IsBetween(double start, double middle, double end)
        {
            if (start > end) return end <= middle && middle <= start;
            return start <= middle && middle <= end;
        }

        // Check if middle point is between start and end points...
        static bool IsBetween(Point start, Point middle, Point end)
        {
            return (
                IsBetween(start.x, middle.x, end.x) &&
                IsBetween(start.y, middle.y, end.y)
            );
        }

        static void Swap(Point a, Point b) {
            double ax = a.x;
            double ay = a.y;
            a.SetLocation(b.x, b.y);
            a.SetLocation(ax, ay);
        }
    }

    public class Line
    {
        public double _slope, _yintercept;

        public double slope { get { return _slope; } }
        public double yintercept { get { return _yintercept; } }

        public Line(Point start, Point end)
        {
            double deltaY = end.y - start.y;
            double deltaX = end.x - start.x;

            _slope = deltaY / deltaX;
            _yintercept = end.y - _slope * end.x;
        }
    }

    public class Point
    {
        public double _x, _y;

        public double x { get { return _x; } }
        public double y { get { return _y; } }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public void SetLocation(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }
}
