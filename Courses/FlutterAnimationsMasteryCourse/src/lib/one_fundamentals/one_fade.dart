import 'package:flutter/material.dart';
import 'package:src/widets/core/controls.dart';

class OneScreenFade extends StatefulWidget {
  const OneScreenFade({super.key});

  @override
  State<OneScreenFade> createState() => _OneScreenFadeState();
}

class _OneScreenFadeState extends State<OneScreenFade> with SingleTickerProviderStateMixin {
  late AnimationController _animationController;

  @override
  void initState() {
    super.initState();
    _animationController = AnimationController(
      duration: const Duration(seconds: 1),
      vsync: this,
    );
  }

  @override
  void dispose() {
    _animationController.dispose();
    super.dispose();
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Module One (Fade)')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            AnimatedBuilder(
              animation: _animationController,
              builder: (ctx, child) {
                return Opacity(opacity: _animationController.value, child: child);
              },
              child: Text('Opacity me baby!'),
            ),
            const SizedBox(height: 16),
            Controls(animationController: _animationController,)
        ],),
      )
    );
  }
}