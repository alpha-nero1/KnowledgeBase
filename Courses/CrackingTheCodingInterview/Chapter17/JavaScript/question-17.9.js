/**
 * 17.9. Kth Multiple
 */
var getKthMagicNumber = function (k) {
    console.log("##### START ######");
    if (k < 0)
        return 0;
    var val = 0;
    // Set up queues.
    var q3 = [1];
    var q5 = [];
    var q7 = [];
    for (var i = 0; i <= k; i++) {
        var v3 = q3.length > 0 ? q3[q3.length - 1] : Number.MAX_VALUE;
        var v5 = q5.length > 0 ? q5[q5.length - 1] : Number.MAX_VALUE;
        var v7 = q7.length > 0 ? q7[q7.length - 1] : Number.MAX_VALUE;
        console.log("##### IT START ######");
        console.log("val ".concat(val));
        console.log("v3 ".concat(v3, ", v5 ").concat(v5, ", v7 ").concat(v7));
        val = Math.min(v3, v5, v7);
        // Enqueue into 3, 5, 7
        if (val === v3) {
            q3.pop();
            q3.push(val * 3);
            q5.push(val * 5);
        }
        // Enqueue into 5 and 7
        else if (val === v5) {
            q5.pop();
            q5.push(5 * val);
        }
        // Enqueue into 7
        else if (val === v7) {
            q7.pop();
        }
        // Always enqueue into q7
        q7.push(val * 7);
        console.log("queue 3 = ".concat(q3));
        console.log("queue 5 = ".concat(q5));
        console.log("queue 7 = ".concat(q7));
        console.log("\n");
    }
    return val;
};
getKthMagicNumber(+process.argv[2]);
