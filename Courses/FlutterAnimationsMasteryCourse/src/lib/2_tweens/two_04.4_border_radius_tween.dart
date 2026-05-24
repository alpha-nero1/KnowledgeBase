import 'package:flutter/material.dart';

class BorderRadiusTween extends Tween<double> {
  BorderRadiusTween({required super.begin, required super.end});

  @override
  double lerp(double t) {
    return begin! + (end! * t);
  }
}

class Two044BorderRadiusTween extends StatefulWidget {
  const Two044BorderRadiusTween({super.key});

  @override
  State<Two044BorderRadiusTween> createState() => _Two044BorderRadiusTweenState();
}

class _Two044BorderRadiusTweenState extends State<Two044BorderRadiusTween>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _borderRadiusAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: Duration(milliseconds: 500),
    );
    _borderRadiusAnimation = BorderRadiusTween(
      begin: 0,
      end: 25,
    ).animate(_controller);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            AnimatedBuilder(
              animation: _borderRadiusAnimation,
              builder: (ctx, builder) {
                return Container(
                  width: 50,
                  height: 50,
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(
                      _borderRadiusAnimation.value,
                    ),
                    color: Colors.red
                  ),
                );
              },
            ),
            const SizedBox(height: 16,),
            ElevatedButton(
              onPressed: () {
                if (_controller.isAnimating) {
                  return;
                }
                if (_controller.value == 0) {
                  _controller.forward(from: 0);
                } else {
                  _controller.reverse(from: 1);
                }
              },
              child: Text('Click me'),
            ),
          ],
        ),
      ),
    );
  }
}
