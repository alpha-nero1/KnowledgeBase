import 'package:flutter/material.dart';

/// Demonstrates a very simple but powerful side drawer implementation!
///
class Four04SideDrawer extends StatefulWidget {
  const Four04SideDrawer({super.key});

  @override
  State<Four04SideDrawer> createState() => _Four04SideDrawerState();
}

class _Four04SideDrawerState extends State<Four04SideDrawer> {
  bool _isExpanded = false;

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;

    return Scaffold(
      body: SafeArea(
        child: Stack(
          children: [
            Center(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  ElevatedButton(
                    onPressed: () {
                      setState(() {
                        _isExpanded = !_isExpanded;
                      });
                    },
                    child: const Text('Expand me!'),
                  ),
                ],
              ),
            ),
            AnimatedPositioned(
              left: _isExpanded ? 0 : -size.width,
              top: 0,
              width: 200,
              height: size.height,
              duration: Duration(milliseconds: 200),
              child: AnimatedContainer(
                duration: Duration(milliseconds: 200),
                color: _isExpanded ? Colors.blue : Colors.transparent,
                child: Column(
                  children: [
                    Text('Henooo')
                  ],
                ),
              )
            )
          ],
        ),
      ),
    );
  }
}
