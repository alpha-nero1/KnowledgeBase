// Hand.
class Hand {
  constructor() {
    this.score = 0;
  }

  addCard(card) {
    this.score += card.value;
  }

  reset() {
    this.score = 0;
  }
}

module.exports = { Hand };