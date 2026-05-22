import 'package:flutter/material.dart';

class Two04BouncyBall extends StatefulWidget {
  const Two04BouncyBall({super.key});

  @override
  State<Two04BouncyBall> createState() => _Two04BouncyBallState();
}

class _Two04BouncyBallState extends State<Two04BouncyBall>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _dropAnimation;
  late Animation<double> _squishAnimation;

  @override
  void initState() {
    super.initState();
    _controller =
        AnimationController(vsync: this, duration: Duration(milliseconds: 1000))
          ..addListener(() {
            setState(() {});
          });
    // Simple drop using alignment.
    _dropAnimation = TweenSequence<double>([
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: -1, end: 0.8)
          .chain(CurveTween(curve: Curves.easeInQuad)), 
        weight: 50.0
      ),
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 0.8, end: -1)
          .chain(CurveTween(curve: Curves.easeOutQuad)), 
        weight: 50.0
      ),
    ]).animate(_controller);

    _squishAnimation = TweenSequence<double>([
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 1.0, end: 1.0), // Normal shape
        weight: 45.0,
      ),
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 1.0, end: 0.6) // Compressed fat circle
            .chain(CurveTween(curve: Curves.easeOut)),
        weight: 5.0,
      ),
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 0.6, end: 1.0) // Snapping back to round
            .chain(CurveTween(curve: Curves.easeIn)),
        weight: 5.0,
      ),
      TweenSequenceItem<double>(
        tween: Tween<double>(begin: 1.0, end: 1.0), // Normal shape remainder
        weight: 45.0,
      ),
    ]).animate(_controller);
  }

  void scaffoldAnimation() {

  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Stack(
          children: [
          AnimatedBuilder(
            animation: _controller,
            builder: (context, child) {
              return Align(
                alignment: Alignment(0, _dropAnimation.value),
                child: Transform.scale(
                  scaleX: 2.0 - _squishAnimation.value,
                  scaleY: _squishAnimation.value,
                  alignment: Alignment.bottomCenter,
                  child: child
                )
              );
            },
            child: Container(height: 50, width: 50, decoration: BoxDecoration(color: Colors.red, shape: BoxShape.circle),),
          ),
          Positioned(
              bottom: 40,
              left: 0,
              right: 0,
              child: Center(
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(backgroundColor: Colors.white),
                  onPressed: () {
                    _controller.repeat();
                  },
                  child: const Text(
                    'Drop Ball',
                    style: TextStyle(color: Colors.black),
                  ),
                ),
              ),
            ),
          ]
        ),
      ),
    );
  }
}
