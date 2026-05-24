import 'package:flutter/material.dart';

/// Basic visual curve comparisons.
///
class Three01CurveComparison extends StatefulWidget {
  const Three01CurveComparison({super.key});

  @override
  State<Three01CurveComparison> createState() => _CurveComparisonState();
}

class _CurveComparisonState extends State<Three01CurveComparison>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _linearAnimation;
  late Animation<Offset> _easeInAnimation;
  late Animation<Offset> _easeOutAnimation;
  late Animation<Offset> _bounceAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    );

    // Same tween, different curves
    final tween = Tween<Offset>(
      begin: const Offset(0, 0),
      end: const Offset(200, 0),
    );

    _linearAnimation = tween.animate(_controller);
    
    _easeInAnimation = tween.animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeIn),
    );
    
    _easeOutAnimation = tween.animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeOut),
    );
    
    _bounceAnimation = tween.animate(
      CurvedAnimation(parent: _controller, curve: Curves.bounceOut),
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
      body: SafeArea(child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            _buildAnimatedBox('Linear', _linearAnimation),
            _buildAnimatedBox('Ease In', _easeInAnimation),
            _buildAnimatedBox('Ease Out', _easeOutAnimation),
            _buildAnimatedBox('Bounce', _bounceAnimation),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () => _controller.forward(from: 0),
              child: const Text('Animate'),
            ),
          ],
        ),
      ))
    );
  }

  Widget _buildAnimatedBox(String label, Animation<Offset> animation) {
    return AnimatedBuilder(
      animation: animation,
      builder: (context, child) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(label, style: const TextStyle(fontSize: 12)),
            const SizedBox(height: 8),
            Transform.translate(
              offset: animation.value,
              child: Container(
                width: 50,
                height: 50,
                color: Colors.blue,
              ),
            ),
            const SizedBox(height: 16),
          ],
        );
      },
    );
  }
}