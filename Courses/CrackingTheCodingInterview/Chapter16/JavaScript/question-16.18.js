const getCounts = (pattern) => {
    const charCounts = { };
    for (let i = 0; i < pattern.length; i++) {
        const char = pattern[i];
        if (!charCounts[char]) charCounts[char] = 0;
        charCounts[char] += 1;
    }
    return charCounts;
}

const isEqual = (sOne, offsetOne, offsetTwo, size) => {
    for (let i = 0; i < size; i++) {
        if (sOne.charAt(offsetOne + i) != sOne.charAt(offsetTwo + i)) return false;
    }
    return true;
}

const matches = (pattern, value, mainSize, altSize, firstAlt) => {
    let stringIndex = mainSize;
    for (let i = 1; i < pattern.length; i++) {
        const size = pattern.charAt(i) === pattern.charAt(0) ? mainSize : altSize;
        const offset = pattern.charAt(i) === pattern.charAt(0) ? 0 : firstAlt;
        if (!isEqual(value, offset, stringIndex, size)) return false;
        stringIndex += 1;
    }
    return true;
}

const patternMatch = (pattern, value) => {
    if (pattern === value) return true;
    // Calc what is the main and alternate char.
    const mainChar = pattern.charAt(0);
    const altChar = mainChar === 'a' ? 'b' : 'a'

    const valueSize = value.length;

    // Get the counts of the two chars.
    const counts = getCounts(pattern);
    const countOfMain = counts[mainChar];
    const countOfAlt = counts[altChar];

    // Find the first occurance of the alt char.
    const firstAltIndex = pattern.indexOf(altChar);
    // Find what could be the maximum number of main chars present.
    const maxMainSize = ~~(valueSize / countOfMain);

    for (let mainSize = 0; mainSize <= maxMainSize; mainSize++) {
        const remainingLen = valueSize - mainSize * countOfMain;
        console.log('main size =', mainSize, ' remaining size =', remainingLen)
        // If there are no alts left OR the total count perfectly divides into the remainining indexes.
        if (countOfAlt == 0 || ~~(remainingLen % countOfAlt) === 0) {
            const altIndex = firstAltIndex * mainSize;
            const altSize = countOfAlt === 0 ? 0 : ~~(remainingLen / countOfAlt);
            if (matches(pattern, value, mainSize, altSize, altIndex)) return true;
        }
    }
    return false;
}

console.log(patternMatch('aba', 'catgogo'));