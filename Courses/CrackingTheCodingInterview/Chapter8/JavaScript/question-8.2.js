const { fill2DArray } = require('../../Core/JavaScript/array-utils');

const blockedPieces = fill2DArray(8, 8, 0);

blockedPieces[4][4] = 1;
blockedPieces[4][5] = 1;
blockedPieces[6][6] = 1;
blockedPieces[0][7] = 1;
blockedPieces[7][0] = 1;
blockedPieces[7][1] = 1;
blockedPieces[1][0] = 1;
blockedPieces[2][0] = 1;

const move = (c, r, it, tc, tr) => {
    console.log('pos: (x: ' + c + ', y: ' + r + ')');
    if (c === tc && r === tr) return it;
    const hitBlock = (blockedPieces[c][r] === 1 || c >= tc);
    return hitBlock ?
        move(c, r + 1, it + 1, tc, tr) :
        move(c + 1, r, it + 1, tc, tr)
} 

const findPath = (tc = 0, tr = 0) => {
    console.log('navigating');
    console.log(blockedPieces);
    return move(0, 0, 0, tc, tr);
}

const steps = findPath(7, 7)

console.log('steps: ', steps);