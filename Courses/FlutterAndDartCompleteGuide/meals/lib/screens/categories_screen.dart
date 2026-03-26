import 'package:flutter/material.dart';
import 'package:meals/constants/dummy_data.dart';
import 'package:meals/models/category.dart';
import 'package:meals/models/meal.dart';
import 'package:meals/screens/meals_screen.dart';
import 'package:meals/widgets/category_grid_item.dart';

/// Shows the categories of meals.
///
class CategoriesScreen extends StatelessWidget {
  final List<Meal> meals;
  
  const CategoriesScreen({super.key, required this.meals});

  // Context is not globally available inside stateless widget.
  void _categoryOnSelect(BuildContext context, Category category) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (ctx) => MealsScreen(
          title: category.title,
          meals: meals.where((m) => m.categories.contains(category.id)).toList(),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    // Scaffold = base screen setup.
    return GridView(
      padding: EdgeInsets.all(24),
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2,
        childAspectRatio: 3 / 2,
        crossAxisSpacing: 20,
        mainAxisSpacing: 20,
      ),
      children: [
        for (final category in availableCategories)
          CategoryGridItem(
            category: category,
            onSelect: () => _categoryOnSelect(context, category),
          ),
      ],
    );
  }
}
