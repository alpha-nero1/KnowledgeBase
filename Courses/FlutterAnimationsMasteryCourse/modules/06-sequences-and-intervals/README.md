# Module 06: Sequences, Intervals & Staggered Animations

## Overview

Real animations rarely run in isolation. This module teaches you to coordinate multiple animations: sequences (one after another), parallel (all at once), and staggered (cascading). These techniques create sophisticated, polished effects.

**Duration:** 3–4 hours  
**Prerequisites:** Modules 01–05  
**Learning Outcomes:**
- Sequence animations (A finishes → B starts)
- Run animations in parallel with timing
- Create staggered effects for lists and grids
- Use Interval for precise timing control
- Common orchestration patterns

---

## Key Concepts

### 1. Sequential Animations (One After Another) [DONE]

Use `addStatusListener()` to trigger the next animation:

```dart
@override
void initState() {
  super.initState();
  _controller = AnimationController(
    duration: const Duration(seconds: 1),
    vsync: this,
  );

  _scaleAnimation = Tween<double>(begin: 0, end: 1)
    .animate(_controller);

  // When scale finishes, fade in
  _controller.addStatusListener((status) {
    if (status == AnimationStatus.completed) {
      _fadeIn();
    }
  });
}

void _fadeIn() {
  _fadeController.forward();
}
```

### 2. Interval: Timing Within a Single Controller [DONE]

Run multiple tweens at different time ranges in one controller:

```dart
// Same 3-second controller, different animations
_scaleAnimation = Tween<double>(begin: 0, end: 1)
  .animate(
    CurvedAnimation(
      parent: _controller,
      curve: const Interval(0.0, 0.33),  // First 1 second
    ),
  );

_rotateAnimation = Tween<double>(begin: 0, end: 2 * 3.14159)
  .animate(
    CurvedAnimation(
      parent: _controller,
      curve: const Interval(0.33, 0.66),  // Second 1 second
    ),
  );

_fadeAnimation = Tween<double>(begin: 0, end: 1)
  .animate(
    CurvedAnimation(
      parent: _controller,
      curve: const Interval(0.66, 1.0),  // Final 1 second
    ),
  );
```

---

## Practical Example: Loading Dialog Sequence [DONE]

Three animations in sequence:

```dart
class SequentialLoadingDialog extends StatefulWidget {
  const SequentialLoadingDialog({Key? key}) : super(key: key);

  @override
  State<SequentialLoadingDialog> createState() => _SequentialLoadingDialogState();
}

class _SequentialLoadingDialogState extends State<SequentialLoadingDialog>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller1;
  late AnimationController _controller2;
  late AnimationController _controller3;
  late Animation<double> _animation1;
  late Animation<double> _animation2;
  late Animation<double> _animation3;

  @override
  void initState() {
    super.initState();
    _controller1 = AnimationController(
      duration: const Duration(seconds: 1),
      vsync: this,
    );
    _controller2 = AnimationController(
      duration: const Duration(seconds: 1),
      vsync: this,
    );
    _controller3 = AnimationController(
      duration: const Duration(seconds: 1),
      vsync: this,
    );

    _animation1 = Tween<double>(begin: 0, end: 1).animate(_controller1);
    _animation2 = Tween<double>(begin: 0, end: 1).animate(_controller2);
    _animation3 = Tween<double>(begin: 0, end: 1).animate(_controller3);

    // Chain them: 1 → 2 → 3
    _controller1.addStatusListener((status) {
      if (status == AnimationStatus.completed) {
        _controller2.forward();
      }
    });

    _controller2.addStatusListener((status) {
      if (status == AnimationStatus.completed) {
        _controller3.forward();
      }
    });

    // Start the sequence
    _controller1.forward();
  }

  @override
  void dispose() {
    _controller1.dispose();
    _controller2.dispose();
    _controller3.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: SizedBox(
        width: 300,
        height: 400,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Step 1: Spinner appears
            AnimatedBuilder(
              animation: _animation1,
              builder: (context, child) {
                return Transform.rotate(
                  angle: _animation1.value * 2 * 3.14159,
                  child: Opacity(
                    opacity: _animation1.value,
                    child: SizedBox(
                      width: 50,
                      height: 50,
                      child: CircularProgressIndicator(
                        value: _animation1.value,
                      ),
                    ),
                  ),
                );
              },
            ),
            const SizedBox(height: 20),
            // Step 2: Checkmark appears
            AnimatedBuilder(
              animation: _animation2,
              builder: (context, child) {
                return Opacity(
                  opacity: _animation2.value,
                  child: Icon(
                    Icons.check_circle,
                    size: 50 + _animation2.value * 10,
                    color: Colors.green,
                  ),
                );
              },
            ),
            const SizedBox(height: 20),
            // Step 3: Text appears
            AnimatedBuilder(
              animation: _animation3,
              builder: (context, child) {
                return Opacity(
                  opacity: _animation3.value,
                  child: Transform.translate(
                    offset: Offset(0, (1 - _animation3.value) * 20),
                    child: const Text('Complete!'),
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
```

