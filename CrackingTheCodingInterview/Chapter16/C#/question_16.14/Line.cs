using System;

namespace question_16._14
{
    public class Point
    {
        public int x { get; }
        public int y { get; }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Line
    {
        public static double Epsilon = .0001;
        public double Slope, Intercept;
        public bool InfiniteSlope;

        public Line(Point p1, Point p2)
        {
            if (Math.Abs(p1.x - p2.x) > Epsilon)
            {
                // If the x's are diff.
                Slope = (p1.y - p2.y) / (p1.x - p2.x);
                // Intercept y=mx+b
                Intercept = p1.y - Slope * p1.x;
            }
            else
            {
                InfiniteSlope = true;
                Intercept = p1.x;
            }
        }

        public static double FloorToNearestEpsilon(double d)
        {
            int r = (int) (d / Epsilon);
            return ((double) r) * Epsilon;
        }

        public bool IsEquivalent(double a, double b)
        {
            return Math.Abs(a - b) < Epsilon;
        }

        public bool IsEquivalent(Line other)
        {
            return (
                IsEquivalent(other.Slope, Slope)
                && IsEquivalent(other.Intercept, Intercept)
                && InfiniteSlope == other.InfiniteSlope
            );
        }
    }
}