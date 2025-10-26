/*
    16.22. Pairs with sum.
*/

// Simply increment/decrement the count in the mapping.
const incrementCount = (map, key, delta) => {
    if (!map) return;
    map.set(
        key,
        (map.get(key) || 0) + delta
    );
}

// Find all pairs in the array that amount to the sum.
const pairsWithSum = (arr, sum) => {
    const pairs = [];
    if (!arr || !arr.length) return pairs;
    const unpairedCount = new Map();
    // Loop the array and perform a counter for the number;

    for (let i = 0; i < arr.length; i++) {
        const num = arr[i];
        const complement = sum - num;
        // No complement was found in previous counts, just increment the count for this number.
        if (unpairedCount.get(complement) < 1) {
            incrementCount(unpairedCount, num, 1);
            continue;
        }
        // Hey we detected a pair! add the pair and decrement the count.
        pairs.push([num, complement])
        incrementCount(unpairedCount, complement, -1);
    }
    return pairs;
}


console.log(pairsWithSum([-2, -1, 0, 3, 5, 6, 7, 9, 13, 14 ], 12));