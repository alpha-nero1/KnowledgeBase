# Module 07: Custom Animation Widgets

## Overview

By now you're writing animation code repeatedly. This module teaches you to encapsulate animation logic into reusable custom widgets—the key to maintainable animation code in production apps.

**Duration:** 3 hours  
**Prerequisites:** Modules 01–06  
**Learning Outcomes:**
- Wrap animation controllers in custom widgets
- Create reusable animation behaviors
- Understand widget composition patterns
- Build production-ready animation components

---

## Key Concepts

### 1. Why Custom Animation Widgets?

**Problem:**
Every time you want a fade-in animation, you write:
```dart
late AnimationController _controller;
late Animation<double> _fadeAnimation;

@override
void initState() {
  _controller = AnimationController(...);
  _fadeAnimation = Tween<double>(begin: 0, end: 1).animate(_controller);
}

@override
void dispose() {
  _controller.dispose();
}

// ... build with AnimatedBuilder
```

**Solution:**
Create a `FadeInWidget` that handles all this internally. Reuse everywhere.

### 2. Anatomy of a Custom Animation Widget

```dart
class FadeInWidget extends StatefulWidget {
  final Widget child;
  final Duration duration;
  final Curve curve;
  final VoidCallback? onComplete;

  const FadeInWidget({
    Key? key,
    required this.child,
    this.duration = const Duration(seconds: 1),
    this.curve = Curves.easeInOut,
    this.onComplete,
  }) : super(key: key);

  @override
  State<FadeInWidget> createState() => _FadeInWidgetState();
}

class _FadeInWidgetState extends State<FadeInWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _fadeAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: widget.duration,
      vsync: this,
    );

    _fadeAnimation = Tween<double>(begin: 0, end: 1).animate(
      CurvedAnimation(parent: _controller, curve: widget.curve),
    );

    _controller.addStatusListener((status) {
      if (status == AnimationStatus.completed) {
        widget.onComplete?.call();
      }
    });

    _controller.forward();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _fadeAnimation,
      builder: (context, child) {
        return Opacity(
          opacity: _fadeAnimation.value,
          child: child,
        );
      },
      child: widget.child,
    );
  }
}
```

**Usage:**
```dart
FadeInWidget(
  duration: Duration(seconds: 1),
  curve: Curves.easeOut,
  onComplete: () => print('Done!'),
  child: MyWidget(),
)
```

Much simpler!

---

## Practical Example 1: Slide-In Widget

```dart
class SlideInWidget extends StatefulWidget {
  final Widget child;
  final Duration duration;
  final SlideDirection direction;
  final Curve curve;

  const SlideInWidget({
    Key? key,
    required this.child,
    this.duration = const Duration(seconds: 1),
    this.direction = SlideDirection.fromLeft,
    this.curve = Curves.easeOut,
  }) : super(key: key);

  @override
  State<SlideInWidget> createState() => _SlideInWidgetState();
}

enum SlideDirection { fromLeft, fromRight, fromTop, fromBottom }

class _SlideInWidgetState extends State<SlideInWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: widget.duration,
      vsync: this,
    );

    final beginOffset = _getBeginOffset();
    _slideAnimation = Tween<Offset>(
      begin: beginOffset,
      end: Offset.zero,
    ).animate(
      CurvedAnimation(parent: _controller, curve: widget.curve),
    );

    _controller.forward();
  }

  Offset _getBeginOffset() {
    return switch (widget.direction) {
      SlideDirection.fromLeft => const Offset(-1, 0),
      SlideDirection.fromRight => const Offset(1, 0),
      SlideDirection.fromTop => const Offset(0, -1),
      SlideDirection.fromBottom => const Offset(0, 1),
    };
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _slideAnimation,
      builder: (context, child) {
        return Transform.translate(
          offset: _slideAnimation.value * 100,  // 100 pixels max
          child: child,
        );
      },
      child: widget.child,
    );
  }
}
```

---

## Practical Example 2: Scale-Bounce Widget

```dart
class ScaleBounceWidget extends StatefulWidget {
  final Widget child;
  final Duration duration;
  final double maxScale;

  const ScaleBounceWidget({
    Key? key,
    required this.child,
    this.duration = const Duration(seconds: 1),
    this.maxScale = 1.2,
  }) : super(key: key);

  @override
  State<ScaleBounceWidget> createState() => _ScaleBounceWidgetState();
}

class _ScaleBounceWidgetState extends State<ScaleBounceWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _scaleAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: widget.duration,
      vsync: this,
    );

    _scaleAnimation = Tween<double>(begin: 0.5, end: widget.maxScale)
      .animate(
        CurvedAnimation(
          parent: _controller,
          curve: Curves.elasticOut,
        ),
      );

    _controller.forward();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _scaleAnimation,
      builder: (context, child) {
        return Transform.scale(
          scale: _scaleAnimation.value,
          child: child,
        );
      },
      child: widget.child,
    );
  }
}
```

---

## Practical Example 3: Controllable Custom Widget

Sometimes you want the parent to control the animation:

