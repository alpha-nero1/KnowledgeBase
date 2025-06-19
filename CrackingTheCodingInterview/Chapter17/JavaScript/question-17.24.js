/*
    17.24. Given an NxN matrix of positive and negative integers, write code to find
    the submatrix with the largest possible sum.
*/

const grid = [
    [0, 1, 2, 3],
    [3, 2, 2, 2],
    [1, 4, 5, 1],
    [0, 1, 1, 1]
];

// This brute force solution is O(n^6)!
const bruteForce = (matrix) => {
    const rowCount = matrix.length;
    const colCount = rowCount;

    const getSum = (startRow = 0, startCol = 0, endRow = 0, endCol = 0) => {
        let sum = 0;
        const toRow = endRow;
        const toCol = endCol;
        for (let row = startRow; row < toRow; row++) {
            for (let col = startCol; col < toCol; col++) {
                if (row >= matrix.length) return sum;
                const num = matrix[row][col];
                sum += num;
            }
        }
        return sum;
    }

    let bestSum = 0;

    // Holy moly!
    for (let row = 0; row < rowCount; row++) {
        for (let endRow = 0; endRow < rowCount; endRow++) {
            for (let col = 0; col < colCount; col++) {
                for (let endCol = 0; endCol < colCount; endCol++) {
                    const sum = getSum(row, col, endRow, endCol);
                    if (sum > bestSum) bestSum = sum;   
                }
            }
        }
    }

    return bestSum;
}

console.log('brute force');
console.log(bruteForce(grid));

const range = (start, end, sum) => {
    return {
        start,
        end,
        sum
    };
}

const subMatrix = (rowStart, colStart, rowEnd, colEnd, sum) => {
    return {
        rowStart,
        colStart,
        rowEnd,
        colEnd,
        sum
    };
}

const optimal = (matrix) => {

    const getMaxSubArray = (arr, n) => {
        let bst = null;
        let start = 0;
        let sum = 0;

        for (let i = 0; i < n; i++) {
            sum += arr[i];
            if (bst === null || sum > bst.sum) {
                bst = range(start, i, sum);
            }

            if (sum < 0) {
                start = i + 1;
                sum = 0;
            }
        }

        return bst;
    }

    const getMaxMatrix = () => {
        const rowCount = matrix.length;
        const colCount = rowCount;

        let best = null;

        for (let rowStart = 0; rowStart < rowCount; rowStart++) {
            const partialSum = new Array(colCount);

            for (let rowEnd = rowStart; rowEnd < rowCount; rowEnd++) {
                // Add values at row end...
                for (let i = 0; i < colCount; i++) {
                    partialSum[i] += matrix[rowEnd, i];
                }

                let nextRange = getMaxSubArray(partialSum, colCount);

                if (best == null || best.sum < nextRange.sum) {
                    best = subMatrix(rowStart, nextRange.start, rowEnd, nextRange.end, nextRange.sum)
                }
            }
        }

        return best;
    }

    return getMaxMatrix();
}

console.log('optimal');
console.log(optimal(grid));
