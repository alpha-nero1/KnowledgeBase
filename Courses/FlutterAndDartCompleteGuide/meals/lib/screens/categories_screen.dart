import 'package:flutter/material.dart';
import 'package:meals/constants/dummy_data.dart';
import 'package:meals/models/category.dart';
import 'package:meals/models/meal.dart';
import 'package:meals/screens/meals_screen.dart';
import 'package:meals/widgets/category_grid_item.dart';

/// Shows the categories of meals.
///
class CategoriesScreen extends StatefulWidget {
  final List<Meal> meals;

  const CategoriesScreen({super.key, required this.meals});

  // Context is not globally available inside stateless widget.
  void categoryOnSelect(BuildContext context, Category category) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (ctx) => MealsScreen(
          title: category.title,
          meals: meals
              .where((m) => m.categories.contains(category.id))
              .toList(),
        ),
      ),
    );
  }

  @override
  State<StatefulWidget> createState() {
    return _State();
  }
}

/// SingleTickerProviderStateMixin provides the frame rate info to the animation controller
/// so that we animate at max FPS.
///
class _State extends State<CategoriesScreen>
    with SingleTickerProviderStateMixin {
  // late means that init will occur in initState();
  late AnimationController _animationController;

  @override
  void initState() {
    super.initState();
    // AnimationController does not automatically call build() we need more steps.
    _animationController = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 300),
      // The values for flutter to animate between
      // These values are the default.
      lowerBound: 0,
      upperBound: 1,
    );

    // Play the animation!
    _animationController.forward();
  }

  @override
  Widget build(BuildContext context) {
    // Scaffold = base screen setup.
    return AnimatedBuilder(
      // Controller here.
      animation: _animationController,
      // Non changing child here.
      child: GridView(
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
              onSelect: () => widget.categoryOnSelect(context, category),
            ),
        ],
      ),
      builder: (ctx, child) => SlideTransition(
        // Still EXPLICIT
        position: Tween(begin: const Offset(0, 0.2), end: const Offset(0, 0))
            .animate(
              CurvedAnimation(
                parent: _animationController,
                curve: Curves.easeInOut,
              ),
            ),
        child: child,
      ),
    );
  }

  @override
  void dispose() {
    // Clean up the animation controller!
    _animationController.dispose();
    super.dispose();
  }
}



/*
Explicit animation:

1)
Widget build(BuildContext context) {
    // Scaffold = base screen setup.
    return AnimatedBuilder(
      // Controller here.
      animation: _animationController,
      // Non changing child here.
      child: GridView(
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
              onSelect: () => widget.categoryOnSelect(context, category),
            ),
        ],
      ),
      // EXPLICIT ANIMATION HERE!
      builder: (ctx, child) => Padding(
        padding: EdgeInsetsGeometry.only(top: 100 - (_animationController.value * 100)),
        child: child
      ),
    );
  }

2)

*/