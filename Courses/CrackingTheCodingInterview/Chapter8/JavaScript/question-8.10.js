/*
    8.10 Paint Fill: Implement the"paint fill"function that one might see on many image editing programs.
    That is, given a screen (represented by a two-dimensional array of colors), a point, and a new color,
    fill in the surrounding area until the color changes from the original color.
*/

const Colours = {
    Red: 'R',
    Green: 'G',
    Blue: 'B',
}

// This is effectively a depth first search on a grid!
function paint(screen, row, col, oldColour, newColour) {
    if (row < 0 || row >= screen.length) return false;
    if (col < 0 || col >= screen[0].length) return false;


    if (screen[row][col] !== oldColour) return true;
    screen[row][col] = newColour;
    paint(screen, row + 1, col, oldColour, newColour);
    paint(screen, row - 1, col, oldColour, newColour);
    paint(screen, row, col + 1, oldColour, newColour);
    paint(screen, row, col - 1, oldColour, newColour);
}

function paintFill(screen, row, col, newColour) {
    if (screen[row][col] == newColour) return false;
    paint(screen, row, col, screen[row][col], newColour);
    return screen;
}


const inputScreen = [
    [Colours.Red, Colours.Red, Colours.Red, Colours.Red],
    [Colours.Green, Colours.Red, Colours.Red, Colours.Red],
    [Colours.Green, Colours.Green, Colours.Red, Colours.Red],
    [Colours.Green, Colours.Green, Colours.Green, Colours.Red],
];


console.log('painted screen = ', paintFill(inputScreen, 3, 0, Colours.Blue));