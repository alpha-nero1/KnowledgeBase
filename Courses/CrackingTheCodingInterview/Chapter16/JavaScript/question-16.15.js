/*
    16.15 Master Mind.
*/

const randRgby = () => {
    const rgby = ['R', 'G', 'B', 'Y'];
    // Remember: ~ = bitewise NOT operator.
    const randIndex = ~~(Math.random() * rgby.length)
    return rgby[randIndex];
}

const mastermind = ((numSlots = 4) => {
    // Create the slots.
    const slots = []
    const slotMap = new Map();
    for (let i = 0; i < numSlots; i++) {
        slots.push(randRgby());
        const arr = slotMap.get(slots[i]) ? slotMap.get(slots[i]) : []
        arr.push(i);
        slotMap.set(slots[i], arr);
    }

    const guess = (guessSlots) => {
        console.log('Guessing ', guessSlots)
        const guesses = [];
        for (let j = 0; j < guessSlots.length; j++) guesses.push([guessSlots[j], j]);
        guesses.forEach(([key, value]) => {
            if (slotMap.has(key)) {
                if (slotMap.get(key).includes(value)) {
                    console.log('You got one!');
                    return;
                }
                console.log('Almost right!')
            }
        });
    }

    const reveal = () => {
        console.log(slots);
    }

    // Expose functions that allow us to interact with the game we set up.
    return {
        guess,
        reveal
    }
});

const mm = mastermind(4);
mm.reveal();
mm.guess('RGGY');
