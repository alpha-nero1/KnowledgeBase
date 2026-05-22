import 'package:flutter/material.dart';

/// Animate multiple (3) properties as once using on controller.
///
class TwoSizeAndPositionScreen extends StatefulWidget {
  const TwoSizeAndPositionScreen({super.key});

  @override
  State<TwoSizeAndPositionScreen> createState() => _TwoSizeAndPositionScreenState();
}

class _TwoSizeAndPositionScreenState extends State<TwoSizeAndPositionScreen>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _sizeAnimation;
  late Animation<Offset> _offsetAnimation;
  late Animation<Color?> _colorAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    )..addListener(() => setState(() {}));

    // Multiple tweens, one controller
    _sizeAnimation = Tween<double>(begin: 50, end: 150)
      .animate(_controller);
    
    _offsetAnimation = Tween<Offset>(
      begin: const Offset(0, 0),
      end: const Offset(100, 50),
    ).animate(_controller);
    
    _colorAnimation = ColorTween(
      begin: Colors.purple,
      end: Colors.orange,
    ).animate(_controller);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Transform.translate(
          offset: _offsetAnimation.value,
          child: Container(
            decoration: BoxDecoration(
              color: _colorAnimation.value,
              borderRadius: BorderRadius.all(Radius.circular(12))
            ),
            height: _sizeAnimation.value,
            width: _sizeAnimation.value,
          ),
        ),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => _controller.forward(),
          child: const Text('Animate'),
        ),
      ],
    );
  }
}
