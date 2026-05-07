# Module 03: Curves & Timing Mastery

## Overview

A linear animation (0 → 1 at constant speed) feels mechanical and unnatural. **Curves** add easing and life. This module teaches you to choose curves, understand easing, and create polish.

**Duration:** 2–3 hours  
**Prerequisites:** Modules 01–02  
**Learning Outcomes:**
- Understand easing and why it matters
- Use Flutter's built-in curves effectively
- Create custom easing curves
- Master timing and duration management

---

## Key Concepts

### 1. What is a Curve?

A **Curve** transforms the controller's linear progress (0.0–1.0) into a different progression.

```
Linear:   ████████████ (straight line, feels robotic)
EaseIn:   ▁▂▃▄▅▆███████ (slow then fast, feels heavy)
EaseOut:  ██████▆▅▄▃▂▁ (fast then slow, feels bouncy)
Bounce:   █▆▇█▅▇█▄▆ (oscillates, feels playful)
```

### 2. Common Built-in Curves

```dart
// Linear (no easing)
Curves.linear

// Ease in/out
Curves.easeIn      // Slow start, fast end
Curves.easeOut     // Fast start, slow end
Curves.easeInOut   // Slow start, slow end, fast middle

// Cubic variations
Curves.easeInCubic
Curves.easeOutCubic
Curves.easeInOutCubic

// Back (overshoots slightly)
Curves.easeInBack
Curves.easeOutBack

// Elastic (oscillates)
Curves.easeInElastic
Curves.easeOutElastic

// Bounce (bounces at end)
Curves.bounceIn
Curves.bounceOut
Curves.bounceInOut

// Custom
Curves.elasticInOut  // Springy, popular for alerts
Curves.fastOutSlowIn // Material's default
```

### 3. Applying Curves to Animations

Use `CurvedAnimation`:

```dart
_animation = Tween<double>(begin: 0, end: 100)
  .animate(
    CurvedAnimation(
      parent: _controller,
      curve: Curves.easeOutQuad,
    ),
  );
```

The curve transforms the controller's progress before the tween interpolates.

### 4. Interval: Timing Subsets

Run an animation only during part of the total duration:

```dart
// This animation only runs from 50% to 80% of total duration
_animation = Tween<double>(begin: 0, end: 100)
  .animate(
    CurvedAnimation(
      parent: _controller,
      curve: const Interval(0.5, 0.8, curve: Curves.easeOut),
    ),
  );
```

Perfect for staggering multi-element animations.

---

## Practical Example: Comparing Curves Visually

```dart
class CurveComparison extends StatefulWidget {
  const CurveComparison({Key? key}) : super(key: key);

  @override
  State<CurveComparison> createState() => _CurveComparisonState();
}

class _CurveComparisonState extends State<CurveComparison>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _linearAnimation;
  late Animation<Offset> _easeInAnimation;
  late Animation<Offset> _easeOutAnimation;
  late Animation<Offset> _bounceAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    );

    // Same tween, different curves
    const tween = Tween<Offset>(
      begin: Offset(0, 0),
      end: const Offset(200, 0),
    );

    _linearAnimation = tween.animate(_controller);
    
    _easeInAnimation = tween.animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeIn),
    );
    
    _easeOutAnimation = tween.animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeOut),
    );
    
    _bounceAnimation = tween.animate(
      CurvedAnimation(parent: _controller, curve: Curves.bounceOut),
    );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        _buildAnimatedBox('Linear', _linearAnimation),
        _buildAnimatedBox('Ease In', _easeInAnimation),
        _buildAnimatedBox('Ease Out', _easeOutAnimation),
        _buildAnimatedBox('Bounce', _bounceAnimation),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => _controller.forward(from: 0),
          child: const Text('Animate'),
        ),
      ],
    );
  }

  Widget _buildAnimatedBox(String label, Animation<Offset> animation) {
    return AnimatedBuilder(
      animation: animation,
      builder: (context, child) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(label, style: const TextStyle(fontSize: 12)),
            Transform.translate(
              offset: animation.value,
              child: Container(
                width: 50,
                height: 50,
                color: Colors.blue,
              ),
            ),
            const SizedBox(height: 10),
          ],
        );
      },
    );
  }
}
```

Run this to see how different curves change motion *quality*.

---

## Understanding Easing Psychologically

Different curves create different *feelings*:

| Curve | Feeling | Use Case |
|-------|---------|----------|
| `linear` | Robotic, mechanical | Progress bars, loading spinners |
| `easeOut` | Snappy, responsive | Button taps, opens, appears |
| `easeIn` | Heavy, settling | Closes, dismisses, hides |
| `easeInOut` | Balanced | General purpose, transitions |
| `bounceOut` | Playful, fun | Alerts, achievements, playful apps |
| `elasticOut` | Springy, bouncy | Emphasis, attention-seeking |

