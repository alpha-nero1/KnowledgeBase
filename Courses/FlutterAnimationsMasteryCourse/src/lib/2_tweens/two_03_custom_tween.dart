import 'package:flutter/material.dart';

/// lerp = linear interpolation, how a value changes ranging from
/// percentage 0.0 to 1.0
///

class AlignmentTween extends Tween<Alignment> {
  AlignmentTween({required Alignment begin, required Alignment end})
    : super(begin: begin, end: end);

  /// t is our animation variable going from 0.0 to 1.0
  ///
  @override
  Alignment lerp(double t) {
    return Alignment(
      begin!.x + (end!.x - begin!.x) * t,
      begin!.y + (end!.y - begin!.y) * t,
    );
  }
}

class Two03CustomTween extends StatefulWidget {
  const Two03CustomTween({super.key});

  @override
  State<Two03CustomTween> createState() => _Two03CustomTweenState();
}

class _Two03CustomTweenState extends State<Two03CustomTween>
    with SingleTickerProviderStateMixin {
  late AnimationController controller;
  late Animation<Alignment> animation;

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
    animation = AlignmentTween(
      begin: Alignment.topLeft,
      end: Alignment.bottomRight,
    ).animate(controller);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Container(
          width: double.infinity,
          height: double.infinity,
          color: Colors.blue,
          alignment: animation.value,
          child: Container(
            height: 60,
            width: 200,
            decoration: BoxDecoration(
              border: Border.all(color: Colors.grey),
            ),
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
