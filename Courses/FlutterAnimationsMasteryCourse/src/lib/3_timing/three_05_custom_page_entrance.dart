import 'package:flutter/material.dart';

/// Wow how nice and instructive this one is!
/// Look how Interval() is used as the param for curve and how I can therefore
/// define transitions to occur at different times during the whole duration.
///
/// Also key to note that you use the animation controller provided to you via transitionsBuilder.
///
Route customPageAnimation(Widget destinationPage) {
  return PageRouteBuilder(
    pageBuilder: (ctx, animation, secondaryAnimation) => destinationPage,
    // This is where you can modify the transition! in transitionsBuilder!
    transitionsBuilder: (ctx, animation, secondaryAnimation, child) {
      final firstHalfAnimation = CurvedAnimation(
        parent: animation,
        // Interval extends curve!!
        curve: const Interval(0, 0.5, curve: Curves.easeOutCubic),
      );
      final fullDurationAnimation = CurvedAnimation(
        parent: animation,
        // Interval extends curve!!
        curve: const Interval(0, 1, curve: Curves.easeOutCubic),
      );

      // Start the offset -50px on the y axis then come up!
      final slideUpTween = Tween<Offset>(
        begin: Offset(0, 50),
        end: Offset.zero,
      ).animate(fullDurationAnimation);

      final fadeInTween = Tween<double>(
        begin: 0,
        end: 1,
      ).animate(firstHalfAnimation);

      final scaleUpTween = Tween<double>(
        begin: 0.8,
        end: 1,
      ).animate(firstHalfAnimation);

      // Position takes an offset tween, queen!
      return ScaleTransition(
        scale: scaleUpTween,
        child: FadeTransition(
          opacity: fadeInTween,
          child: SlideTransition(position: slideUpTween, 
            child: child
          ),
        ),
      );
    },
    transitionDuration: Duration(milliseconds: 300),
  );
}

/// Completely custom page navigaton using EXPLICIT animations!
///
class Three05CustomPageEntrance extends StatelessWidget {
  const Three05CustomPageEntrance({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.blueAccent,
      body: SafeArea(
        child: Center(
          child: ElevatedButton(
            onPressed: () {
              Navigator.of(
                context,
              ).push(customPageAnimation(Three05CustomPageEntrance()));
            },
            child: Text('Navigate hombre!'),
          ),
        ),
      ),
    );
  }
}
