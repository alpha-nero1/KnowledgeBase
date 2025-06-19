/**
 * 17.9. Kth Multiple
 */

declare const process;

const getKthMagicNumber = (k: number): number => {
    console.log("##### START ######");
    if (k < 0) return 0;
    let val = 0;

    // Set up queues.
    const q3: number[] = [1];
    const q5: number[] = [];
    const q7: number[] = [];

    for (let i = 0; i <= k; i++) {
        let v3 = q3.length > 0 ? q3[q3.length - 1] : Number.MAX_VALUE;
        let v5 = q5.length > 0 ? q5[q5.length - 1] : Number.MAX_VALUE;
        let v7 = q7.length > 0 ? q7[q7.length - 1] : Number.MAX_VALUE;

        console.log("##### IT START ######");
        console.log(`val ${val}`);
        console.log(`v3 ${v3}, v5 ${v5}, v7 ${v7}`);

        val = Math.min(v3, v5, v7);
        // Take from 3, Enqueue into 3, 5, 7
        if (val === v3) {
            q3.pop();
            q3.push(val * 3);
            q5.push(val * 5);
        }
        // Take from 5, Enqueue into 5 and 7
        else if (val === v5) {
            q5.pop();
            q5.push(5 * val);
        }
        // Take from 7, Enqueue into 7
        else if (val === v7) {
            q7.pop()
        }

        // Always enqueue into q7
        q7.push(val * 7);

        console.log(`queue 3 = ${q3}`);
        console.log(`queue 5 = ${q5}`);
        console.log(`queue 7 = ${q7}`);
        console.log(`\n`);
    }

    return val;
}

getKthMagicNumber(+process.argv[2]);
