/*
    13.8 Lamda Random
    Using Lambda expressions, write a function List <Integer>
    getRandomSub set ( List< Integer> list) that returns a random subset of arbitrary size. All
    subsets (including the empty set) should be equally likely to be chosen.
*/

List<Integer> getRandomSubset(List<Integer> list) {
    Random random = new Random();

    // Here we go with the flip coin solution because
    // we could get ANY number of elements in the list!
    List<Integer> subset = list.stream().filter(k -> (
        random.nextBoolean()
    )).collect(Collectors.toList());
    return subset;
}