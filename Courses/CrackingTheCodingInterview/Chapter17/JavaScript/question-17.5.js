const lettersAndNumbers = (arr) => {
    const isNum = (val) => !isNaN(+val);

    const computeDeltaArray = (subArr) => {
        let delta = 0;
        const retArr = []
        for (let i = 0; i < subArr.length; i++) {
            if (isNum(subArr[i])) delta -= 1;
            else delta += 1;
            retArr.push(delta);
        }
        return retArr;
    }

    const map = new Map();
    const deltas = computeDeltaArray(arr);

    map.set(0, -1);
    const maxes = [0, 0];
    console.log(deltas);

    for (let i = 0; i < deltas.length; i++) {
        const delt = deltas[i];
        if (!map.has(delt)) {
            map.set(delt, i);
            continue;
        }
        const match = map.get(delt);
        const distance = i - match;
        const longest = maxes[1] - maxes[0];
        console.log('longest', longest, maxes, distance)
        if (distance > longest) {
            maxes[1] = i;
            maxes[0] = match;
        }
    }
    return arr.splice(maxes[0], maxes[1]);
}

console.log(lettersAndNumbers(['a', 'a', 'a', 'a', 1, 1, 'a', 1, 1, 'a', 'a', 1, 'a', 'a', 1, 'a', 'a', 'a', 'a', 'a']));
