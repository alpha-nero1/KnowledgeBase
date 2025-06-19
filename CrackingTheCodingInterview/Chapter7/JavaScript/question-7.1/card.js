// Card implementation.
class Card {
  constructor(suit, value, faceValue) {
    this.suit = suit;
    this.value = value;
    this.faceValue = faceValue;
  }

  toString() {
    return `[Card value: ${this.faceValue}, suit: ${this.suit.name}]`;
  }
}

module.exports = { Card };
