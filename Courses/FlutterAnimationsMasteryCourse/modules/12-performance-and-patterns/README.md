# Module 12: Performance, Best Practices & Advanced Patterns

## Overview

Master animators ship performant code. This module teaches profiling, optimization strategies, architectural patterns, and production-grade best practices that separate good animations from professional ones.

**Duration:** 3–4 hours  
**Prerequisites:** Modules 01–11  
**Learning Outcomes:**
- Profile animations using DevTools
- Optimize for 60fps+ (90fps on high-refresh devices)
- Recognize and avoid common performance pitfalls
- Apply professional patterns to complex projects

---

## Key Concepts

### 1. 60fps Goal

**Target:** 60 frames per second = ~16.67ms per frame

Modern phones: 90fps (11ms) or 120fps (8.3ms)

If your animation frame takes >16ms, you drop frames → jank.

### 2. Common Performance Killers

❌ **Rebuilding expensive widgets:**
```dart
AnimatedBuilder(
  animation: controller,
  builder: (context, _) {
    return ExpensiveWidget();  // Rebuilds every frame!
  },
);
```

✅ **Const expensive widgets:**
```dart
AnimatedBuilder(
  animation: controller,
  builder: (context, _) {
    return Container(...);  // Light rebuild
  },
  child: const ExpensiveWidget(),  // Rebuild once
);
```

---

❌ **Animating many objects without batching:**
```dart
// 100 animated containers = 100 AnimationControllers
for (int i = 0; i < 100; i++) {
  AnimatedContainer(...);
}
```

✅ **Batch animations onto one controller:**
```dart
// One controller, 100 tweens with intervals
for (int i = 0; i < 100; i++) {
  final animation = Tween(...).animate(
    CurvedAnimation(
      parent: _controller,
      curve: Interval(start, end),
    ),
  );
}
```

---

❌ **Heavy computations in paint():**
```dart
@override
void paint(Canvas canvas, Size size) {
  // Expensive calculation on every frame
  final points = calculateMandelbrotSet();
  drawPoints(canvas, points);
}
```

✅ **Cache expensive calculations:**
```dart
@override
void paint(Canvas canvas, Size size) {
  // Pre-computed, just render
  drawCachedPoints(canvas, _cachedPoints);
}
```

---

### 3. Profiling with DevTools

**Start profiling:**
1. Run app with `flutter run`
2. Open DevTools: `flutter devtools`
3. Go to **Performance** tab
4. Tap the app to trigger animation
5. Look for **frame rendering times**

**Red = jank (dropped frames)**
**Green = smooth (< 16ms)**

---

## Practical Example: Optimizing a List Animation

❌ **Naive approach:**
```dart
// BAD - rebuilds all children
ListView.builder(
  itemBuilder: (context, index) {
    return AnimatedContainer(  // Individual controller
      width: _widths[index],
      duration: Duration(seconds: 1),
    );
  },
)
```

✅ **Optimized approach:**
```dart
// GOOD - one controller, batched animations
class OptimizedListAnimation extends StatefulWidget {
  final int itemCount;
  const OptimizedListAnimation({required this.itemCount});

  @override
  State<OptimizedListAnimation> createState() => _OptimizedListAnimationState();
}

class _OptimizedListAnimationState extends State<OptimizedListAnimation>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late List<Animation<double>> _widthAnimations;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: Duration(seconds: 2),
      vsync: this,
    );

    _widthAnimations = List.generate(widget.itemCount, (index) {
      final start = (index * 0.05).clamp(0.0, 1.0);
      final end = (start + 0.3).clamp(0.0, 1.0);
      return Tween<double>(begin: 100, end: 300).animate(
        CurvedAnimation(
          parent: _controller,
          curve: Interval(start, end, curve: Curves.easeInOut),
        ),
      );
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
    return ListView.builder(
      itemCount: widget.itemCount,
      itemBuilder: (context, index) {
        return AnimatedBuilder(
          animation: _widthAnimations[index],
          builder: (context, child) {
            return Container(
              width: _widthAnimations[index].value,
              height: 50,
              color: Colors.blue,
            );
          },
        );
      },
    );
  }
}
```

**Benefits:**
- One controller instead of N
- One listener instead of N
- Batched animations in intervals

---

## Practical Example: RepaintBoundary Optimization

