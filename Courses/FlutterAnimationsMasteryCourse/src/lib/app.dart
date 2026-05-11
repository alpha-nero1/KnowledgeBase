import 'package:flutter/material.dart';
import 'package:src/one_fundamentals/one.dart';
import 'package:src/one_fundamentals/one_excercises.dart';
import 'package:src/one_fundamentals/one_fade.dart';
import 'package:src/two_tweens/two_chain_size_and_position.dart';
import 'package:src/two_tweens/two_colour_tween.dart';

class App extends StatelessWidget {
  final List<({ String title, Widget widget })> items = [
    (title: 'Module 1 (Basics)', widget: OneScreen()),
    (title: 'Module 1 (Fade)', widget: OneScreenFade()),
    (title: 'Module 1 (Exercises)', widget: OneScreenExcercises()),
    (title: 'Module 2 (ColorTween)', widget: TwoColourTweenScreen()),
    (title: 'Module 2 (Animate Size, Position & Colour simultaneously)', widget: TwoSizeAndPositionScreen())
  ];

  App({super.key});

  Future _screenOnNavigate(BuildContext context, int index) async {
    return Navigator.of(
      context,
    ).push(MaterialPageRoute(builder: (ctx) => items[index].widget));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          const SizedBox(height: 32),
          Expanded(
            child: ListView.builder(
              itemCount: items.length,
              itemBuilder: (ctx, index) => ListTile(
                title: Text(items[items.length - (index + 1)].title),
                onTap: () => _screenOnNavigate(ctx, items.length - (index + 1)),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
