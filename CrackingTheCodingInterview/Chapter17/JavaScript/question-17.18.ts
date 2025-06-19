/*
    17.18. Shortest Subsequence.
*/

// Entry function.
const shortestSubsequence = (bigArr, smallArr) => {
    // Build the next elements array.
    const nextElements = buildAllNextElements(bigArr, smallArr);
    // Get the closures, closures indicate the max position of the next element present
    const closures = getClosures(nextElements);
    let shortest = getShortestClosure(closures);
    return shortest;
}

const buildAllNextElements = (bigArr, smallArr) => {
    const nextElements: number[][] = [[]];
    for (let i = 0; i < smallArr.length; i++) {
        if (nextElements.length <= i) nextElements.push([]);
        nextElements[i] = buildNextElementsArray(bigArr, smallArr[i]);
    }
    return nextElements;
}

// Crawl the big array backwards recording points where the next of the given value is,
// pretty clever!
const buildNextElementsArray = (bigArr, value) => {
    let next = -1;
    const nexts: number[] = [];
    for (let i = bigArr.length - 1; i > -1; i--) {
        if (bigArr[i] === value) next = i;
        nexts.unshift(next);
    }
    return nexts;
}

const getClosures = (nextElements) => {
    const maxNextElement: number[] = [];
    for (let i = 0; i < nextElements[0].length; i++) {
        maxNextElement.push(getClosureForIndex(nextElements, i));
    }
    return maxNextElement;
}

const getClosureForIndex = (nextElements, index): number => {
    let max = -1;
    for (let i = 0; i < nextElements.length; i++) {
        if (nextElements[i][index] === -1) return -1;
        max = Math.max(max, nextElements[i][index]);
    }
    return max;
}

// Actually a very brilliant function.
// At each index we are able to calculate the diff (i value - i)
// this ends up giving us how many indexes till the next closure.
// if that is smaller than the current best difference,
// then i and closures[i] become the best values.
// Very hard to understand at first but it is marvelous!
const getShortestClosure = (closures): number[] => {
    let bestStart = -1;
    let bestEnd = -1;

    for (let i = 0; i < closures.length; i++) {
        if (closures[i] === -1) break;
        const current = closures[i] - i;
        const bestDiff = bestEnd - bestStart;
        if (bestStart === -1 || current < bestDiff) {
            bestStart = i;
            bestEnd = closures[i];
        }
    }
    return [bestStart, bestEnd];
}

const res = shortestSubsequence(
    [7, 5, 9, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 8, 9, 7],
    [1, 5, 9]
);

console.log(res);