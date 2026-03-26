import 'package:meals/constants/dummy_data.dart';
import 'package:riverpod/riverpod.dart';

final mealsProvider = Provider((ref) {
  return dummyMeals;
});