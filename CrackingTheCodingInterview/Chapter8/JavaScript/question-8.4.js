// Will be honest I have no idea what is going on here or
// what we are even solving for.
function getSubsets(set, index = 0) {
    let allSubsets = [];

    if (set.length === index) {
        // Base case - add an empty set.
        allSubsets = [[]];
    } else {
        allSubsets = getSubsets(set, index + 1);
        const item = set[index];
        const moreSubsets = [];
        for (const subset in allSubsets) {
            const newSubset = [...subset, item];
            moreSubsets.push(newSubset);
        }
        allSubsets = [...allSubsets, ...moreSubsets]
    }
    return allSubsets;
}

const subs = getSubsets([1, 2]);
console.log('subs are: ', subs)