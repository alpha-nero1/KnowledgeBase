import 'package:flutter_riverpod/legacy.dart';
import 'package:meals/models/meal.dart';

/// Pretty much a mobx store.
///
class FavouritesNotifier extends StateNotifier<List<Meal>> {
  FavouritesNotifier() : super([]);

  void toggleFavourite(Meal meal) {
    final isExisting = state.contains(meal);
    if (isExisting) {
      state = state.where((m) => m.id != meal.id).toList();
      return;
    }
    state = [...state, meal];
  }

  bool isFavourite(Meal meal) => state.contains(meal);
}

final favouritesProvider = StateNotifierProvider<FavouritesNotifier, List<Meal>>((ref) {
  return FavouritesNotifier();
});