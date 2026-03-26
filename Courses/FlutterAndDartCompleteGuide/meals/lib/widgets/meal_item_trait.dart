import 'package:flutter/material.dart';

class MealItemTrait extends StatelessWidget {
  final IconData icon;
  final String label;

  const MealItemTrait({super.key, required this.icon, required this.label});

  @override
  Widget build(BuildContext context) {
    return Row(children: [
      Icon(icon, size: 16, color: Colors.white,),
      const SizedBox(width: 8),
      Text(label, style: TextStyle(color: Colors.white))
    ],);
  }

}