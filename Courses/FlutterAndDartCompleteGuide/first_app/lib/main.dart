import 'package:flutter/material.dart';
import 'package:first_app/gradient_container.dart';

// Main is picked up as the app entrypoint.
void main() {
  runApp(
    const MaterialApp(
      home: Scaffold(
        // backgroundColor: Colors.deepPurple,
        body: GradientContainer(Colors.deepPurple, Colors.lightBlue)
      ),
    ),
  );
}