**Material Design Principle:**
- Elements entering should use `easeOut` (fast)
- Elements exiting should use `easeIn` (graceful)

---

## Custom Curves

Create your own easing function:

```dart
class CustomCurve extends Curve {
  @override
  double transformInternal(double t) {
    // t ranges from 0.0 to 1.0
    // Return a new value (can be <0 or >1)
    return t * t;  // Quadratic easing in
  }
}

// Use it:
CurvedAnimation(
  parent: _controller,
  curve: CustomCurve(),
)
```

---

## Interval: Staggering Example

```dart
class StaggeredAnimation extends StatefulWidget {
  const StaggeredAnimation({Key? key}) : super(key: key);

  @override
  State<StaggeredAnimation> createState() => _StaggeredAnimationState();
}

class _StaggeredAnimationState extends State<StaggeredAnimation>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _box1Animation;
  late Animation<Offset> _box2Animation;
  late Animation<Offset> _box3Animation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 3),
      vsync: this,
    );

    // Box 1: slides from 0% to 50%
    _box1Animation = Tween<Offset>(
      begin: const Offset(0, 0),
      end: const Offset(200, 0),
    ).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.0, 0.33, curve: Curves.easeOut),
      ),
    );

    // Box 2: slides from 33% to 66%
    _box2Animation = Tween<Offset>(
      begin: const Offset(0, 0),
      end: const Offset(200, 0),
    ).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.33, 0.66, curve: Curves.easeOut),
      ),
    );

    // Box 3: slides from 66% to 100%
    _box3Animation = Tween<Offset>(
      begin: const Offset(0, 0),
      end: const Offset(200, 0),
    ).animate(
      CurvedAnimation(
        parent: _controller,
        curve: const Interval(0.66, 1.0, curve: Curves.easeOut),
      ),
    );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        AnimatedBuilder(
          animation: Listenable.merge([
            _box1Animation,
            _box2Animation,
            _box3Animation,
          ]),
          builder: (context, child) {
            return Column(
              children: [
                Transform.translate(
                  offset: _box1Animation.value,
                  child: Container(width: 50, height: 50, color: Colors.red),
                ),
                Transform.translate(
                  offset: _box2Animation.value,
                  child: Container(width: 50, height: 50, color: Colors.green),
                ),
                Transform.translate(
                  offset: _box3Animation.value,
                  child: Container(width: 50, height: 50, color: Colors.blue),
                ),
              ],
            );
          },
        ),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => _controller.forward(from: 0),
          child: const Text('Stagger'),
        ),
      ],
    );
  }
}
```

Three boxes slide in sequence, creating a cascading effect.

---

## Exercises

### Exercise 1: Bouncy Button
Create a button that bounces when tapped:
- Scale from 1.0 → 1.1 → 1.0
- Use `bounceOut` curve
- Hint: Use repeat() or reverse() after forward()

### Exercise 2: Page Transition
Animate a page entrance:
- Opacity: 0 → 1 (0% to 50%)
- Scale: 0.8 → 1.0 (0% to 50%)
- SlideUp: 50px → 0 (0% to 100%)
- Hint: Use multiple tweens with different intervals

### Exercise 3: Loading Spinner
Create a spinning loader with ease:
- Rotates 360° every 2 seconds
- Use `linear` curve
- Add scale pulse using nested controller
- Hint: Use Transform.rotate with angle calculation

### Exercise 4: Custom Elastic Curve
Implement a custom curve that overshoots (goes past target then bounces back):
- Design the math to overshoot by 20%
- Use in a slide animation
- Hint: Return values > 1.0 at certain points

---

## Duration & Timing Best Practices

| Animation Type | Duration | Curve |
|---|---|---|
| Micro (button tap) | 150–200ms | easeOut |
| Appear/Disappear | 300–500ms | easeOut/easeIn |
| Page transition | 300–500ms | easeInOut |
| Entrance animation | 500–800ms | easeOut |
| Complex choreography | 1500–3000ms | varies |

**Rule of Thumb:**
- Fast animations feel responsive
- Slow animations feel cinematic
- 300–500ms is the sweet spot for most UI animations

---

## Key Takeaways

1. **Curves Transform Time**: They make animations feel alive
2. **Choose Curves Intentionally**: Different curves communicate different meanings
3. **Intervals Enable Staggering**: Run animations at different times within one timeline
4. **Material Design Rules**: easeOut for enters, easeIn for exits
5. **Custom Curves**: You can create any easing function you need

---

## Next Steps

- Complete all exercises
- Play with different curves to develop intuition
- You now understand timing—ready for Module 04 (Animated Widgets)

---

## Resources

- [Curves API](https://api.flutter.dev/flutter/animation/Curves-class.html)
- [CurvedAnimation](https://api.flutter.dev/flutter/animation/CurvedAnimation-class.html)
- [Easing Functions Cheat Sheet](https://easings.net/)
