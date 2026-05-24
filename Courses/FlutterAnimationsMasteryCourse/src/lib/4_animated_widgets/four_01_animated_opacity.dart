import 'package:flutter/material.dart';
import 'package:src/widets/core/thing.dart';

class Four01AnimatedOpacity extends StatefulWidget {
  const Four01AnimatedOpacity({super.key});

  @override
  State<Four01AnimatedOpacity> createState() => _Four01AnimatedOpacityState();
}

class _Four01AnimatedOpacityState extends State<Four01AnimatedOpacity> {
  double _opacity = 1.0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              AnimatedOpacity(
                opacity: _opacity, 
                duration: Duration(milliseconds: 300),
                child: Thing(),
              ),
              const SizedBox(height: 16,),
              ElevatedButton(
                onPressed: () {
                  setState(() {
                    _opacity = _opacity == 0
                      ? 1
                      : 0;
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
