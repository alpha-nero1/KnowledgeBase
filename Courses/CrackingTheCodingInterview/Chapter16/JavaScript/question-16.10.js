/*
    16.10. Living people.
    Which year has the most concurrent living people.
*/

const people = [
    [1912, 1999],
    [1965, Number.MAX_SAFE_INTEGER],
    [1997, Number.MAX_SAFE_INTEGER],
    [1930, 2006],
    [1910, 2001]
]

const sortYears = (ppl, yearType = 0) => {
    let years = [];
    if (yearType === 0) years = ppl.map(p => p[0]);
    if (yearType === 1) years = ppl.map(p => p[1]);
    return years.sort();
}

const maxAliveYear = (ppl, min, max) => {
    // Sort the births and pass aways, arrays WILL be same lenght,
    // if someone has not passed, then pass date will be max int value.
    const births = sortYears(ppl, 0);
    const passes = sortYears(ppl, 1);

    let birthIndex = 0;
    let passIndex = 0;
    let currentlyAlive = 0;
    let maxAlive = 0;
    let maxAliveYear = min;

    // We are going off of birth date because this would represent
    // adding 1 to the count of currently alive.
    while (birthIndex < births.length) {
        // If the birth value is less than the associated path value,
        // then we have one more currently alive!
        if (births[birthIndex] < passes[passIndex]) {
            currentlyAlive += 1;
            // If current alive is larger than max alive, increment it!
            if (currentlyAlive > maxAlive) {
                maxAlive = currentlyAlive;
                // And record the year!
                maxAliveYear = births[birthIndex];
            }
            birthIndex += 1;
            continue;
        }
        if (births[birthIndex] > passes[passIndex]) {
            // Decrement currently alive and increment the pass index.
            currentlyAlive -= 1;
            passIndex += 1;
        }
    }
    return maxAliveYear;
}

console.log('Max alive year', maxAliveYear(people))