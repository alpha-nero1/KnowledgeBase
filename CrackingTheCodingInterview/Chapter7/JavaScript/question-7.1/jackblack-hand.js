const { Hand } = require('./hand');

// Jackblack hand
class JackblackHand extends Hand {

  static MaxHandValue = 21;

  addCard(card) {
    if (card) {
      const { value } = card;
      const maxPossibleValue = value === 1 ? 13 : value;
      this.score += maxPossibleValue;
    }
  }

  isBust() {
    return this.score > JackblackHand.MaxHandValue;
  }

  hasWon() {
    return this.score === JackblackHand.MaxHandValue;
  }
}

module.exports = { JackblackHand };