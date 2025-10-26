// Entry function.
var shortestSubsequence = function (bigArr, smallArr) {
    // Build the next elements array.
    var nextElements = buildAllNextElements(bigArr, smallArr);
    // Get the closures, closures indicate the max position of the next element present
    var closures = getClosures(nextElements);
    var shortest = getShortestClosure(closures);
    console.log(nextElements);
    console.log(closures);
    return shortest;
};
var buildAllNextElements = function (bigArr, smallArr) {
    var nextElements = [[]];
    for (var i = 0; i < smallArr.length; i++) {
        if (nextElements.length <= i)
            nextElements.push([]);
        nextElements[i] = buildNextElementsArray(bigArr, smallArr[i]);
    }
    return nextElements;
};
// Crawl the big array backwards recording points where the next of the given value is,
// pretty clever!
var buildNextElementsArray = function (bigArr, value) {
    var next = -1;
    var nexts = [];
    for (var i = bigArr.length - 1; i > -1; i--) {
        if (bigArr[i] === value)
            next = i;
        nexts.unshift(next);
    }
    return nexts;
};
var getClosures = function (nextElements) {
    var maxNextElement = [];
    for (var i = 0; i < nextElements[0].length; i++) {
        maxNextElement.push(getClosureForIndex(nextElements, i));
    }
    console.log('Closures');
    console.log(maxNextElement);
    return maxNextElement;
};
var getClosureForIndex = function (nextElements, index) {
    var max = -1;
    for (var i = 0; i < nextElements.length; i++) {
        if (nextElements[i][index] === -1)
            return -1;
        max = Math.max(max, nextElements[i][index]);
    }
    return max;
};
// Further study this function.
var getShortestClosure = function (closures) {
    var bestStart = -1;
    var bestEnd = -1;
    for (var i = 0; i < closures.length; i++) {
        if (closures[i] === -1)
            break;
        var current = closures[i] - i;
        console.log('best start = ', bestStart, 'best end = ', bestEnd, 'current =', current, 'bestEnd - bestStart', bestEnd - bestStart, 'closures[i]', closures[i], 'i', i);
        if (bestStart === -1 || current < (bestEnd - bestStart)) {
            bestStart = i;
            bestEnd = closures[i];
        }
    }
    return [bestStart, bestEnd];
};
var res = shortestSubsequence([7, 5, 9, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 8, 9, 7], [1, 5, 9]);
console.log(res);
