import 'package:flutter/material.dart';
import 'package:src/app.dart';
import 'package:src/widets/core/controls.dart';

class OneScreen extends StatefulWidget {
  const OneScreen({super.key});

  @override
  State<OneScreen> createState() => _OneScreenState();
}

class _OneScreenState extends State<OneScreen>
    with SingleTickerProviderStateMixin {
  late AnimationController _animationController;
  late Animation<double> _countAnimation;
  int _counter = 0;

  @override
  void initState() {
    super.initState();
    _animationController = AnimationController(
      duration: Duration(seconds: 100),
      vsync: this,
    );
    _animationController.addStatusListener((s) {
      if (s == AnimationStatus.completed) {
        print('Completed');
      }
    });
    _countAnimation = Tween<double>(begin: 0, end: 100)
      .animate(_animationController);
    _countAnimation.addListener(() {
      setState(() {
        final value = _countAnimation.value;
        _counter = (value).toInt();
      });
    });
  }

  @override
  void dispose() {
    super.dispose();
    _animationController.dispose();
  }

  void _forward() {
    _animationController.forward();
  }

  void _reverse() {
    _animationController.reverse();
  }

  void _repeat() {
    _animationController.repeat();
  }

  void _repeatBounce() {
    _animationController.repeat(reverse: true);
  }

  void _cancel() {
    _animationController.reset();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Module One (Basics)'),),
      body: Center(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text('$_counter'),
            const SizedBox(height: 16),
            Controls(animationController: _animationController,)
          ],
        ),
      ),
    );
  }
}
