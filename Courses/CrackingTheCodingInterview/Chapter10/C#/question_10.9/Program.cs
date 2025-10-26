using System;

/*
Sorted Matrix Search: Given an M x N matrix in which each row and each column is sorted in
ascending order, write a method to find an element.
*/
namespace question_10._9
{
    class Program
    {
        static void Main(string[] args)
        {
            // You could do first a binary search on each row as the
            // O notation would be O(n * log (n))

            /*
            col
            [[0, 1, 2]], row
            [[3, 4, 5]],
            [[6, 7, 8]]
            */
        }

        public static int[] FindElementInMatrix(int[][] matrix, int el)
        {
            // Start with the first row and the last column.
            int row = 0;
            int col = matrix[0].Length - 1; // Start with last col.

            // While we have now gone past all rows and gone before all columns.
            while (row < matrix.Length && col >= 0)
            {
                // Lets do a search on [row][col]
                if (matrix[row][col] == el) return new int[] { row, col };
                // If the col value for the row is greater than el then
                // lets go back 1 col to search in the same row.
                if (matrix[row][col] > el)
                {
                    col--;
                    continue;
                }
                // Else lets increment the row, it wasn't found in this col.
                // But we are closer on the column side of the search!
                row++;
            }
            return new int[2] { -1, -1 };
        }

        /*
            The more sophisticated approach would be to execute a binary search on the grid.

            The bottom rightmost element is always the largest number in the matrix.
            And top left will always be the smallest.
        */
        // <param name="origin">The origin of the search (top left most)</param>
        // <param name="dest">The end of the search (bottom right most)</param>
        public static Coordinate FindElement(int[][] matrix, Coordinate origin, Coordinate dest, int x)
        {
            // Initial sanity validation.
            if (!IsValidFindElementCall(matrix, origin, dest)) return null;

            // The found exit.
            if (matrix[origin.row][origin.col] == x) return origin;

            /*
                Set the start of the search to the start of the diagonal, and end to the end of
                the diagonal, Since the grid may not be a perfect square, the end of the diagonal may
                not equal dest.
            */
            // Starting point = origin.
            Coordinate start = (Coordinate) origin.Clone();
            int diagonalDest = Math.Min(dest.row - origin.row, dest.col - origin.col);
            // End point for this recursion is the box that is the box that is made to the dest.
            // that is dest cannot just be dest, it needs to consider diagonal distances in case
            // box is not a square.
            Coordinate end = (Coordinate) new Coordinate(
                start.row + diagonalDest,
                start.col + diagonalDest
            );

            Coordinate search = new Coordinate(0, 0);
            // Do binary search on the diagonal, looking for first el greater than x.
            while (start.IsBefore(end))
            {
                search.SetToAverage(start, end);
                // Keep closing down the search.
                if (x > matrix[search.row][search.col])
                {
                    start.row = search.row + 1;
                    start.col = search.col + 1;
                    continue;
                }
                end.row = search.row - 1;
                end.col = search.col - 1;
            }

            return PartitionAndSearch(matrix, origin, dest, start, x);
        }

        public static bool IsValidFindElementCall(int[][] matrix, Coordinate origin, Coordinate dest) => (
            origin.IsInBounds(matrix)
            && dest.IsInBounds(matrix)
            && origin.IsBefore(dest)
        );

        // <param name="pivot">The result of the initial narrow down search</param>
        public static Coordinate PartitionAndSearch(int[][] matrix, Coordinate origin, Coordinate dest, Coordinate pivot, int x)
        {
            // Because the value can now only be in
            // [1           , upperRight(1)]
            // [lowerLeft(1),             0]
            Coordinate lowerLeftOrigin = new Coordinate(pivot.row, pivot.col);
            Coordinate lowerLeftDest = new Coordinate(dest.row, pivot.col - 1);
            Coordinate upperRightOrigin = new Coordinate(origin.row, pivot.col);
            Coordinate upperRightDest = new Coordinate(pivot.row - 1, dest.col);

            Coordinate lowerLeft = FindElement(matrix, lowerLeftOrigin, lowerLeftDest, x);
            if (lowerLeft == null) return FindElement(matrix, upperRightOrigin, upperRightDest, x);
            return lowerLeft;
        }

        public static Coordinate FindElement(int[][] matrix, int x, bool isSecondMethod = true)
        {
            Coordinate origin = new Coordinate(0, 0);
            Coordinate dest = new Coordinate(matrix.Length - 1, matrix[0].Length - 1);
            return FindElement(matrix, origin, dest, x);
        }
    }
}
