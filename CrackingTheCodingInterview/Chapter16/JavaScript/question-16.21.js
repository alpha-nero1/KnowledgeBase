const getTarget = (sumOne, sumTwo) => {
    // There are no valid pairs if the diff is not even
    if ((sumOne - sumTwo) % 2 != 0) return null;
    return ~~((sumOne - sumTwo) / 2);
}

const sumSwap = (arrOne, arrTwo) => {
    const sumOne = arrOne.reduce((a, b) => a + b);
    const sumTwo = arrTwo.reduce((a, b) => a + b);
    console.log(sumOne, sumTwo)
    const target = getTarget(sumOne, sumTwo);
    if (target === null) return null;

    const arrTwoValSet = new Set();
    arrTwo.forEach((el, i) => {
        arrTwoValSet.add(el);
    })

    for (let i = 0; i < arrOne.length; i++) {
        const one = arrOne[i];
        const two = one - target;
        console.log(one, two, target)
        if (arrTwoValSet.has(two)) {
            return [one, two];
        }
    }
    return null;
}


console.log(sumSwap([3, 6, 3, 3], [4, 1, 2, 1, 1, 2]));