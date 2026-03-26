import 'package:flutter_riverpod/legacy.dart';

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