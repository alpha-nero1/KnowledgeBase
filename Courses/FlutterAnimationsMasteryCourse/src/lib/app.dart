import 'package:flutter/material.dart';
import 'package:src/one_fundamentals/one.dart';
import 'package:src/one_fundamentals/one_fade.dart';

class App extends StatelessWidget {
  final items = [
    'Module 1 (Basics)',
    'Module 1 (Fade)'
  ];

  App({super.key});

  Future _screenOnNavigate(BuildContext context, int index) async {
    if (index == 0) {
      return Navigator.of(
        context,
      ).push(MaterialPageRoute(builder: (ctx) => OneScreen()));
    }
    if (index == 1) {
      return Navigator.of(
        context,
      ).push(MaterialPageRoute(builder: (ctx) => OneScreenFade()));
    }
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
                title: Text(items[index]),
                onTap: () => _screenOnNavigate(ctx, index),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
