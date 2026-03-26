import 'package:flutter/material.dart';
import 'package:meals/models/category.dart';

class CategoryGridItem extends StatelessWidget {
  final Category category;
  final void Function() onSelect;

  const CategoryGridItem({
    super.key, 
    required this.category,
    required this.onSelect
  });


  @override
  Widget build(BuildContext context) {
    // InkWell is a gesture detector with special effects.
    return InkWell(
      onTap: onSelect,
      splashColor: Theme.of(context).primaryColor,
      borderRadius: BorderRadius.circular(20),
      // Container is kinda like div or view tbh I think.
      child: Container(
        padding: const EdgeInsets.all(16),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(20),
          gradient: LinearGradient(
            begin: Alignment.topLeft,
            end: Alignment.bottomRight,
            colors: [
            category.color.withAlpha(255),
            category.color.withAlpha(200)
          ]),
        ),
        child: Text(category.title, style: Theme.of(context).textTheme.titleLarge!.copyWith(
          color: Theme.of(context).colorScheme.onSurface
        ))
      ),
    );
  }

}