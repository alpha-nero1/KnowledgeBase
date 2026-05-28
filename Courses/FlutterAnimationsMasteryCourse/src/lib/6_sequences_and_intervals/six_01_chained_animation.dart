import 'dart:math';

import 'package:flutter/material.dart';
import 'package:src/widets/core/action_button.dart';

class Six01ChainedAnimation extends StatefulWidget {
  const Six01ChainedAnimation({super.key});

  @override
  State<Six01ChainedAnimation> createState() => _Six01ChainedAnimationState();
}

class _Six01ChainedAnimationState extends State<Six01ChainedAnimation> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: ActionButton('Show dialog', () {
          showDialog(context: context, builder: (ctx) => Dialog());
        }),
      ),
    );
  }
}

/// ----- Dialog section ----- ///

class Dialog extends StatefulWidget {
  const Dialog({super.key});

  @override
  State<Dialog> createState() => _DialogState();
}

class _DialogState extends State<Dialog> with TickerProviderStateMixin {
  late AnimationController _controller1;
  late AnimationController _controller2;
  late AnimationController _controller3;
  late Animation<double> _animation1;
  late Animation<double> _animation2;
  late Animation<double> _animation3;
  int _activeStep = 1;

  @override
  void initState() {
    super.initState();
    _controller1 =
        AnimationController(duration: const Duration(seconds: 1), vsync: this)
          ..addStatusListener((status) {
            if (status == AnimationStatus.completed) {
              setState(() => _activeStep = 2);
              _controller2.forward(from: 0);
            }
          });
    _controller2 =
        AnimationController(duration: const Duration(seconds: 1), vsync: this)
          ..addStatusListener((status) {
            if (status == AnimationStatus.completed) {
              setState(() => _activeStep = 3);
              _controller3.forward(from: 0);
            }
          });
    _controller3 = AnimationController(
      duration: const Duration(seconds: 1),
      vsync: this,
    );

    _animation1 = Tween<double>(begin: 0, end: 1).animate(_controller1);
    _animation2 = Tween<double>(begin: 0, end: 1).animate(_controller2);
    _animation3 = Tween<double>(begin: 0, end: 1).animate(_controller3);

    _controller1.forward(from: 0);
  }

  @override
  void dispose() {
    super.dispose();
    _controller1.dispose();
    _controller2.dispose();
    _controller3.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text('Processing...'),
      content: SizedBox(
        height: 400,
        width: 300,
        child: Stack(
          children: [
            // Step 1
            if (_activeStep == 1)
              Align(
                alignment: Alignment.center,
                child: AnimatedBuilder(
                  animation: _animation1,
                  builder: (ctx, child) => Transform.rotate(
                    angle: _animation1.value * 2 * pi,
                    child: Opacity(
                      opacity: _animation1.value,
                      child: SizedBox(
                        width: 50,
                        height: 50,
                        child: CircularProgressIndicator(
                          value: _animation1.value,
                        ),
                      ),
                    ),
                  ),
                ),
              ),
            // Step 2
            if (_activeStep == 2)
              Align(
                alignment: Alignment.center,
                child: AnimatedBuilder(
                  animation: _animation2,
                  builder: (context, child) {
                    return Opacity(
                      opacity: _animation2.value,
                      child: Icon(
                        Icons.check_circle,
                        size: 50 + _animation2.value * 10,
                        color: Colors.green,
                      ),
                    );
                  },
                ),
              ),
            // Step 3
            if (_activeStep == 3)
              Align(
                alignment: Alignment.center,
                child: AnimatedBuilder(
                  animation: _animation3,
                  builder: (context, child) {
                    return Opacity(
                      opacity: _animation3.value,
                      child: Transform.translate(
                        offset: Offset(0, (1 - _animation3.value) * 20),
                        child: const Text('Complete!'),
                      ),
                    );
                  },
                ),
              ),
          ],
        ),
      ),
    );
  }
}
