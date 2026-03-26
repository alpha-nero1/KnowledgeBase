import 'package:flutter/material.dart';
import 'package:meals/models/meal.dart';
import 'package:meals/screens/meal_details_screen.dart';
import 'package:meals/widgets/meal_item.dart';

/// Show the meals under a category.
///
class MealsScreen extends StatelessWidget {
  final String? title;
  final List<Meal> meals;

  const MealsScreen({
    super.key,
    this.title,
    required this.meals,
  });

  void mealOnTap(BuildContext context, Meal meal) {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (ctx) =>
            MealDetailsScreen(meal: meal),
      ),
    );
  }

  Widget getList(BuildContext context) {
    if (meals.isEmpty) {
      return Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(
              'No meals found',
              style: Theme.of(context).textTheme.headlineLarge!.copyWith(
                color: Theme.of(context).colorScheme.onSurface,
              ),
            ),
            const SizedBox(height: 16),
            Text(
              'Try selecting a different category',
              style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                color: Theme.of(context).colorScheme.onSurface,
              ),
            ),
          ],
        ),
      );
    }

    return ListView.builder(
      itemCount: meals.length,
      itemBuilder: (ctx, i) =>
          MealItem(meal: meals[i], onTap: (meal) => mealOnTap(context, meal)),
    );
  }

  @override
  Widget build(BuildContext context) {
    final body = getList(context);
    if (title == null) {
      return body;
    }
    return Scaffold(
      appBar: AppBar(title: Text(title!)),
      body: body,
    );
  }
}
