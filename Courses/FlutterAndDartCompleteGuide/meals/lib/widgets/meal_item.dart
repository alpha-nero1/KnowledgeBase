import 'package:flutter/material.dart';
import 'package:meals/models/meal.dart';
import 'package:meals/widgets/meal_item_trait.dart';
import 'package:transparent_image/transparent_image.dart';

class MealItem extends StatelessWidget {
  final Meal meal;
  final void Function(Meal meal) onTap;

  const MealItem({super.key, required this.meal, required this.onTap});

  String get complexityText {
    return '${meal.complexity.name[0].toUpperCase()}${meal.complexity.name.substring(1)}';
  }

  String get affordabilityText {
    return '${meal.affordability.name[0].toUpperCase()}${meal.affordability.name.substring(1)}';
  }

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: EdgeInsets.all(8),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadiusGeometry.circular(8),
      ),
      clipBehavior: Clip.hardEdge,
      elevation: 2,
      child: InkWell(
        onTap: () { onTap(meal); },
        child: Stack(
          children: [
            // Displays an image that is being faded in, smooth load!
            // Loads from transparent image to real image.
            FadeInImage(
              placeholder: MemoryImage(kTransparentImage),
              image: NetworkImage(meal.imageUrl),
              fit: BoxFit.cover,
              height: 200,
              width: double.infinity,
              imageErrorBuilder: (ctx, a, b) =>
                  Image(image: MemoryImage(kTransparentImage)),
            ),
            // Positioned gets rid of need to use Expanded inside.
            // That is because we end up having a fixed width and it is indeed constrained.
            Positioned(
              bottom: 0,
              right: 0,
              left: 0,
              top: 0,
              child: Container(
                color: Colors.black54,
                padding: EdgeInsets.symmetric(vertical: 8, horizontal: 44),
                child: Column(
                  children: [
                    Text(
                      meal.title,
                      maxLines: 2,
                      textAlign: TextAlign.center,
                      softWrap: true,
                      overflow: TextOverflow.ellipsis,
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.bold,
                        color: Colors.white,
                      ),
                    ),
                    const SizedBox(height: 8),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        MealItemTrait(
                          icon: Icons.work,
                          label: '${meal.duration} min',
                        ),
                        const SizedBox(width: 8),
                        MealItemTrait(icon: Icons.work, label: complexityText),
                        const SizedBox(width: 8),
                        MealItemTrait(
                          icon: Icons.work,
                          label: affordabilityText,
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
