/**
The Apocalypse: In the new post-apocalyptic world, the world queen is desperately 
concerned about the birth rate. Therefore, she decrees that all families should 
ensure that they have one girl or else they face massive fines. 
If all families abide by this policy-that is, they have continue to have children 
until they have one girl, at which point they immediately stop-what will the gender 
ratio of the new generation be? (Assume that the odds of someone having a boy or a 
girl on any given pregnancy is equal.) Solve this out logically and then write 
a computer simulation of it.
 */

const runFamily = () => {
  let boys = 0;
  let girls = 0;
  while (girls === 0) {
    const r = Math.random();
    if (r > 0.5) {
      boys += 1;
    } else {
      girls += 1;
    }
  }
  return [boys, girls];
}

const runFamilies = (n) => {
  let boys = 0;
  let girls = 0;
  for (let i = 0; i < n; i++) {
    const genders = runFamily();
    boys += genders[0];
    girls += genders[1];
  }
  return (girls / (boys + girls));
}

console.log('runFamilies(1) = ', runFamilies(1));
console.log('runFamilies(5) = ', runFamilies(5));
console.log('runFamilies(10) = ', runFamilies(10));
console.log('runFamilies(20) = ', runFamilies(20));
console.log('runFamilies(30) = ', runFamilies(30));
console.log('runFamilies(100) = ', runFamilies(100));
console.log('runFamilies(1000) = ', runFamilies(1000));
console.log('runFamilies(10000) = ', runFamilies(10000));

// Odds are generally around 0.5 (especially on large numbers).
