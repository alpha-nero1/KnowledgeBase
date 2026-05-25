import 'package:flutter/material.dart';
import 'package:src/widets/core/action_button.dart';

final animationDuration = Duration(milliseconds: 300);

class Colours {
  final Color background;
  final Color text;

  Colours({required this.background, required this.text});
}

/// Using AnimatedContainer & AnimatedDefaultTextStyle to implement theme changing!
/// 
class Four07ThemeToggle extends StatefulWidget {
  const Four07ThemeToggle({super.key});

  @override
  State<Four07ThemeToggle> createState() => _Four07ThemeToggleState();
}

class _Four07ThemeToggleState extends State<Four07ThemeToggle> {
  bool _isDark = false;

  Colours getTheme() {
    if (_isDark) {
      return Colours(text: Colors.white, background: Colors.black87);
    }
    return Colours(text: Colors.black, background: Colors.lightBlue);
  }

  void toggleTheme() {
    setState(() {
      _isDark = !_isDark;
    });
  }

  @override
  Widget build(BuildContext context) {
    final theme = getTheme();
    return Scaffold(
      body: AnimatedContainer(
        color: theme.background,
        duration: animationDuration,
        // AnimatedDefaultTextStyle that is unreal!
        // Now I do not need to pass text colour anywhere!
        // they have an implicit animated widget for default text style!
        child: AnimatedDefaultTextStyle(
          style: TextStyle(color: theme.text, fontSize: 16),
          duration: animationDuration,
          child: SafeArea(
            child: Center(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  const Text('Theme Demo!'),
                  const Spacer(),
                  ActionButton(_isDark ? 'White theme' : 'Dark theme', toggleTheme)
              ],),
            ),
          ),
        ),
      ),
    );
  }
}
