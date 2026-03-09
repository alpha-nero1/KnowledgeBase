import 'package:flutter/material.dart';
import 'dart:math';

// Need create only once, once single memory.
final random = Random();

class DiceRoller extends StatefulWidget {
  const DiceRoller({ super.key });

  @override
  State<StatefulWidget> createState() {
    return _DiceRollerState();
  }
}

class _DiceRollerState extends State<DiceRoller> {
  var number = 0;

  // Need to use setState() to see updates.
  void rollDiceOnPress() {
    setState(() {
      number = random.nextInt(6) + 1;
    });
  }
  
  @override
  Widget build(context) {
    return Column(
      // Go against the default
      mainAxisSize: MainAxisSize.min,
      children: [
        Image.asset('assets/images/dice-$number.png', width: 200),
        // I prefer this way.
        // Sized box has a fixed width and height
        const SizedBox(height: 20),
        TextButton(
          onPressed: rollDiceOnPress,
          style: TextButton.styleFrom(
            // Way to do padding!
            // padding: EdgeInsets.only(top: 20),
            foregroundColor: Colors.white,
            textStyle: TextStyle(fontSize: 28)
          ),
          child: const Text('Roll dice'),
        ),
      ],
    );
  }
}