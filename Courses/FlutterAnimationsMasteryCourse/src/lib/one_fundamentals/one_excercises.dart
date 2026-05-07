import 'package:flutter/material.dart';

class OneScreenExcercises extends StatefulWidget {
  const OneScreenExcercises({super.key});

  @override
  State<OneScreenExcercises> createState() => _OneScreenExcercisesState();
}

class _OneScreenExcercisesState 
  extends State<OneScreenExcercises> 
  with TickerProviderStateMixin 
{
  late AnimationController _spinAnimationController;

  @override
  void initState() {
    super.initState();
    _spinAnimationController = AnimationController(
      duration: Duration(seconds: 3),
      vsync: this
    );
  }

  @override
  void dispose() {
    super.dispose();
    _spinAnimationController.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return const Placeholder();
  }
}