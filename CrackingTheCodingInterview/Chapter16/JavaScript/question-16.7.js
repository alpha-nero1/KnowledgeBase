/*
    16.7. Number Max.
*/

// Flips a 1 to 0 and vice versa.
const flip = (bit) => {
    return 1^bit;
}

// returns 1 if positive, 0 if negative.
const sign = (a) => {
    const thirtySecondBit = a >> 31;
    return flip(thirtySecondBit & 0x1);
}

const maxWithoutIfElse = (a, b) => {
    const c = a - b;
    // So may be 1, 1, 0
    const signs = [sign(a), sign(b), sign(c)];
    // Define a value of k which will be 1 if a > b and 0 if a < b
    // if a = b then the value of k will be irrelevant.
    // 1 ^ 1 = 0, 1 ^ 0 = 1
    const signAIsLarger = signs[0] ^ signs[1];
    const signBIsLarger = flip(signAIsLarger);

    const leftSideEliminator = signAIsLarger * signs[0] * signBIsLarger * signs[2];
    const rightSideEliminator = flip(leftSideEliminator);

    return (a * leftSideEliminator) + (b * rightSideEliminator)
}

console.log(maxWithoutIfElse(15, 25))