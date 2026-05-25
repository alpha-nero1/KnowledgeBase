import 'dart:async';
import 'package:flutter/material.dart';
import 'package:src/widets/core/action_button.dart';

final animationDuration = Duration(seconds: 1);

/// Using a periodic timer to trigger a loading indicator indefinitely.
/// 
/// In my opinion explcit is better in this scenario.
///
class Four06ImplicitSpinner extends StatefulWidget {
  const Four06ImplicitSpinner({super.key});

  @override
  State<Four06ImplicitSpinner> createState() => _Four06ImplicitSpinnerState();
}

class _Four06ImplicitSpinnerState extends State<Four06ImplicitSpinner> {
  late Timer animationTimer;
  double _turns = 0;
  double _scale = 1;
  double _opacity = 1;
  bool _isAnimating = false;

  @override
  void dispose() {
    super.dispose();
    animationTimer.cancel();
  }

  void animate() {
    setState(() {
      _isAnimating = true;
      _turns += 1;
      // Could have used a different time for these two but you get the jist!
      _scale = _turns % 2 == 0 ? 1.0 : 0.5;
      _opacity = _turns % 2 == 0 ? 1.0 : 0.3;
    });
  }

  void stop() {
    // Do not reset turns! as this will cause flutter to
    // rollback all de turns you made.
    setState(() {
      _isAnimating = false;
      _scale = 1;
      _opacity = 1;
    });
  }

  void animateMe() {
    // Back out if animating.
    if (_isAnimating) {
      animationTimer.cancel();
      stop();
      return;
    }
    animate();
    animationTimer = Timer.periodic(animationDuration, (_) {
      if (!_isAnimating) return;
      animate();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Spacer(),
              AnimatedRotation(
                duration: animationDuration,
                turns: _turns,
                child: AnimatedScale(
                  scale: _scale,
                  duration: animationDuration,
                  child: AnimatedOpacity(
                    opacity: _opacity,
                    duration: animationDuration,
                    child: Text('I spin'),
                  ),
                )
              ),
              Spacer(),
              ActionButton(_isAnimating ? 'Stop' : 'Animate me!', animateMe)
          ],),
        ),
      ),
    );
  }
}
