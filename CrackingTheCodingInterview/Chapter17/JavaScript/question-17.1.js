// More on half adders here: https://tinyurl.com/2pst34jn
// A half adder is doing "Long Addition"
const halfAdder = (a, b) => {
    if (b === 0) return a;
    console.log(`adding ${a} + ${b}`)
    // add without carrying. Finds the difference.
    // This is exactly how you would do "Long Addition" in school.
    // So for instance 7 + 8 = 5 (we later carry the 10 to the next left digit)
    const carry = a ^ b;
    console.log(`carrying ${carry}`)
    // Because we know that b is not 0, let's move spots left by one
    // of the ADDED a & b.
    const sum = (a & b) << 1; // carry but don't add.
    console.log(`sum ${sum}`)
    return halfAdder(carry, sum);
}

console.log('halfAdder(23, 56): ', halfAdder(23, 56),'\n')
console.log('halfAdder(28, 31): ', halfAdder(28, 31),'\n')
console.log('halfAdder(4, 5): ', halfAdder(4, 5),'\n')
console.log('halfAdder(7, 8): ', halfAdder(7, 8),'\n')
console.log('halfAdder(17, 108): ', halfAdder(17, 108),'\n')

// Example run
/**
    adding 23 + 56
    carrying 47
    sum 32 // NOTICE: base2
    adding 47 + 32
    carrying 15
    sum 64 // NOTICE: base2
    adding 15 + 64
    carrying 79
    sum 0 // NOTICE: base2
    halfAdder(23, 56):  79
 */