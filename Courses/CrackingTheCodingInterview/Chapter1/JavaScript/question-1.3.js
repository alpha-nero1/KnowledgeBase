const urlifyString = (urlString) => {
  if (urlString && urlString.length) {
    return urlString.split(' ').join('%20')
  }
  return urlString;
}

console.log('urlified https://www.google.com/Hello There/Why You', urlifyString('https://www.google.com/Hello There/Why You'))
console.log('urlified https://www.google.com/Hello hello/Jedi Scum', urlifyString('https://www.google.com/Hello hello/Jedi Scum'))