/*
    16.4. Tic Tac Win
    Design an algorithm to figure out if someone has won a game of tic-tac-toe.
*/

class Piece {
    constructor(value) {
        _value = value;
    }

    getValue = () => _value;
}

const hasWonRow = (board, row) => {
    for (let c = 1; c < board[row].length; c++) {
        if (board[row][c] !== board[row][0]) return false;
    }
    return true;
}

const hasWonColumn = (board, col) => {
    for (let r = 1; r < board.length; r++) {
        if (board[r][col] !== board[0][col]) return false;
    }
    return true;
}

const hasWonDiagonal = (board, direction) => {
    const row = 0;
    const col = (
        direction == 1 ?
        0 :
        board.length - 1
    );
    const first = board[0][col];
    for (let i = 0; i < board.length; i++) {
        if (board[row][col] !== first) return false;
        row += 1;
        // Love it!
        column += direction;
    }
    return true;
}

const hasWon = (board, row, column) => {
    if (board.length != board[0].length) return null;

    const piece = board[row][column];

    if (piece === null) return null;

    if (hasWonRow(board, row) || hasWonColumn(board, column)) return piece;
    if (row === column && hasWonDiagonal(board, 1)) return piece;
    if (row === (board.length - column - 1) && hasWonDiagonal(board, -1)) return piece;

    return null;
}