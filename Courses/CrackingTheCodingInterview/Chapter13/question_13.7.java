/*
    13.7. Lambda Expressions
    There is a class Country that has methods getContinent() and
    getPopulation(). Write a function int getPopulation(List<Country> countries,
    String continent) that computes the total population of a given continent, given a list of all
    countries and the name of a continent.
*/

int getPopulation(List<Country> countries, String continent) {
    Stream<Integer> populations = countries.stream().map(c -> (
        c.getContinent().equals(continent) ? c.getPopulation() : 0
    ));
    return populations.reduce(0, (a, b) -> a + b);
}