/*
    17.15. Longest Word.
*/

const getLongestWord = (arr: string[]) => {
    let words = [...arr];
    const wordMap = new Map<string, boolean>();
    for (let word in words) {
        wordMap.set(word, true)
    }

    words = words.sort((a, b) => a.length - b.length);

    for (let word in words) {
        if (canBuildWord(word, true, wordMap)) return word;
    }

    return '';
}

const canBuildWord = (word: string, isOriginalWord: boolean, map: Map<string, boolean>) => {
    if (map.has(word) && !isOriginalWord) return true;

    for (let i = 0; i < word.length; i++) {
        const left = word.substring(0, i);
        const right = word.substring(i);

        if (map.has(left) && map.get(left) && canBuildWord(right, false, map)) return true;
    }

    // This word has been eliminated, remove from set.
    map.set(word, false);
    return false;
}


console.log(getLongestWord(['dog', 'kennel', 'big', 'crate', 'bigkennel', 'bigcrate', 'bigdogkennelcrate']))