```dart
class ControllableSlideWidget extends StatefulWidget {
  final Widget child;
  final Duration duration;

  const ControllableSlideWidget({
    Key? key,
    required this.child,
    this.duration = const Duration(seconds: 1),
  }) : super(key: key);

  @override
  State<ControllableSlideWidget> createState() => _ControllableSlideWidgetState();
}

class _ControllableSlideWidgetState extends State<ControllableSlideWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: widget.duration,
      vsync: this,
    );

    _slideAnimation = Tween<Offset>(
      begin: const Offset(0, 0.2),
      end: Offset.zero,
    ).animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeOut),
    );
  }

  // Expose controller to parent
  AnimationController get controller => _controller;

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _slideAnimation,
      builder: (context, child) {
        return Transform.translate(
          offset: _slideAnimation.value,
          child: child,
        );
      },
      child: widget.child,
    );
  }
}

// Usage:
final key = GlobalKey<_ControllableSlideWidgetState>();

ControllableSlideWidget(
  key: key,
  child: MyWidget(),
)

// Later, in button callback:
key.currentState?.controller.reverse();
```

---

## Pattern: Composition Over Inheritance

Instead of deep inheritance, compose:

```dart
// ❌ Don't do this (deep inheritance)
class ScaleFadeSlideWidget extends ScaleWidget {
  // ...
}

// ✅ Do this (composition)
ScaleBounceWidget(
  child: FadeInWidget(
    child: SlideInWidget(
      child: MyWidget(),
    ),
  ),
)
```

---

## Practical Example 4: Reusable Stagger List

```dart
class StaggeredListWidget extends StatefulWidget {
  final List<Widget> children;
  final Duration itemDuration;
  final Duration delayBetweenItems;

  const StaggeredListWidget({
    Key? key,
    required this.children,
    this.itemDuration = const Duration(milliseconds: 300),
    this.delayBetweenItems = const Duration(milliseconds: 100),
  }) : super(key: key);

  @override
  State<StaggeredListWidget> createState() => _StaggeredListWidgetState();
}

class _StaggeredListWidgetState extends State<StaggeredListWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late List<Animation<Offset>> _itemAnimations;

  @override
  void initState() {
    super.initState();
    final totalDuration = (widget.children.length - 1) *
            widget.delayBetweenItems.inMilliseconds +
        widget.itemDuration.inMilliseconds;

    _controller = AnimationController(
      duration: Duration(milliseconds: totalDuration),
      vsync: this,
    );

    _itemAnimations = List.generate(
      widget.children.length,
      (index) {
        final start =
            index * widget.delayBetweenItems.inMilliseconds / totalDuration;
        final end = (start * totalDuration + widget.itemDuration.inMilliseconds) /
            totalDuration;

        return Tween<Offset>(
          begin: const Offset(100, 0),
          end: Offset.zero,
        ).animate(
          CurvedAnimation(
            parent: _controller,
            curve: Interval(start, end, curve: Curves.easeOut),
          ),
        );
      },
    );

    _controller.forward();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: List.generate(
        widget.children.length,
        (index) {
          return AnimatedBuilder(
            animation: _itemAnimations[index],
            builder: (context, child) {
              return Transform.translate(
                offset: _itemAnimations[index].value,
                child: child,
              );
            },
            child: widget.children[index],
          );
        },
      ),
    );
  }
}

// Usage:
StaggeredListWidget(
  children: [
    Text('Item 1'),
    Text('Item 2'),
    Text('Item 3'),
  ],
  itemDuration: Duration(milliseconds: 500),
  delayBetweenItems: Duration(milliseconds: 100),
)
```

---

## Exercises

### Exercise 1: RotateWidget
Create a custom widget that rotates child:
- Takes `rotationsPerSecond` parameter
- Repeats forever
- Hint: Use `repeat()` on controller

### Exercise 2: PulseWidget
Create a widget that pulses (scales 1.0 → 1.1 → 1.0):
- Customizable pulse duration
- Repeats forever
- Hint: Use `reverse()` after forward()

### Exercise 3: FlipWidget
Create a widget that flips 3D-style:
- Flips on tap
- Smoothly animates
- Hint: Use Matrix4 for 3D transform

### Exercise 4: Custom List Animation
Create a list animation widget that:
- Accepts list of items
- Each item animates in with custom curve
- Hint: Extend StaggeredListWidget pattern

---

## Best Practices

1. **Always Provide Defaults**: `duration`, `curve`, callbacks
2. **Expose Callbacks**: `onComplete`, `onStart`, etc.
3. **Handle Lifecycle**: Dispose controllers properly
4. **Consider Composition**: Easier than deep inheritance
5. **Document Parameters**: Clear intent for users

---

## Key Takeaways

1. **Encapsulate Animation Logic**: Hide complexity in custom widgets
2. **Reuse Aggressively**: Don't repeat animation boilerplate
3. **Compose Widgets**: Build complex animations from simple pieces
4. **Provide Control**: Let parents configure duration/curve/callbacks
5. **Production Ready**: These patterns scale to large apps

---

## Next Steps

- Build a library of 5–10 reusable animation widgets
- Use them in real projects
- Ready for Module 08: CustomPaint (more advanced, lower-level rendering)

---

## Resources

- [StatefulWidget Lifecycle](https://api.flutter.dev/flutter/widgets/StatefulWidget-class.html)
- [Widget Composition](https://flutter.dev/docs/development/ui/widgets-intro)
