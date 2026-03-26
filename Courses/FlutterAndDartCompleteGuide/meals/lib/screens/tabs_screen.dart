import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:meals/providers/favourites_provider.dart';
import 'package:meals/providers/filters_provider.dart';
import 'package:meals/providers/meals_provider.dart';
import 'package:meals/screens/categories_screen.dart';
import 'package:meals/screens/filters_screen.dart';
import 'package:meals/screens/meals_screen.dart';
import 'package:meals/widgets/tabs_side_drawer.dart';


/// Setup of our tab navigation.
///
/// ConsumerStatefulWidget & ConsumerWidget allows widgets to listen to riverpod provider changes.
class TabsScreen extends ConsumerStatefulWidget {
  const TabsScreen({super.key});

  @override
  ConsumerState<TabsScreen> createState() {
    return _TabsScreenState();
  }
}

class _TabsScreenState extends ConsumerState<TabsScreen> {
  int _selectedIndex = 0;

  void _pageOnSelect(int index) => setState(() {
    _selectedIndex = index;
  });

  Widget _getActiveScreen() {
    // If mealsProvider ever changed it would trigger rebuild.
    final meals = ref.watch(mealsProvider);
    final filtersNotifier = ref.read(filtersProvider.notifier);

    if (_selectedIndex == 0) {
      var availableMeals = meals.where((meal) {
        if (filtersNotifier.isEnabled(Filter.glutenFree) && !meal.isGlutenFree) return false;
        if (filtersNotifier.isEnabled(Filter.lactoseFree) && !meal.isLactoseFree) return false;
        if (filtersNotifier.isEnabled(Filter.vegan) && !meal.isVegan) return false;
        if (filtersNotifier.isEnabled(Filter.vegetarian) && !meal.isVegetarian) return false;
        return true;
      }).toList();

      return CategoriesScreen(
        meals: availableMeals,
      );
    }
    final favouriteMeals = ref.watch(favouritesProvider);
    return MealsScreen(
      meals: favouriteMeals,
    );
  }

  String _getActiveTitle() {
    if (_selectedIndex == 0) {
      return 'Categories';
    }
    return 'Favourites';
  }

  void _screenOnSelect(String id) {
    // Always pop before a nav!
    Navigator.of(context).pop();
    if (id == 'Filters') {
      // pushReplacement replaces, does not add to the stack but swaps out.
      Navigator.of(context).push<Map<Filter, bool>>(
        MaterialPageRoute(
          builder: (ctx) => FiltersScreen(),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(_getActiveTitle())),
      body: _getActiveScreen(),
      bottomNavigationBar: BottomNavigationBar(
        onTap: _pageOnSelect,
        currentIndex: _selectedIndex,
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.set_meal),
            label: 'Categories',
          ),
          BottomNavigationBarItem(icon: Icon(Icons.star), label: 'Favourites'),
        ],
      ),
      drawer: TabsSideDrawer(screenOnSelect: _screenOnSelect),
    );
  }
}
