import 'package:flutter/material.dart';
import 'package:src/widets/core/thing.dart';

/// Can easily extend Curve and override the transformInternal() metodo.
class OvershootCustomCurve extends Curve {
  @override
  double transformInternal(double t) {
    // The magic scaling constant for an exact 20% overshoot
    const double s = 1.70158;

    // Decrement t by 1 to shift our cubic calculation base
    final double tMinusOne = t - 1;

    // Formula: (t-1)^2 * ((s+1)*(t-1) + s) + 1
    // This is the optimized algebraic equivalent of the overshoot polynomial
    // Man what the heck is a polynomial?!?
    return tMinusOne * tMinusOne * ((s + 1) * tMinusOne + s) + 1;
  }
}

/// Demonstrates how we can easily create custom curves for our animations
/// if we are not satisfied with the status quo.
///
class Three07CustomOvershootCurve extends StatefulWidget {
  const Three07CustomOvershootCurve({super.key});

  @override
  State<Three07CustomOvershootCurve> createState() =>
      _Three07CustomOvershootCurveState();
}

class _Three07CustomOvershootCurveState
    extends State<Three07CustomOvershootCurve>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: Duration(milliseconds: 400),
    );
    _slideAnimation = Tween<Offset>(begin: Offset(-10, 0), end: Offset(0, 0))
        .animate(
          CurvedAnimation(parent: _controller, curve: OvershootCustomCurve()),
        );
  }

  @override
  void dispose() {
    super.dispose();
    _controller.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              SlideTransition(position: _slideAnimation, child: const Thing()),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: () {
                  _controller.forward(from: 0);
                },
                child: Text('Animate me!'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
