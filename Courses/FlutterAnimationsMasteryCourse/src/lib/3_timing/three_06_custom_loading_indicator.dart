import 'dart:math';

import 'package:flutter/material.dart';

/// Demonstrates the use of a TweenSequence to add a natural pulse to
/// a loading indicator whilst it is spinning indefinitely.
/// 
/// ALSO we used a cheeky method to draw a small curved line without using a painter.
///
class Three06CustomLoadingIndicator extends StatefulWidget {
  const Three06CustomLoadingIndicator({super.key});

  @override
  State<Three06CustomLoadingIndicator> createState() =>
      _Three06CustomLoadingIndicatorState();
}

class _Three06CustomLoadingIndicatorState
    extends State<Three06CustomLoadingIndicator>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _rotationAnimation;
  late Animation<double> _pulseAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: Duration(seconds: 2),
    );
    _rotationAnimation = Tween<double>(
      begin: 0,
      end: pi * 2,
    ).animate(_controller);

    // Tween sequence basically masks or abstracts a single tween as many little ones!
    _pulseAnimation = TweenSequence([
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 1, end: 1.1),
        weight: 70.0,
      ),
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 1.1, end: 1.0),
        weight: 30.0,
      ),
    ]).animate(CurvedAnimation(parent: _controller, curve: const Interval(0.8, 1)));
  }

  @override
  void dispose() {
    super.dispose();
    _controller.dispose();
  }

  Widget buildIndicator() {
    return AnimatedBuilder(
      animation: _rotationAnimation,
      builder: (context, child) => Transform.scale(
        scale: _pulseAnimation.value,
        child: Transform.rotate(
          angle: _rotationAnimation.value,
          child: Container(
            height: 50,
            width: 50,
            decoration: BoxDecoration(
              border: BoxBorder.fromLTRB(
                top: BorderSide(width: 1, color: Colors.white),
              ),
              borderRadius: BorderRadius.all(Radius.circular(25)),
            ),
          ),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              ElevatedButton(
                onPressed: () {
                  _controller.repeat();
                },
                child: Text('Animate me!'),
              ),
              const SizedBox(height: 16),
              buildIndicator(),
            ],
          ),
        ),
      ),
    );
  }
}
