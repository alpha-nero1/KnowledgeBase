import 'package:flutter/material.dart';

/// Demonstrates a simple pulse animation, could have also used TweenSequence
/// but instead this manipulates .forward() and .reverse() in sequence to give the pulse
/// impression.
///
class Three03BouncyButton extends StatefulWidget {
  const Three03BouncyButton({super.key});

  @override
  State<Three03BouncyButton> createState() => _Three03BouncyButtonState();
}

class _Three03BouncyButtonState extends State<Three03BouncyButton> with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _bounceAnimation;

  @override void initState() {
    super.initState();
    _controller = AnimationController(vsync: this, duration: Duration(milliseconds: 300));
    _bounceAnimation = Tween<double>(begin: 1, end: 1.1).animate(
      CurvedAnimation(
        parent: _controller, 
        curve: Curves.bounceOut,
        // reverseCurve: Curves.bounceIn
      ));
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(child: 
        Center(
          child: AnimatedBuilder(
            animation: _bounceAnimation,
            builder: (context, child) => Transform.scale(
              scale: _bounceAnimation.value,
              child: ElevatedButton(onPressed: () {
                _controller.forward(from: 0)
                .then((r) {
                  _controller.reverse(from: 1);
                });
              }, child: Text('Click moi!')),
            ),
          ),
        )
      ),
    );
  }
}