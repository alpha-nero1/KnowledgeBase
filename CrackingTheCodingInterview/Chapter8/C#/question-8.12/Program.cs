using System.Text.RegularExpressions;
using System;

namespace question_8._12
{
    class Program
    {
        private static int GRID_SIZE = 8;

        static void Main(string[] args)
        {
            var queens = PlaceQueens(0, new int[8]);
            foreach (var q in queens)
            {
                Console.WriteLine($"Hello queen: {q}");
            }
        }

        static int[] PlaceQueens(int row, int[] columns)
        {
            if (row == GRID_SIZE) return columns; // If you reached the end then escape...
            // Loop the columns in the recursive call for row.
            for (int col = 0; col < GRID_SIZE; col++)
            {
                // Easy way of checking each col spot for each row.
                if (CheckValid(columns, row, col)) {
                    // If you pass the checks, all good to place a queen.
                    columns[row] = col;
                    // You succeeded for this row, check the next one.
                    PlaceQueens(row + 1, columns);
                }
            }
            return columns;
        }

        // Check if it is okay to place on a particular spot.
        static bool CheckValid(int[] columns, int rowToPlace, int colToPlace)
        {
            // Loop the old part of the grid to see if the new value is valid...
            for (int pastRow = 0; pastRow < rowToPlace; pastRow++)
            {
                // Get the column of the current past row selected.
                int pastCol = columns[pastRow];
                // If same col as one to place, invalid.
                if (colToPlace == pastCol) return false;
                // get the sides of the triangle.
                int colDistance = Math.Abs(pastCol - colToPlace);
                int rowDistance = rowToPlace - pastRow;
                // If col distance = row distance, the triangle is made and we back out.
                if (colDistance == rowDistance) return false;
            }
            // If we have covered the column and diagonal use case, you are good to place!!
            return true;
        }
    }
}
