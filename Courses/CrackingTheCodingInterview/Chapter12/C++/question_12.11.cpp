/*
    12.11. 2D Alloc.
    Write a function in C called my2DA1loc which allocates a two-dimensional array.
    Minimize the number of calls to malloc and make sure that the memory is accessible by the
    notation arr [ i] [ j].
*/

int** my2DA1loc(int rows, int cols) {
    int i;
    int header = rows * sizeof(int*); // sizeof(int*) returns the size of int*
    int data = rows * cols * sizeof(int);
    int** rowptr = (int**)malloc(header + data);
    if (rowptr == NULL) return NULL;

    int* buf = (int*) (rowptr + rows);
    for (i = 0; i < rows; i++) {
        rowptr[i] = buf + i * cols;
    }
    return rowptr;
}