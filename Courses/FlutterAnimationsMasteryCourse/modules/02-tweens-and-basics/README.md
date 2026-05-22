# Module 02: Tweens & Basic Animations

## Overview

Now that you understand `AnimationController`, you need to know how to transform its 0.0–1.0 output into meaningful values: colors, sizes, offsets, and more. That's what **Tweens** do.

**Duration:** 2–3 hours  
**Prerequisites:** Module 01  
**Learning Outcomes:**
- Understand the Tween system and value interpolation
- Use Tween chaining with controllers
- Know when to use different Tween types
- Build practical animations with tweens

---

## Key Concepts

### 1. What is a Tween? [DONE]

A **Tween** (short for "in-between") maps the controller's 0.0–1.0 to your desired range.

```
Controller:  0.0 ────────────────────► 1.0
             ↓
Tween:       100 (begin) ────► 200 (end)
             ↓
Value:       100 ────────────────────► 200
```

### 2. Common Tween Types [DONE]

```dart
// Double (size, opacity, offset)
Tween<double>(begin: 0, end: 100)

// Color (color transitions)
// ColorTween very interesring.
ColorTween(begin: Colors.red, end: Colors.blue)

// Offset (position)
Tween<Offset>(begin: Offset(0, 0), end: Offset(100, 50))

// Rect (bounds)
RectTween(begin: Rect.fromLTWH(...), end: Rect.fromLTWH(...))

// Integer (count animations)
IntTween(begin: 0, end: 100)
```

### 3. Creating a Tween Animation [DONE]

```dart
late AnimationController _controller;
late Animation<double> _sizeAnimation;

@override
void initState() {
  super.initState();
  _controller = AnimationController(
    duration: const Duration(seconds: 1),
    vsync: this,
  );
  
  // Create tween from 50 to 200
  _sizeAnimation = Tween<double>(begin: 50, end: 200)
    .animate(_controller);
}
```

Now `_sizeAnimation.value` ranges from 50 → 200 as controller goes 0.0 → 1.0.

### 4. Using Tweens in Your UI [DONE]

```dart
AnimatedBuilder(
  animation: _sizeAnimation,
  builder: (context, child) {
    return Container(
      width: _sizeAnimation.value,
      height: _sizeAnimation.value,
      color: Colors.blue,
    );
  },
);
```

---

## Practical Example: Color Transition [DONE]

```dart
class ColorTweenExample extends StatefulWidget {
  const ColorTweenExample({Key? key}) : super(key: key);

  @override
  State<ColorTweenExample> createState() => _ColorTweenExampleState();
}

class _ColorTweenExampleState extends State<ColorTweenExample>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Color?> _colorAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    );

    _colorAnimation = ColorTween(
      begin: Colors.red,
      end: Colors.blue,
    ).animate(_controller);
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
          animation: _colorAnimation,
          builder: (context, child) {
            return Container(
              width: 100,
              height: 100,
              color: _colorAnimation.value,
            );
          },
        ),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => _controller.forward(),
          child: const Text('Change Color'),
        ),
      ],
    );
  }
}
```

---

## Tween Chaining [DONE]

You can chain tweens together:

```dart
_animation = Tween<double>(begin: 0, end: 100)
  .chain(CurveTween(curve: Curves.easeInOut))
  .animate(_controller);
```

This applies an easing curve *before* interpolation.

---

## Practical Example: Size & Position [DONE]

```dart
class ComplexTweenExample extends StatefulWidget {
  const ComplexTweenExample({Key? key}) : super(key: key);

  @override
  State<ComplexTweenExample> createState() => _ComplexTweenExampleState();
}

class _ComplexTweenExampleState extends State<ComplexTweenExample>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _sizeAnimation;
  late Animation<Offset> _offsetAnimation;
  late Animation<Color?> _colorAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    );

    // Multiple tweens, one controller
    _sizeAnimation = Tween<double>(begin: 50, end: 150)
      .animate(_controller);
    
    _offsetAnimation = Tween<Offset>(
      begin: const Offset(0, 0),
      end: const Offset(100, 50),
    ).animate(_controller);
    
    _colorAnimation = ColorTween(
      begin: Colors.purple,
      end: Colors.orange,
    ).animate(_controller);
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
            _sizeAnimation,
            _offsetAnimation,
            _colorAnimation,
          ]),
          builder: (context, child) {
            return Transform.translate(
              offset: _offsetAnimation.value,
              child: Container(
                width: _sizeAnimation.value,
                height: _sizeAnimation.value,
                color: _colorAnimation.value,
              ),
            );
          },
        ),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => _controller.forward(),
          child: const Text('Animate'),
        ),
      ],
    );
  }
}
```

