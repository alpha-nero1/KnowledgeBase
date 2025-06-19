// Zero matrix. O(n^2)

const zeroMatrixRow = (row, matrix) => {
  for (let i = 0; i < matrix[row].length; i++) {
    matrix[row][i] = 0;
  }
}

const zeroMatrixCol = (col, matrix) => {
  for (let rowInCol = 0; rowInCol < matrix.length; rowInCol++) {
    matrix[rowInCol][col] = 0;
  }
}

const zeroMatrix = matrix => {
  if (matrix && matrix.length) {
    const zeroableRows = [];
    const zeroableColumns = [];
    for (let row = 0; row < matrix.length; row++) {
      const thisMatrix = matrix[row];
      for (let col = 0; col < thisMatrix.length; col++) {
        if (matrix[row][col] === 0) {
          zeroableRows.push(row);
          zeroableColumns.push(col)
        }
      }
    }
    for (let i = 0; i < zeroableRows.length; i++) {
      zeroMatrixRow(zeroableRows[i], matrix);
    }
    for (let i = 0; i < zeroableColumns.length; i++) {
      zeroMatrixCol(zeroableColumns[i], matrix);
    }
  }
  return matrix;
}

const m = [
  [0, 1, 2, 3],
  [4, 5, 0, 7],
  [8, 9, 10, 11],
  [12, 13, 14, 15],
];

console.log('\nbefore: ')
m.forEach(item => console.log(item))

zeroMatrix(m);

console.log('\nafter: ')
m.forEach(item => console.log(item))