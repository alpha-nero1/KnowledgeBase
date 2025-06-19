# Swift

Use the `swift` command to compile and run swift files.

Notes on swift.
- Swift strings are weird and cannot natively be indexed to get a char.
- uses `.count` instead of JS `.length`
- Cannot do a boolean check on a number, must include comparitor e.g `myNum != 0`.
- Can make use of `for in` loops.
- String concat is done with "\(variable)"
- We can specify multiple constructir sigs, general is: `init() {}` but we could also do `init(withPairs pairs: [Any]) {}` for instance.
- The `Character` class has a property called unicodeScalars that return an array of unicode numerical values. to get the unicode val return the first in that array.
- arrays have .sort and .filter functions availabel to them, they accept the { $0, $1... syntax } e.g.
```
myArr.sort {
  return $0 > $1
}
```
- Cannot use the typical for(i = ...) loop syntax, must use for in and if you want to enumerate the array you have to do `for (index, item) in arr.enumerated()`