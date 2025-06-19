// Rotate Matrix.

// Got from: https://medium.com/@justinminh225/cracking-the-coding-interview-rotate-matrix-79f0d61dc27f
// I find it easier to understand..
const rotateMatrix = function(matrix) {  
  const results = [];
  const cols = matrix.length;
  // we use cols for both loops because we are looking at this from a  
  // column and row perspective rather than nested arrays
  for (let col = 0; col < cols; col++) {
    const inner = [];
    for (let row = 0; row < cols; row++) {
      const item = matrix[row][col];
      inner.unshift(item);
    }
    results.push(inner);
  }
  results.forEach(item => console.log(item))
  return results;
}

rotateMatrix([
  [0, 1, 2, 3],
  [4, 5, 6, 7],
  [8, 9, 10, 11],
  [12, 13, 14, 15],
]);