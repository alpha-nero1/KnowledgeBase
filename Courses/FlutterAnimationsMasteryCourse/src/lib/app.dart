import 'package:flutter/material.dart';
import 'package:src/one_fundamentals/one.dart';
import 'package:src/one_fundamentals/one_excercises.dart';
import 'package:src/one_fundamentals/one_fade.dart';
import 'package:src/three_timing/three_01_curve_comparison.dart';
import 'package:src/three_timing/three_02_staggered_animation.dart';
import 'package:src/three_timing/three_03_bouncy_button.dart';
import 'package:src/two_tweens/two_02_chain_size_and_position.dart';
import 'package:src/two_tweens/two_01_colour_tween.dart';
import 'package:src/two_tweens/two_03_custom_tween.dart';
import 'package:src/two_tweens/two_04.1_gradient_chain.dart';
import 'package:src/two_tweens/two_04.2.5_bouncy_ball_dissipate.dart';
import 'package:src/two_tweens/two_04.2_bouncy_ball.dart';
import 'package:src/two_tweens/two_04.3_growing_text.dart';
import 'package:src/two_tweens/two_04.4_border_radius_tween.dart';

class App extends StatelessWidget {
  final List<({ String title, Widget widget })> items = [
    (title: 'Module 1 (Basics)', widget: OneScreen()),
    (title: 'Module 1 (Fade)', widget: OneScreenFade()),
    (title: 'Module 1 (Exercises)', widget: OneScreenExcercises()),
    (title: 'Module 2 (ColorTween)', widget: TwoColourTweenScreen()),
    (title: 'Module 2 (Animate Size, Position & Colour simultaneously)', widget: TwoSizeAndPositionScreen()),
    (title: 'Module 2 (Custom Tween)', widget: Two03CustomTween()),
    (title: 'Module 2 (Gradient chain of animation)', widget: Two04GradientChain()),
    (title: 'Module 2 (Bouncy ball)', widget: Two04BouncyBall()),
    (title: 'Module 2 (Bouncy ball disspate)', widget: Two042DissipatingBouncyBall()),
    (title: 'Module 2 (Growing text)', widget: Two043GrowingText()),
    (title: 'Module 2 (Border radius custom tween)', widget: Two044BorderRadiusTween()),
    (title: 'Module 3 (Curve comparison)', widget: Three01CurveComparison()),
    (title: 'Module 3 (Staggered animation)', widget: Three02StaggeredAnimation()),
    (title: 'Module 3 (Bounce out button)', widget: Three03BouncyButton())
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
