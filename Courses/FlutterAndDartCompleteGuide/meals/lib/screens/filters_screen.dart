import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:meals/providers/filters_provider.dart';

/// ConsumerWidget can work with normal components and do not need to be stateful
/// because they are managing NO state.
///
class FiltersScreen extends ConsumerWidget {
  const FiltersScreen({ super.key });

  void setFilter(WidgetRef ref, Filter filter, bool value) {
    ref.read(filtersProvider.notifier).setFilter(filter, value);
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final filters = ref.watch(filtersProvider);

    return Scaffold(
      appBar: AppBar(title: Text('Filters')),
      body: Column(children: [
        SwitchListTile(
          title: Text('Gluten-free', style: Theme.of(context).textTheme.titleLarge!.copyWith(
            color: Theme.of(context).colorScheme.onSurface
          )),
          subtitle: Text('Only include gluten free meals', style: Theme.of(context).textTheme.labelMedium!.copyWith(
            color: Theme.of(context).colorScheme.onSurface
          )),
          activeThumbColor: Theme.of(context).colorScheme.tertiary,
          contentPadding: const EdgeInsets.only(left: 34, right: 22),
          value: filters[Filter.glutenFree]!, 
          onChanged: (isChecked) => setFilter(ref, Filter.glutenFree, isChecked)
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
          value: filters[Filter.lactoseFree]!, 
          onChanged: (isChecked) => setFilter(ref, Filter.lactoseFree, isChecked)
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
          value: filters[Filter.vegetarian]!, 
          onChanged: (isChecked) => setFilter(ref, Filter.vegetarian, isChecked)
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
          value: filters[Filter.vegan]!, 
          onChanged: (isChecked) => setFilter(ref, Filter.vegan, isChecked)
        )
      ],),
    );
  }
}


// Remember good ol popscope"
/*

PopScope(
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
        child: 

*/
