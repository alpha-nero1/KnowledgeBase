import 'dart:ui';

import 'package:flutter/material.dart';

class Three02StaggeredAnimation extends StatefulWidget {
  const Three02StaggeredAnimation({super.key});

  @override
  State<Three02StaggeredAnimation> createState() =>
      _Three02StaggeredAnimationState();
}

class _Three02StaggeredAnimationState extends State<Three02StaggeredAnimation>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _box1Animation;
  late Animation<Offset> _box2Animation;
  late Animation<Offset> _box3Animation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 3),
      vsync: this,
    );

    // Box 1: slides from 0% to 50%
    _box1Animation =
        Tween<Offset>(
          begin: const Offset(0, 0),
          end: const Offset(200, 0),
        ).animate(
          CurvedAnimation(
            parent: _controller,
            curve: const Interval(0.0, 0.33, curve: Curves.easeOut),
          ),
        );

    // Box 2: slides from 33% to 66%
    _box2Animation =
        Tween<Offset>(
          begin: const Offset(0, 0),
          end: const Offset(200, 0),
        ).animate(
          CurvedAnimation(
            parent: _controller,
            curve: const Interval(0.33, 0.66, curve: Curves.easeOut),
          ),
        );

    // Box 3: slides from 66% to 100%
    _box3Animation =
        Tween<Offset>(
          begin: const Offset(0, 0),
          end: const Offset(200, 0),
        ).animate(
          CurvedAnimation(
            parent: _controller,
            curve: const Interval(0.66, 1.0, curve: Curves.easeOut),
          ),
        );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            AnimatedBuilder(
              animation: Listenable.merge([
                _box1Animation,
                _box2Animation,
                _box3Animation,
              ]),
              builder: (context, child) {
                return Column(
                  children: [
                    Transform.translate(
                      offset: _box1Animation.value,
                      child: Container(
                        width: 50,
                        height: 50,
                        color: Colors.red,
                      ),
                    ),
                    Transform.translate(
                      offset: _box2Animation.value,
                      child: Container(
                        width: 50,
                        height: 50,
                        color: Colors.green,
                      ),
                    ),
                    Transform.translate(
                      offset: _box3Animation.value,
                      child: Container(
                        width: 50,
                        height: 50,
                        color: Colors.blue,
                      ),
                    ),
                  ],
                );
              },
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () => _controller.forward(from: 0),
              child: const Text('Stagger'),
            ),
          ],
        ),
      ),
    );
  }
}
