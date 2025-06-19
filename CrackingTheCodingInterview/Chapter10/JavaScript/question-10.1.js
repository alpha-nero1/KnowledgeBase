function boostrapArray(elCount, fillWithIndex) {
    const arr = [];
    for (let i = 0; i < elCount; i++) {
        arr.push(fillWithIndex ? i : 0)
    }
    return arr;
}

// Merge two sorted arrays a and b, b fits into a.
function mergeArrays(arrA, arrB) {
    const lastA = arrA.length - 1;
    const lastB = arrB.length - 1;
    let indexMerged = lastB + lastA - 1;
    // Start from the very last indeces.
    let indexA = lastA - 1;
    let indexB = lastB - 1;
    // Remember that b is fitting into A.
    while (indexB > - 1) {
        if (indexA >= 0 && arrA[indexA] > arrB[indexB]) {
            console.log('aa arrA', arrA[indexA], 'is bigger than', arrB[indexB])
            arrA[indexMerged] = arrA[indexA]
            indexA -= 1;
        } else {
            console.log('aa arrB', arrB[indexB], 'is bigger or same than', arrA[indexA])
            arrA[indexMerged] = arrB[indexB]
            indexB -= 1;
        }
        indexMerged -= 1;
    }

    return arrA;
}

const merged = mergeArrays(
    boostrapArray(20, true),
    boostrapArray(5, true),
);

console.log('merged two arrays to become: ', JSON.stringify(merged), merged.length);