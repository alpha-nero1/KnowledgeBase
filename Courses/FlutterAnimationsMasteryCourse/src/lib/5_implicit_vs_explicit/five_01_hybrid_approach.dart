import 'dart:async';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:src/widets/core/action_button.dart';

/// Demonstrates the use of implcit and explicit animations together.
///
class Five01HybridApproach extends StatefulWidget {
  const Five01HybridApproach({super.key});

  @override
  State<Five01HybridApproach> createState() => _Five01HybridApproachState();
}

class _Five01HybridApproachState extends State<Five01HybridApproach> with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _animation;
  double _scale = 1;
  Timer? _timer;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this, duration: Duration(milliseconds: 2000));
    _animation = Tween<double>(begin: 0, end: pi * 2).animate(_controller);
  }

  @override
  void dispose() {
    super.dispose();
    _controller.dispose();
    _timer?.cancel();
  }

  void _animationOnStart() {
    setState(() {
      _scale = 1.3;
    });
    if (_controller.isAnimating) {
      _controller.stop();
    } else {
      _controller.repeat();
    }
    _timer?.cancel();
    _timer = Timer(Duration(milliseconds: 150), () {
      setState(() {
        _scale = 1;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        color: Colors.amber,
        child: SafeArea(
          child: Stack(
            children: [
              AnimatedBuilder(
                animation: _controller,
                builder: (context, child) => Transform.rotate(
                  angle: _animation.value,
                  child: SizedBox.expand(child: Container(color: Colors.blue,)),
                ),
              ),
              Align(
                alignment: Alignment.center,
                child: AnimatedScale(
                  scale: _scale,
                  duration: Duration(milliseconds: 150),
                  curve: Curves.easeInOut,
                  child: ActionButton('Click me!', _animationOnStart)
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
