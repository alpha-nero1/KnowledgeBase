const SuitTypes = {
  Diamond: 1,
  Heart: 2,
  Spade: 3,
  Club: 4
}

class Suit {
  constructor(type, name) {
    this.type = type;
    this.name = name;
  }
}

module.exports = { Suit, SuitTypes };
