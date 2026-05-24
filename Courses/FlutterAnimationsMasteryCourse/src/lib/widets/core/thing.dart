import 'package:flutter/material.dart';

class Thing extends StatelessWidget {
  const Thing({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 50, 
      width: 50, 
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(8),
        color: Colors.blue
      )
    );
  }

}