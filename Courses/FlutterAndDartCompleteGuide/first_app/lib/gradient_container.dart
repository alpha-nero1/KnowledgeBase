import 'package:flutter/material.dart';

// final = const - final is a runtime constant
// const = const - gives extra info to dart - value is a compile time constant
const beginAlign = Alignment.topLeft;
const endAlign = Alignment.bottomRight;

class GradientContainer extends StatelessWidget {
  final Color left;
  final Color right;
  // super.key forwards the key to super
  // `const` here tells dart that this class can be optimised in memory!
  // Can also use { required this.left } to add required named arg.
  const GradientContainer(this.left, this.right, { super.key });

  // Alternative constructor function.
  const GradientContainer.purple({ super.key }):
    left = Colors.deepPurple,
    right = Colors.lightBlue;

  // Overriding a method defined in StatelessWidget
  @override
  Widget build(context) {
    // Container is of type Widget so can just return that directly.
    return Container(
      decoration: BoxDecoration(
        // Linear gradient goes from left to right by default.
        gradient: LinearGradient(
          colors: [left, right],
          begin: beginAlign,
          end: endAlign,
        ),
      ),
      child: Center(
        child: Image.asset('assets/images/dice-1.png', width: 200)
      ),
    );
  }
}