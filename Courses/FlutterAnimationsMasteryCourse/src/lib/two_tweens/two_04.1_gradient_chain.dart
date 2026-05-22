import 'package:flutter/material.dart';


class Two04GradientChain extends StatefulWidget {
  const Two04GradientChain({super.key});

  @override
  State<Two04GradientChain> createState() => _Two04GradientState();
}

class _Two04GradientState extends State<Two04GradientChain>
    with SingleTickerProviderStateMixin {
  late AnimationController controller;
  late Animation<Color?> animation;

  @override
  void initState() {
    super.initState();
    controller = AnimationController(
      vsync: this,
      duration: Duration(milliseconds: 3000),
    )
    ..addListener(() {
      setState(() {
        
      });
    });
    animation = TweenSequence<Color?>(
      [
        TweenSequenceItem<Color?>(
          tween: ColorTween(begin: Colors.red, end: Colors.yellow),
          weight: 50.0, // Takes up 50% of the total time (2 seconds)
        ),
        TweenSequenceItem<Color?>(
          tween: ColorTween(begin: Colors.yellow, end: Colors.blue),
          weight: 50.0, // Takes up the remaining 50% of the time (2 seconds)
        ),
      ]
    ).animate(controller);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Container(
          width: double.infinity,
          height: double.infinity,
          color: animation.value,
          alignment: Alignment.bottomCenter,
          child: SizedBox(
            height: 60,
            width: 200,
            child: ElevatedButton(
              onPressed: () {
                controller.forward(from: 0);
              },
              child: Text('Animate me!'),
            ),
          ),
        ),
      ),
    );
  }
}
