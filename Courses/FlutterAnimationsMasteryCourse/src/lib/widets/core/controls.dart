import 'package:flutter/material.dart';

class Controls extends StatelessWidget {
  final AnimationController animationController;
  const Controls({super.key, required this.animationController});

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.min,
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        // Play
        IconButton.filled(
          onPressed: () {
            animationController.forward();
          },
          icon: Icon(Icons.play_arrow),
        ),
        const SizedBox(width: 16),
        // Reverse
        IconButton.filled(
          onPressed: () {
            animationController.reverse();
          },
          icon: Icon(Icons.fast_rewind),
        ),
        const SizedBox(width: 16),
        // Play repeat
        IconButton.filled(
          onPressed: () {
            animationController.repeat();
          },
          icon: Icon(Icons.replay),
        ),
        const SizedBox(width: 16),
        // Play repeat bounce
        IconButton.filled(
          onPressed: () {
            animationController.repeat(reverse: true);
          },
          icon: Icon(Icons.replay_circle_filled),
        ),
        const SizedBox(width: 16),
        IconButton.filled(
          onPressed: () {
            animationController.stop();
            animationController.value = 0;
          },
          icon: Icon(Icons.stop),
        ),
      ],
    );
  }
}
