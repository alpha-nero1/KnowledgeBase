import 'package:flutter/material.dart';
import 'package:src/widets/core/controls.dart';

class TwoColourTweenScreen extends StatefulWidget {
  const TwoColourTweenScreen({super.key});

  @override
  State<TwoColourTweenScreen> createState() => _TwoColourTweenScreenState();
}

class _TwoColourTweenScreenState extends State<TwoColourTweenScreen>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Color?> _colourAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    )..addListener(() => setState(() {}));
    _colourAnimation = ColorTween(
      begin: Colors.amber,
      end: Colors.blue,
    ).animate(CurvedAnimation(parent: _controller, curve: Curves.easeInOut));
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(body: Container(
      color: _colourAnimation.value,
      child: Center(
        child: Controls(animationController: _controller)
      ),
    ));
  }
}
