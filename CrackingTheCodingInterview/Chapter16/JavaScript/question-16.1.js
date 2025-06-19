const nums = [0, 1, 2, 3];

const swapInPlace = (arr, i, j) => {
    // Set i to the difference between i and j
    arr[i] = arr[i] - arr[j];
    // Then set j to i + j (the old i value)
    arr[j] = arr[i] + arr[j];
    // Then set i to new j value - diff value.
    arr[i] = arr[j] - arr[i];
    return arr;
}

console.log('aa swapping: ', nums);
console.log('aa swapped', swapInPlace(nums, 0, 1));
console.log('aa swapped again', swapInPlace(nums, 0, 1));