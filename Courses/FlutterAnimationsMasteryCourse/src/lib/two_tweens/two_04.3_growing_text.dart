import 'package:flutter/material.dart';

class Two043GrowingText extends StatefulWidget {
  const Two043GrowingText({super.key});

  @override
  State<Two043GrowingText> createState() => _Two043GrowingTextState();
}

class _Two043GrowingTextState extends State<Two043GrowingText>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _fontAnimation;
  late Animation<Color?> _colourAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: Duration(seconds: 1),
    );
    _fontAnimation = Tween<double>(
      begin: 12,
      end: 48,
    ).animate(CurvedAnimation(parent: _controller, curve: Curves.easeInOut));
    _colourAnimation = ColorTween(
      begin: Colors.black,
      end: Colors.blue,
    ).animate(CurvedAnimation(parent: _controller, curve: Curves.easeInOut));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            /// Animated builder seems to be the standard by kinda encapsulating the animation
            /// logic in this builder method as opposed to just trigger setState() randomly on listener.
            AnimatedBuilder(
              animation: _controller,
              builder: (context, child) => Text(
                'Hello',
                style: TextStyle(
                  color: _colourAnimation.value,
                  fontSize: _fontAnimation.value,
                ),
              ),
            ),
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
