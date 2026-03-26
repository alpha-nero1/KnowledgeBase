import 'package:flutter_riverpod/legacy.dart';
import 'package:meals/providers/meals_provider.dart';
import 'package:riverpod/riverpod.dart';

enum Filter {
  glutenFree,
  lactoseFree,
  vegetarian,
  vegan
}

const gInitialFilters = {
  Filter.glutenFree: false,
  Filter.lactoseFree: false,
  Filter.vegan: false,
  Filter.vegetarian: false
};

class FiltersNotifier extends StateNotifier<Map<Filter, bool>> {
  FiltersNotifier() : super(gInitialFilters);

  void setFilter(Filter filter, bool value) => state = {
    ...state,
    filter: value
  };

  void setFilters(Map<Filter, bool> filtersMap) => state = filtersMap;

  bool isEnabled(Filter filter) => state[filter]!;
}

final filtersProvider = StateNotifierProvider<FiltersNotifier, Map<Filter, bool>>((ref) {
  return FiltersNotifier();
});

final filteredMealsProvider = Provider((ref) {
  final meals = ref.watch(mealsProvider);
  final notifier = ref.watch(filtersProvider.notifier);

  return meals.where((meal) {
    if (notifier.isEnabled(Filter.glutenFree) && !meal.isGlutenFree) return false;
    if (notifier.isEnabled(Filter.lactoseFree) && !meal.isLactoseFree) return false;
    if (notifier.isEnabled(Filter.vegan) && !meal.isVegan) return false;
    if (notifier.isEnabled(Filter.vegetarian) && !meal.isVegetarian) return false;
    return true;
  }).toList();
});