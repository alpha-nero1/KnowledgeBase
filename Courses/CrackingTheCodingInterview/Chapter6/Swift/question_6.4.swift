import Darwin 

// Find the probability of ants running into eachother
func antsOnPolygon(vertices: Int) -> Double {
  // Chance of 1 ant going clockwise is 0.5, therefor chance of all is * 3.
  let chanceOfClockwise = pow(0.5, Double(vertices)); // = 1/2 * 3
  let chanceOfAnticlockwise = chanceOfClockwise; // = 1/2 * 3
  let chanceOfSameDirection = chanceOfClockwise + chanceOfAnticlockwise; // = 1/4
  return 1 - chanceOfSameDirection; // Negate chance of same direction.
}

let res = antsOnPolygon(vertices: 3);
print(res)