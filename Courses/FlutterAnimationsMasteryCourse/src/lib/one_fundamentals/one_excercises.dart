import 'dart:math';

import 'package:flutter/material.dart';
import 'package:src/widets/core/controls.dart';

class OneScreenExcercises extends StatefulWidget {
  const OneScreenExcercises({super.key});

  @override
  State<OneScreenExcercises> createState() => _OneScreenExcercisesState();
}

class _OneScreenExcercisesState extends State<OneScreenExcercises>
    with TickerProviderStateMixin {
  late AnimationController _spinAnimationController;
  late Animation<double> _spinAnimation;
  late AnimationController _pulseAnimationController;
  late Animation<double> _pulseAnimation;

  @override
  void initState() {
    super.initState();
    _spinAnimationController = AnimationController(
      duration: Duration(seconds: 3),
      vsync: this,
    );
    _spinAnimationController.addListener(() {
      setState(() {});
    });
    _spinAnimation = Tween<double>(begin: 0, end: pi * 6).animate(
      CurvedAnimation(parent: _spinAnimationController, curve: Curves.linear),
    );
    _pulseAnimationController = AnimationController(
      duration: Duration(milliseconds: 600),
      vsync: this,
    );
    _pulseAnimation = Tween<double>(begin: 1, end: 1.2).animate(
      CurvedAnimation(
        parent: _pulseAnimationController,
        curve: Curves.easeIn,
      ),
    );
    _pulseAnimationController.addListener(() => setState(() {}));
  }

  @override
  void dispose() {
    super.dispose();
    _spinAnimationController.dispose();
    _pulseAnimationController.dispose();
  }

  Widget buildSpinner() {
    return Column(
      children: [
        Text('Spin animation', style: TextStyle(fontSize: 24)),
        Transform.rotate(
          angle: _spinAnimation.value,
          child: Text('🐶', style: TextStyle(fontSize: 48)),
        ),
        Controls(animationController: _spinAnimationController),
      ],
    );
  }

  Widget buildPulse() {
    return Column(
      children: [
        Text('Pulse animation', style: TextStyle(fontSize: 24)),
        Transform.scale(
          scale: _pulseAnimation.value,
          child: Text('🐮', style: TextStyle(fontSize: 48)),
        ),
        FilledButton(
          onPressed: () {
            _pulseAnimationController.forward()
            .then((_) {
              _pulseAnimationController.reverse();
            });
          },
          child: Text('Pulse!'),
        ),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [buildSpinner(), const SizedBox(height: 32), buildPulse()],
        ),
      ),
    );
  }
}
