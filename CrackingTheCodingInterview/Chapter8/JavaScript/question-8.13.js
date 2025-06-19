/*
    8.13 Stack of Boxes: You have a stack of n boxes, with widths w1, heights hi, and depths di. The boxes 
    cannot be rotated and can only be stacked on top of one another if each box in the stack is strictly 
    larger than the box above it in width, height, and depth. Implement a method to compute the 
    height of the tallest possible stack. The height of a stack is the sum of the heights of each box. 
*/

class Box {
    constructor(w, h, d) {
        this.w = w;
        this.h = h;
        this.d = d;
    }

    isLargerThanBox = (box) => {
        return (
            this.w > box.w &&
            this.h > box.h &&
            this.d > box.d
        );
    };
}

const boxes = [
    new Box(10, 12, 8),
    new Box(10, 14, 8),
    new Box(10, 14, 7),
    new Box(10, 14, 9),
    new Box(6, 10, 14),
    new Box(5, 8, 14),
]

const getTallestPossibleStack = (boxes, stack, index = 0) => {
    if (index < boxes.lenght - 1) return;
    const box = boxes[index];
    const nextBox = boxes[index + 1];
    if (box.isLargerThanBox()) {
        stack.push(box);
    } else {
    }
}

const stack = (() => {
    const sortedBoxes = boxes.sort((a, b) => b.h - a.h);
    const stack = [];
    getTallestPossibleStack(sortedBoxes, stack);
    return stack;
})();