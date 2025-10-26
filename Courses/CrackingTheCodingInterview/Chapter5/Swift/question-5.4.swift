// Find the next smallest and largest integer that have the same number of bits.
// To help: https://www.geeksforgeeks.org/next-higher-number-with-same-number-of-set-bits/

func getNextHighest(num: Int) -> Int {
  var val = num;
  // Compute C0 and C1
  var c = val;
  var c0 = 0;
  var c1 = 0;

  while ((c & 1 == 0) && (c != 0)) {
    // While we have not yet ran into a 1 and c still has value.
    c0 += 1;
    c >>= 1;
  }

  while (c & 1 == 1) {
    // Now we are crawling through the ones seq.
    c1 += 1;
    c >>= 1;
  }

  // Error check the value.
  if (c0 + c1 == 31 || c0 + c1 == 0) {
    return -1; // There could not possibly be a higer value.
  }

  // RE: p would be those 0 and 1 lenghts combined
  let p = c0 + c1;
  val |= (1 << p); // turn the p bit into a 1.
  // Clear bits to right of P.
  let a = 1 << p; // p bit
  let b = a - 1; // seq of 1s after p.
  let mask = ~b; // seq of 0s after p.
  val &= mask;
  // Insert c1 - 1 to the right.
  let d = 1 << (c1 - 1) // offset bit
  let e = d - 1;
  val |= e;
  return val;
}

func getPrevHighest(num: Int) -> Int {
  var val = num;
  var c = val;
  var c0 = 0;
  var c1 = 0;

  while ((c & 1) == 1) { // get num of 1s FIRST
    c1 += 1;
    c >>= 1;
  }

  while ((c & 1) == 0 && c != 0) { // get num of 0s SECOND
    c0 += 1;
    c >>= 1;
  }

  let p = c1 + c0;
  // Flip p (e.g p= 5, c1 = 2, c0 = 5)
  val &= (~0 << (p + 1)); // seq of 1s shifted = 11000000
  let mask = 1 << (c1 + 1) - 1; // seq of c1 + 1 1s. = 00000011
  val |= mask << (c0 - 1) // Shift over to where the 0s ended minues one, giving a lower shifted value.
  // = 00110000 >
  return val;
}

// Returns [smallest, largest]
func nextNumber(val: Int) -> [Int] {
  return [getPrevHighest(num: val), getNextHighest(num: val)];
}

let next = nextNumber(val: 123456);
print("nextNumber (123456) = \(next)");