---

## Practical Example: Staggered List Animation [DONE]

Multiple items animate with delay:

```dart
class StaggeredListAnimation extends StatefulWidget {
  final List<String> items;
  const StaggeredListAnimation({Key? key, required this.items}) : super(key: key);

  @override
  State<StaggeredListAnimation> createState() => _StaggeredListAnimationState();
}

class _StaggeredListAnimationState extends State<StaggeredListAnimation>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late List<Animation<Offset>> _itemAnimations;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: Duration(seconds: widget.items.length ~/ 2 + 1),
      vsync: this,
    );

    // Each item starts at different time but uses same easing
    _itemAnimations = List.generate(widget.items.length, (index) {
      final start = (index * 0.1).clamp(0.0, 1.0);
      final end = (start + 0.3).clamp(0.0, 1.0);
      
      return Tween<Offset>(
        begin: const Offset(100, 0),  // Slide in from right
        end: Offset.zero,
      ).animate(
        CurvedAnimation(
          parent: _controller,
          curve: Interval(start, end, curve: Curves.easeOut),
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
      itemCount: widget.items.length,
      itemBuilder: (context, index) {
        return AnimatedBuilder(
          animation: _itemAnimations[index],
          builder: (context, child) {
            return Transform.translate(
              offset: _itemAnimations[index].value,
              child: Opacity(
                opacity: 1 - (_itemAnimations[index].value.dx / 100).abs(),
                child: ListTile(
                  title: Text(widget.items[index]),
                ),
              ),
            );
          },
        );
      },
    );
  }
}
```

---

## Practical Example: Multi-Stage Choreography []

Using Interval for a complex dance:

```dart
class ChoreographedAnimation extends StatefulWidget {
  const ChoreographedAnimation({Key? key}) : super(key: key);

  @override
  State<ChoreographedAnimation> createState() => _ChoreographedAnimationState();
}

class _ChoreographedAnimationState extends State<ChoreographedAnimation>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;
  late Animation<double> _scaleAnimation;
  late Animation<double> _rotateAnimation;
  late Animation<Color?> _colorAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 4),
      vsync: this,
    );

    // Slide in for first 25%
    _slideAnimation = Tween<Offset>(
      begin: const Offset(-100, 0),
      end: Offset.zero,
    ).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.0, 0.25, curve: Curves.easeOut),
      ),
    );

    // Scale up during 25%-50%
    _scaleAnimation = Tween<double>(begin: 0.5, end: 1.0).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.25, 0.5, curve: Curves.elasticOut),
      ),
    );

    // Rotate during 50%-75%
    _rotateAnimation = Tween<double>(begin: 0, end: 2 * 3.14159).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.5, 0.75, curve: Curves.easeInOut),
      ),
    );

    // Change color during 75%-100%
    _colorAnimation = ColorTween(begin: Colors.red, end: Colors.green).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.75, 1.0, curve: Curves.easeOut),
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
    return Center(
      child: AnimatedBuilder(
        animation: Listenable.merge([
          _slideAnimation,
          _scaleAnimation,
          _rotateAnimation,
          _colorAnimation,
        ]),
        builder: (context, child) {
          return Transform.translate(
            offset: _slideAnimation.value,
            child: Transform.scale(
              scale: _scaleAnimation.value,
              child: Transform.rotate(
                angle: _rotateAnimation.value,
                child: Container(
                  width: 100,
                  height: 100,
                  color: _colorAnimation.value,
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}
```

---

## Exercises

### Exercise 1: Page Load Sequence
Create a page that:
- Hero image scales in
- Title fades in after image
- Content slides up after title
- Button pulses after content
- All chained sequentially

### Exercise 2: Staggered Grid
Animate a grid of items:
- All items appear with stagger effect
- Hint: Create unique animations for each grid cell

### Exercise 3: Complex Choreography
Combine all techniques:
- Multiple animations
- Interval timing
- Sequential triggers
- Status listeners

### Exercise 4: Reverse Sequence
Create animation that:
- Plays forward (as above)
- On tap, reverses in opposite order
- Hint: Implement reverse() for controllers

---

## Performance Tips

- **Use Interval over Multiple Controllers**: More efficient
- **Merge Listeners When Possible**: Fewer rebuilds
- **Avoid Deep Nesting**: Flatten your animation tree
- **Profile Your Work**: DevTools → Performance tab

---

## Key Takeaways

1. **Interval = One Controller, Multiple Timings**: Efficient and clean
2. **Sequential = Chain Status Listeners**: Simple but limited
3. **Staggered = Offset Start Times**: Creates cascade effects
4. **Choreography = Combine All Techniques**: Professional results
5. **Always Profile**: Animation complexity grows fast

---

## Next Steps

- Complete all exercises
- Practice these patterns frequently
- Module 07 will show how to wrap these patterns into reusable widgets

---

## Resources

- [Interval](https://api.flutter.dev/flutter/animation/Interval-class.html)
- [AnimationStatus](https://api.flutter.dev/flutter/animation/AnimationStatus.html)
