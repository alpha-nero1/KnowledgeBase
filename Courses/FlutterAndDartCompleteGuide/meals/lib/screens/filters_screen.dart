import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:meals/providers/filters_provider.dart';

class FiltersScreen extends ConsumerStatefulWidget {
  const FiltersScreen({ super.key });

  @override
  ConsumerState<FiltersScreen> createState() {
    return _FiltersState();
  }
}

class _FiltersState extends ConsumerState<FiltersScreen> {
  var _isGlutenFree = false;
  var _isLactoseFree = false;
  var _isVegatarian = false;
  var _isVegan = false;

  // So we can access widget.
  @override
  void initState() {
    super.initState();
    final activeFilters = ref.read(filtersProvider);
    _isGlutenFree = activeFilters[Filter.glutenFree] ?? false;
    _isLactoseFree = activeFilters[Filter.lactoseFree] ?? false;
    _isVegatarian = activeFilters[Filter.vegetarian] ?? false;
    _isVegan = activeFilters[Filter.vegan] ?? false;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Filters')),
      body: PopScope(
        canPop: false,
        onPopInvokedWithResult: (didPop, result) {
          if (didPop) {
            return;
          }

          ref.read(filtersProvider.notifier).setFilters({
            Filter.glutenFree: _isGlutenFree,
            Filter.lactoseFree: _isLactoseFree,
            Filter.vegan: _isVegan,
            Filter.vegetarian: _isVegatarian,
          });
        },
        child: Column(children: [
          SwitchListTile(
            title: Text('Gluten-free', style: Theme.of(context).textTheme.titleLarge!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            subtitle: Text('Only include gluten free meals', style: Theme.of(context).textTheme.labelMedium!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            activeThumbColor: Theme.of(context).colorScheme.tertiary,
            contentPadding: const EdgeInsets.only(left: 34, right: 22),
            value: _isGlutenFree, 
            onChanged: (isChecked) { 
              setState(() {
                _isGlutenFree = isChecked;
              });
            }
          ),
          SwitchListTile(
            title: Text('Lactose-free', style: Theme.of(context).textTheme.titleLarge!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            subtitle: Text('Only include lactose free meals', style: Theme.of(context).textTheme.labelMedium!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            activeThumbColor: Theme.of(context).colorScheme.tertiary,
            contentPadding: const EdgeInsets.only(left: 34, right: 22),
            value: _isLactoseFree, 
            onChanged: (isChecked) { 
              setState(() {
                _isLactoseFree = isChecked;
              });
            }
          ),
          SwitchListTile(
            title: Text('Vegetarian', style: Theme.of(context).textTheme.titleLarge!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            subtitle: Text('Only include vegetarian free meals', style: Theme.of(context).textTheme.labelMedium!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            activeThumbColor: Theme.of(context).colorScheme.tertiary,
            contentPadding: const EdgeInsets.only(left: 34, right: 22),
            value: _isVegatarian, 
            onChanged: (isChecked) { 
              setState(() {
                _isVegatarian = isChecked;
              });
            }
          ),
          SwitchListTile(
            title: Text('Vegan', style: Theme.of(context).textTheme.titleLarge!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            subtitle: Text('Only include vegan free meals', style: Theme.of(context).textTheme.labelMedium!.copyWith(
              color: Theme.of(context).colorScheme.onSurface
            )),
            activeThumbColor: Theme.of(context).colorScheme.tertiary,
            contentPadding: const EdgeInsets.only(left: 34, right: 22),
            value: _isVegan, 
            onChanged: (isChecked) { 
              setState(() {
                _isVegan = isChecked;
              });
            }
          )
        ],),
      ),
    );
  }
}
