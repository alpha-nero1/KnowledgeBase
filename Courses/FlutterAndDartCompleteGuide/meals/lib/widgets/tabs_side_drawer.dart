import 'package:flutter/material.dart';

class TabsSideDrawer extends StatelessWidget {
  final void Function(String id) screenOnSelect;
  const TabsSideDrawer({super.key, required this.screenOnSelect});

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Column(
        children: [
          DrawerHeader(
            padding: EdgeInsets.all(16),
            decoration: BoxDecoration(
              gradient: LinearGradient(
                begin: AlignmentGeometry.topLeft,
                end: AlignmentGeometry.bottomRight,
                colors: [
                  Theme.of(context).colorScheme.primaryContainer,
                  Theme.of(context).colorScheme.primaryContainer.withAlpha(150),
                ],
              ),
            ),
            child: Row(
              children: [
                Icon(
                  Icons.fastfood,
                  size: 48,
                  color: Theme.of(context).colorScheme.primary,
                ),
                const SizedBox(width: 16),
                Text(
                  'Cooking up',
                  style: Theme.of(context).textTheme.titleLarge!.copyWith(
                    color: Theme.of(context).colorScheme.primary,
                  ),
                ),
              ],
            ),
          ),
          ListTile(
            title: Text(
              'Meals',
              style: Theme.of(context).textTheme.titleSmall!.copyWith(
                color: Theme.of(context).colorScheme.onSurface,
                fontSize: 24,
              ),
            ),
            leading: Icon(
              Icons.restaurant,
              size: 24,
              color: Theme.of(context).colorScheme.onSurface,
            ),
            onTap: () => screenOnSelect('Meals'),
          ),
          ListTile(
            title: Text(
              'Filters',
              style: Theme.of(context).textTheme.titleSmall!.copyWith(
                color: Theme.of(context).colorScheme.onSurface,
                fontSize: 24,
              ),
            ),
            leading: Icon(
              Icons.filter_alt,
              size: 24,
              color: Theme.of(context).colorScheme.onSurface,
            ),
            onTap: () => screenOnSelect('Filters'),
          ),
        ],
      ),
    );
  }
}