```dart
class OptimizedPainter extends StatefulWidget {
  const OptimizedPainter({Key? key}) : super(key: key);

  @override
  State<OptimizedPainter> createState() => _OptimizedPainterState();
}

class _OptimizedPainterState extends State<OptimizedPainter>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _animation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: Duration(seconds: 2),
      vsync: this,
    )..repeat();

    _animation = Tween<double>(begin: 0, end: 360).animate(_controller);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Expensive painter cached with RepaintBoundary
        RepaintBoundary(
          child: AnimatedBuilder(
            animation: _animation,
            builder: (context, _) {
              return CustomPaint(
                painter: ExpensivePainter(angle: _animation.value),
                size: Size(300, 300),
              );
            },
          ),
        ),
        // Other UI here (doesn't rebuild when animation runs)
        const Text('Not affected by animation'),
      ],
    );
  }
}

class ExpensivePainter extends CustomPainter {
  final double angle;
  ExpensivePainter({required this.angle});

  @override
  void paint(Canvas canvas, Size size) {
    // Complex drawing logic
    final path = Path();
    for (int i = 0; i < 1000; i++) {
      final angle = (i / 1000 * 360 + this.angle) * 3.14159 / 180;
      final x = 150 + 100 * cos(angle);
      final y = 150 + 100 * sin(angle);
      if (i == 0) {
        path.moveTo(x, y);
      } else {
        path.lineTo(x, y);
      }
    }
    canvas.drawPath(path, Paint()..color = Colors.blue);
  }

  @override
  bool shouldRepaint(ExpensivePainter oldDelegate) => oldDelegate.angle != angle;
}

import 'dart:math';
```

---

## Best Practices Checklist

### Architecture
- [ ] Animations are self-contained (dispose properly)
- [ ] Controllers encapsulated in custom widgets
- [ ] Reusable animation components in lib/animations/
- [ ] Clear separation: animation logic vs. UI logic

### Performance
- [ ] Frame rendering < 16ms (profile in DevTools)
- [ ] const constructors where possible
- [ ] shouldRepaint returns false when unchanged
- [ ] RepaintBoundary around expensive painters
- [ ] Batch animations on single controller

### Code Quality
- [ ] AnimationControllers always disposed
- [ ] Meaningful variable names (not anim1, anim2)
- [ ] Comments explaining complex curves/timings
- [ ] Tests for animation completion/status

### User Experience
- [ ] Animations have purpose (not just flair)
- [ ] Duration consistent with Material guidelines
- [ ] Curves appropriate to action
- [ ] Accessible (respects reduceMotion setting)

---

## Practical Example: Respecting reduceMotion

Some users prefer reduced animation:

```dart
void _buildAnimation(BuildContext context) {
  final mediaQuery = MediaQuery.of(context);
  final shouldReduceMotion = mediaQuery.disableAnimations;

  final duration = shouldReduceMotion
    ? Duration.zero
    : Duration(seconds: 1);

  AnimatedOpacity(
    opacity: _visible ? 1.0 : 0.0,
    duration: duration,
    child: child,
  );
}
```

---

## Professional Pattern: Animation Coordinator

For complex multi-animation sequences, use a coordinator:

```dart
class AnimationCoordinator {
  final TickerProvider vsync;
  late AnimationController _mainController;
  late List<Animation> _coordinated = [];

  AnimationCoordinator({required this.vsync}) {
    _mainController = AnimationController(
      duration: Duration(seconds: 5),
      vsync: vsync,
    );
  }

  Animation<double> createAnimation({
    required double begin,
    required double end,
    required Interval interval,
    required Curve curve,
  }) {
    final animation = Tween<double>(begin: begin, end: end).animate(
      CurvedAnimation(
        parent: _mainController,
        curve: curve,
      ),
    );
    _coordinated.add(animation);
    return animation;
  }

  void play() => _mainController.forward();
  void pause() => _mainController.stop();
  void reset() => _mainController.reset();

  void dispose() {
    _mainController.dispose();
  }
}
```

---

## Exercises

### Exercise 1: Profile Your Code
Take any animation you've built:
1. Run in profile mode: `flutter run --profile`
2. Open DevTools performance tab
3. Identify bottlenecks
4. Optimize and measure again
5. Document improvements

### Exercise 2: Optimize a List
Create a list with 100 animated items:
1. Measure frame time (likely jank)
2. Refactor to batch animations
3. Measure again (should be smooth)
4. Record before/after performance

### Exercise 3: Accessibility
Build an animation that:
- Respects `disableAnimations` setting
- Works with screen readers
- Doesn't rely on animation for understanding
- Hint: Check MediaQuery.disableAnimations

### Exercise 4: Production Widget Library
Build 5 reusable animation widgets:
- Clean API
- Proper disposal
- Documentation
- Examples
- Ready to ship

---

## Key Takeaways

1. **Profile Early, Optimize Intentionally**: Don't guess
2. **Batch Animations**: One controller beats many
3. **Respect Accessibility**: Not everyone wants animations
4. **Memory Matters**: Dispose properly or leak
5. **Professional Code**: Is readable, tested, and optimized

---

## Next Steps

- Complete all exercises
- Build production animations
- Ready for Capstone: You're now equipped to build complex, performant, professional animations

---

## Resources

- [Flutter Performance](https://flutter.dev/docs/perf)
- [DevTools Profiling](https://flutter.dev/docs/development/tools/devtools/performance)
- [Material Timing Guidelines](https://material.io/design/motion/speed.html)
