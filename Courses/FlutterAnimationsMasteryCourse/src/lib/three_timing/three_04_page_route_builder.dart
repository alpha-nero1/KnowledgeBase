import 'package:flutter/material.dart';

Route exampleONLY_createCustomRoute(Widget destinationPage) {
  return PageRouteBuilder(
    pageBuilder: (context, animation, secondaryAnimation) => destinationPage,
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      // 1. Define the entry curve
      final curvedAnimation = CurvedAnimation(
        parent: animation,
        curve: Curves.easeOutCubic, // Snappy start, smooth deceleration
      );

      // 2. Slide Transition (from bottom to top)
      final slideTween = Tween<Offset>(
        begin: const Offset(0.0, 0.2), // Start slightly below the screen
        end: Offset.zero,             // End at original position
      ).animate(curvedAnimation);

      // 3. Fade Transition
      final fadeTween = Tween<double>(
        begin: 0.0,
        end: 1.0,
      ).animate(curvedAnimation);

      // Combine them by nesting the transition widgets
      return FadeTransition(
        opacity: fadeTween,
        child: SlideTransition(
          position: slideTween,
          child: child,
        ),
      );
    },
    transitionDuration: const Duration(milliseconds: 400),
  );
}