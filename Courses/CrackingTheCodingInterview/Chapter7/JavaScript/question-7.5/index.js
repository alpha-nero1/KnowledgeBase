// Great sleep implementation...
const sleep = (ms) => {
    return new Promise(resolve => setTimeout(resolve, ms));
}
  
class Page {
    constructor(lines) {
        this.lines = lines || [];
    }
}

class Book {
    constructor(name, pages) {
        this.name = name;
        this.pages = pages || [];
    }
}

class OnlineBookReader {
    constructor(books) {
        this.loadBooks(books);
        this.readingTimeout = null;
    }

    // Public
    // Note here that all loops need to be standard for loops so that
    // we stay in the main thread.
    async read(bookName) {
        const book = this.bookStore.get(bookName);
        if (!book) throw new Error(`We don't have ${bookName}`);
        // Start reading the lines of each page.
        console.log(`\nReading ${bookName}`)
        // Loop pages of book.
        for (let i = 0; i < book.pages.length; i++) {
            const page = book.pages[i];
            console.log(`\nPage ${i} \n`)
            // Loop lines of page.
            for (let j = 0; j < page.lines.length; j++) {
                await sleep(1000);
                const line = page.lines[j];
                console.log(line);
            }
        }
        console.log('\n');
    }

    // Public
    stopReading() {
        if (this.readingTimeout) {
            clearInterval(this.readingTimeout);
            this.readingTimeout = null;
        }
    }

    // Private
    loadBooks(books = []) {
        this.bookStore = new Map();
        books.forEach(book => this.bookStore.set(book.name, book));
    }
}


const bookStore = new OnlineBookReader([
    new Book('Cocktail Bible', [
        new Page([
            '1. Margarita',
            'Ingredients:',
            '- Lime',
            '- Tequilla',
            '- Contreau',
        ]),
        new Page([
            '2. Old Fashioned',
            'Ingredients:',
            '- Whiskey',
            '- Orange peel',
            '- Angostura bitters',
        ])
    ]),
    new Book('Cracking the Coding Interview', [
        new Page([
            '1. Reverse a Linked List',
            'Cheers',
            '- Go for it',
        ])
    ])
]);


bookStore.read('Cocktail Bible')
.then(res => {
    bookStore.read('Cracking the Coding Interview')
});
