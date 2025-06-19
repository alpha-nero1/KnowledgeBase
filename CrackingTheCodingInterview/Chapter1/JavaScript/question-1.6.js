// String Compression.

const compressString = value => {
  let retArr = [];
  if (value && value.length) {
    let lastCharIndex = 0;
    for (let i = 0; i < value.length; i++) {
      const char = value[i];
      if (i > 0 && char === value[lastCharIndex] && char != ' ') {
        if (+retArr[retArr.length - 1]) {
          retArr[retArr.length - 1] = retArr[retArr.length - 1] + 1;
        } else {
          retArr.push(2);
        }
      } else {
        // Just add the char.
        retArr.push(char);
        lastCharIndex = i;
      }
    }
  }
  return retArr.join('');
}

console.log('compressString aaaabccdefffgg: ', compressString('aaaabccdefffgg'));
console.log('compressString hello there mr waggsworldd: ', compressString('hello there mr waggsworldd'));
console.log('compressString hello   there mr  waggsworldd: ', compressString('hello   there mr  waggsworldd'));