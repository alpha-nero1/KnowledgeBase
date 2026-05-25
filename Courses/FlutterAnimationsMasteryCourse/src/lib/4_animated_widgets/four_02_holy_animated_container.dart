import 'package:flutter/material.dart';
import 'package:src/widets/core/thing.dart';

/// Demonstrates the might of the AnimatedContainer.
///
class Four02HolyAnimatedContainer extends StatefulWidget {
  const Four02HolyAnimatedContainer({super.key});

  @override
  State<Four02HolyAnimatedContainer> createState() => _Four02HolyAnimatedContainerState();
}

class _Four02HolyAnimatedContainerState extends State<Four02HolyAnimatedContainer> {
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
              AnimatedContainer(
                duration: Duration(milliseconds: 300),
                width: double.infinity,
                height: _isExpanded ? 400 : 100,
                color: _isExpanded ? Colors.red : Colors.blue,
                margin: EdgeInsets.all(_isExpanded ? 20 : 12),
                padding: EdgeInsets.all(_isExpanded ? 20 : 12),
                curve: Curves.easeInOut,
                child: Center(child: Thing()),
              ),
              const SizedBox(height: 16,),
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