**Note:** Use `Listenable.merge()` when listening to multiple animations.

---

## Custom Tween [DONE]

You can create tweens for custom types:

```dart
class AlignmentTween extends Tween<Alignment> {
  AlignmentTween({
    required Alignment begin,
    required Alignment end,
  }) : super(begin: begin, end: end);

  @override
  Alignment lerp(double t) {
    return Alignment(
      begin!.x + (end!.x - begin!.x) * t,
      begin!.y + (end!.y - begin!.y) * t,
    );
  }
}
```

The key method is `lerp()` (linear interpolation). For value `t` (0.0–1.0), compute the interpolated value.

---

## Common Mistakes [DONE]

❌ **Creating multiple tweens without merging listeners:**
```dart
// WRONG - only listens to first animation
AnimatedBuilder(
  animation: _sizeAnimation,
  builder: (context, child) {
    // This won't rebuild when _colorAnimation changes!
    return Container(
      width: _sizeAnimation.value,
      color: _colorAnimation.value,
    );
  },
);
```

✅ **Use Listenable.merge():**
```dart
// RIGHT
AnimatedBuilder(
  animation: Listenable.merge([
    _sizeAnimation,
    _colorAnimation,
  ]),
  builder: (context, child) {
    return Container(
      width: _sizeAnimation.value,
      color: _colorAnimation.value,
    );
  },
);
```

---

❌ **Forgetting `.animate(_controller)`:**
```dart
// WRONG - this is just a Tween, not animated
var tween = Tween<double>(begin: 0, end: 100);
// tween.value doesn't exist!
```

✅ **Always animate:**
```dart
// RIGHT
var animation = Tween<double>(begin: 0, end: 100)
  .animate(_controller);
// animation.value works
```

---

## Exercises

### Exercise 1: Gradient Animation [DONE]
Create a container that animates through multiple colors in sequence:
- Red → Yellow → Green
- Hint: Use two controllers and trigger them sequentially

### Exercise 2: Bouncing Ball [DONE]
Animate a ball that:
- Falls down (size stays same)
- Bounces when it hits bottom
- Moves back up
- Repeats
- Hint: Use Offset tween with custom positioning

### Exercise 3: Growing Text [DONE]
Create a text widget that:
- Starts at size 12
- Grows to size 48
- Changes color while growing
- Hint: Combine TextStyle with double tween

### Exercise 4: Custom Tween [DONE]
Create a `BorderRadiusTween`:
- Animates from circle (50) to square (0) border radius
- Hint: Implement custom `lerp()` method

---

## Key Takeaways

1. **Tweens Map 0.0–1.0 to Your Values**: They bridge controller and UI
2. **Chaining Tweens**: Use `.chain()` for more complex transformations
3. **Merge Listeners**: Use `Listenable.merge()` for multiple animations
4. **Custom Tweens**: Implement `lerp()` for any type
5. **Animation Objects are Cheap**: Create them in `initState()`, not `build()`

---

## Next Steps

- Complete all exercises
- Experiment with different Tween types
- Understand that `lerp()` is the core—everything builds on interpolation
- Ready for Module 03: Curves will add easing and visual polish

---

## Resources

- [Tween API](https://api.flutter.dev/flutter/animation/Tween-class.html)
- [ColorTween](https://api.flutter.dev/flutter/animation/ColorTween-class.html)
- [Listenable.merge](https://api.flutter.dev/flutter/foundation/Listenable/merge.html)
