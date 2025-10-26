const { Suit } = require('./suit');
const { Card } = require('./card');

// Deck implementation.
class Deck {

  static CardsInDeck = 52;

  static SuitsInDeck = 4;

  static Suits = [
    new Suit(1, 'Diamond'),
    new Suit(2, 'Heart'),
    new Suit(3, 'Spade'),
    new Suit(4, 'Club')
  ];

  constructor() {
    this.initCards();
  }

  // Initialise a deck.
  initCards() {
    this.cards = [];
    this.cardIndex = 0;
    let currentNum = 0;
    for (let i = 0; i < Deck.CardsInDeck; i++) {
      const fourOffset = i % Deck.SuitsInDeck;
      if (fourOffset === 0) currentNum++;
      const suite = this.getSuite(fourOffset);
      this.cards.push(new Card(suite, currentNum, this.getFaceValue(currentNum)));
    }
  }

  // Reset the cards.
  reset() {
    this.initCards();
  }

  // Deal a card.
  dealCard() {
    if (
      this.cards &&
      this.cards.length &&
      this.cards.length > this.cardIndex
    ) {
      const card = this.cards[this.cardIndex];
      this.cardIndex += 1;
      return card;
    }
    return null;
  }

  // Nice little algorithm dude!
  shuffle() {
    for (let _ = 0; _ < Deck.CardsInDeck; _++) {
      const randCardIndex = ~~(Math.random() * Deck.CardsInDeck);
      const randCardToSwapIndex = ~~(Math.random() * Deck.CardsInDeck);
      // Swap the two.
      const tmp = this.cards[randCardIndex]
      this.cards[randCardIndex] = this.cards[randCardToSwapIndex];
      this.cards[randCardToSwapIndex] = tmp;
    }
    // Do the half n' half.
    const firstHalf = this.cards.slice(0, (this.cards.length / 2));
    const secondHalf = this.cards.slice((this.cards.length / 2), this.cards.length);
    this.cards = [...secondHalf, ...firstHalf];
  }

  // Get the actual value of the card.
  getFaceValue(value) {
    if (value <= 10) return value;
    if (value === 11) return 'Jack';
    if (value === 12) return 'Queen';
    if (value === 13) return 'King';
    return value;
  }

  // Get the suit based off the modulus of dividing by 4.
  getSuite(modulus) {
    return Deck.Suits[modulus]
  }

  toString() {
    let retStr = '';
    this.cards.forEach(c => {
      retStr += (c.toString() + "\n")
    })
    return retStr;
  }
}

module.exports = { Deck };
