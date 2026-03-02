import 'package:flutter/material.dart';

// Main is picked up as the app entrypoint.
void main() {
  runApp(
    MaterialApp(
      home: Scaffold(
        // backgroundColor: Colors.deepPurple,
        body: Container(
          decoration: BoxDecoration(gradient: LinearGradient()),
          child: const Center(child: Text('Hello World!'))
        )),
    ),
  );
}
