/*
    17.21. Volume of histogram.
*/

// Go through each bar and calculate the volume of water above it
// volume of water at bar = height - min (tallest on left and right)
// where above equation is positive
const computeHistogramVolume = (hist) => {
    const leftMaxes = [];
    let leftMax = hist[0];

    for (let i = 0; i < hist.length; i++) {
        const col = hist[i];
        leftMax = Math.max(col, leftMax);
        leftMaxes.push(leftMax);
    };

    let sum = 0;

    let rightMax = hist[hist.length - 1];
    for (let j = hist.length - 1; j > -1; j--) {
        const col = hist[j];
        rightMax = Math.max(col, rightMax);

        // Get the min of the left and right side.
        const outerMin = Math.min(rightMax, leftMaxes[j]);

        // if outerMin is greater than this current col
        // then add to the sum!
        if (outerMin > col) {
            sum += outerMin - hist[j];
        }
    };

    return sum;
}


console.log(computeHistogramVolume([0, 0, 4, 0, 0, 6, 0, 0, 3, 0, 8, 0, 2, 0, 5, 2, 0, 3, 0, 0]));