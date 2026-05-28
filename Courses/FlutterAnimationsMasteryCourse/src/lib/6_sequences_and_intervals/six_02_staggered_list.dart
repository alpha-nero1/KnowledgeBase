import 'package:flutter/material.dart';

const double offsetXDistance = 100;
final six02staggeredListItems = List.generate(10, (i) {
  return 'Item ${i + 1}';
});

class Six02StaggeredList extends StatefulWidget {
  final List<String> items;

  const Six02StaggeredList({super.key, required this.items});

  @override
  State<Six02StaggeredList> createState() => _Six02StaggeredListState();
}

class _Six02StaggeredListState extends State<Six02StaggeredList> with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late List<Animation<Offset>> _animations;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this, duration: Duration(seconds: widget.items.length ~/ 2 + 1));
    _animations = List.generate(widget.items.length, (i) {
      final double start = (i * 0.1).clamp(0, 1);
      final double end = (start + 0.3).clamp(0, 1);

      return Tween<Offset>(
        begin: const Offset(offsetXDistance, 0),
        end: Offset.zero
      ).animate(
        CurvedAnimation(
          parent: _controller, 
          curve: Interval(
            // This is the real thing that is dynamic per list item.
            start, 
            end, 
            curve: Curves.easeOut
          ))
      );
    });

    _controller.forward(from: 0);
  }

  @override
  void dispose() {
    super.dispose();
    _controller.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(body: ListView.builder(
      itemCount: widget.items.length,
      itemBuilder: (ctx, i) {
      return AnimatedBuilder(
        animation: _animations[i], 
        builder: (ctx, child) {
          return Transform.translate(offset: _animations[i].value, child: Opacity(
            opacity: 1 - (_animations[i].value.dx / offsetXDistance).abs(),
            child: ListTile(
              title: Text(widget.items[i]),
              textColor: Colors.white,
            ),
          ),);
        }
      );
    }));
  }
}
