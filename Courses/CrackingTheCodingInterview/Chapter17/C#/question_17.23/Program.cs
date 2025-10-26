/*
    17.23. Max square matrix. Imagine you have a square matrix, where each cell (pixel) is either black or white.
    Design an algorithm to find the maximum subsquare such that all four borders are filled with black pixels.

    To clear up any confusion, our definition of matrix looks like:

    [
        [0, 1, 1, 0]
        [1, 1, 1, 0]
        [0, 0, 0, 1]
        [0, 0, 0, 0]
    ]

    if we loop this data structure via the first layer, we get each row
    [0, 1, 1, 0] then [1, 1, 1, 0] then [0, 0, 0, 1] e.t.c.

    if we further loop inside this we get the column element.

    This algorithm is O(n^4)
*/

using question_17._23;

var square = FindSquare(new int[][]
{
    new int[] { 0, 1, 1, 0 },
    new int[] { 1, 1, 1, 0 },
    new int[] { 0, 0, 0, 1 },
    new int[] { 0, 0, 0, 0 },
});

Console.WriteLine(square);

// Brute force.
Square FindSquare(int[][] matrix)
{
    for (int i = matrix.Length; i > 0; i--)
    {
        Square square = FindSquareWithSize(matrix, i);
        if (square != null) return square;
    }
    return null;
}

Square FindSquareWithSize(int[][] matrix, int squareSize)
{
    int count = matrix.Length - (squareSize + 1);

    for (int row = 0; row < count; row++)
    {
        for (int col = 0; col < count; col++)
        {
            if (IsSquare(matrix, row, col, squareSize)) return new Square(row, col, squareSize);
        }
    }

    return null;
}

bool IsSquare(int[][] matrix, int row, int col, int squareSize)
{
    // Check top and bottom border
    for (int i = 0; i < squareSize; i++)
    {
        // Check top border;
        if (matrix[row][col+i] == 1) return false;
        // Check bottom border.
        if (matrix[row + (squareSize - 1)][col+i] == 1) return false;
    }

    // Check left and right border.
    for (int j = 0; j < squareSize - 1; j++)
    {
        // Check left border.
        if (matrix[row + j][col] == 1) return false;
        // Check right border.
        if (matrix[row + j][col + (squareSize - 1)] == 1) return false;
    }

    return true;
}
