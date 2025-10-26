const { Deck } = require('./deck');

const d = new Deck();
d.shuffle();

console.log(d.toString());