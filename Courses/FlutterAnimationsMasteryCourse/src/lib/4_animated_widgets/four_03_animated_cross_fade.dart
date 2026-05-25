import 'package:flutter/material.dart';

/// Demonstrates a AnimatedCrossFade, simply shifting from one
/// little widget to another.
///
class Four03AnimatedCrossFade extends StatefulWidget {
  const Four03AnimatedCrossFade({super.key});

  @override
  State<Four03AnimatedCrossFade> createState() =>
      _Four03AnimatedCrossFadeState();
}

class _Four03AnimatedCrossFadeState
    extends State<Four03AnimatedCrossFade> {
  bool _isExpanded = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              // Like look how many properties you can animate implicitly
              AnimatedCrossFade(
                firstChild: Container(
                  width: 200,
                  height: 200,
                  color: Colors.red,
                  child: const Center(child: Text('First')),
                ),
                secondChild: Container(
                  width: 200,
                  height: 200,
                  color: Colors.blue,
                  child: const Center(child: Text('Second')),
                ),
                crossFadeState: _isExpanded
                    ? CrossFadeState.showSecond
                    : CrossFadeState.showFirst,
                duration: Duration(milliseconds: 300),
              ),
              const SizedBox(height: 16),
              ElevatedButton(
                onPressed: () {
                  setState(() {
                    _isExpanded = !_isExpanded;
                  });
                },
                child: Text('Click me!'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
