# Module 01: Animation Fundamentals & AnimationController [DONE]

## Overview

Before you can build animations, you need to understand how they work. This module covers the foundational concepts: what an animation *is*, how Flutter manages it, and how `AnimationController` drives everything.

**Duration:** 3–4 hours  
**Prerequisites:** Basic Dart, StatefulWidget knowledge  
**Learning Outcomes:**
- Understand animation lifecycle and state
- Build custom animations using AnimationController
- Recognize listener patterns and how animations notify changes
- Understand forward(), reverse(), and repeat()

---

## Key Concepts

### 1. What is an Animation in Flutter? [DONE]

In Flutter, an **Animation** is an object that changes a value over time. It's not the visual change itself—it's the *data* driving the visual change.

```
AnimationController (the engine)
        ↓
    Animation (the value stream)
        ↓
  Widget Tree (uses the values to rebuild)
```

**Animation ≠ Visual Change**
An Animation is a *value* (like 0.0 → 1.0). The widget decides what to do with that value.

### 2. AnimationController: The Engine [DONE]

`AnimationController` is a special `Animation` that you manually drive.

```dart
late AnimationController _controller;

@override
void initState() {
  super.initState();
  _controller = AnimationController(
    duration: Duration(seconds: 2),
    vsync: this,  // Sync with screen refresh rate
  );
}

@override
void dispose() {
  _controller.dispose();  // Always cleanup!
  super.dispose();
}
```

**Key points:**
- `duration`: How long for 0.0 → 1.0
- `vsync`: Tick provider (usually `this` if StatefulWidget is TickerProvider)
- `dispose()`: Critical—animations hold resources

### 3. Driving an Animation [DONE]

```dart
_controller.forward();   // 0.0 → 1.0
_controller.reverse();   // 1.0 → 0.0
_controller.repeat();    // Loop forever
_controller.repeat(reverse: true);  // Bounce forever
```

### 4. Listening to Changes [DONE]

Animations are streams of values. You listen in two ways:

**Listener (rebuilds widget):**
```dart
_controller.addListener(() {
  setState(() {}); // Trigger rebuild on every value change
});
```

**Status Listener (animation phase changes):**
```dart
_controller.addStatusListener((status) {
  if (status == AnimationStatus.completed) {
    print('Animation finished');
  }
});
```

### 5. Animation Value [DONE]

Access the current value anytime:
```dart
print(_controller.value);  // 0.0 to 1.0
```

---

## Practical Example: Simple Opacity Animation [DONE]

```dart
class FadeInWidget extends StatefulWidget {
  const FadeInWidget({Key? key}) : super(key: key);

  @override
  State<FadeInWidget> createState() => _FadeInWidgetState();
}

class _FadeInWidgetState extends State<FadeInWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 1),
      vsync: this,
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
        // Rebuild on every animation value change
        AnimatedBuilder(
          animation: _controller,
          builder: (context, child) {
            return Opacity(
              opacity: _controller.value,
              child: child,
            );
          },
          child: const Text('Hello'),
        ),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => _controller.forward(),
          child: const Text('Fade In'),
        ),
      ],
    );
  }
}
```

**What happens:**
1. You press the button → `_controller.forward()` starts
2. AnimationController ticks every frame (60/120 fps)
3. `_controller.value` changes from 0.0 → 1.0
4. AnimatedBuilder rebuilds on each tick
5. Opacity follows the value

---

## Mental Model: Ticking & Syncing [DONE]

When you call `forward()`, the controller **synchronizes with the display refresh rate** (via `vsync`):

```
Frame 1: value=0.00,  opacity=0.00
Frame 2: value=0.02,  opacity=0.02
Frame 3: value=0.04,  opacity=0.04
...
Frame 60: value=1.00, opacity=1.00 → Complete
```

**Why vsync matters:**
- Without it, animations can tear or stutter
- `vsync: this` tells Flutter to sync with your widget's ticker
- For StatefulWidget, use `SingleTickerProviderStateMixin`
- For multiple animations, use `TickerProviderStateMixin`

---

## The Lifecycle [DONE]

```
initState()
    ↓
AnimationController created
    ↓
forward() / reverse() / repeat() called
    ↓
AnimationStatus.forward (during animation)
    ↓
AnimationStatus.completed (at end)
    ↓
dispose() called → cleanup resources
```

---

## Common Mistakes [DONE]

❌ **Forgetting to dispose:**
```dart
// WRONG - memory leak
_controller = AnimationController(vsync: this);
// Never calls dispose()
```

✅ **Always dispose:**
```dart
// RIGHT
@override
void dispose() {
  _controller.dispose();
  super.dispose();
}
```

---

❌ **Using `addListener()` without rebuilding:**
```dart
// WRONG - listener doesn't trigger rebuild
_controller.addListener(() {
  // This doesn't call setState!
  print(_controller.value);
});
```

✅ **Use AnimatedBuilder or setState:**
```dart
// RIGHT - with AnimatedBuilder
AnimatedBuilder(
  animation: _controller,
  builder: (context, child) => Container(),
);

// Or setState
_controller.addListener(() {
  setState(() {});
});
```

---

## Exercises [DONE]

### Exercise 1: Spin Animation [DONE]
Create a widget with a rotating icon that:
- Starts spinning when a button is pressed
- Spins 3 times then stops
- Hint: `_controller.repeat()` with a stop condition

### Exercise 2: Pulse Animation [DONE]
Create a pulse effect:
- Scale from 1.0 → 1.2 → 1.0
- Repeats forever
- Hint: Use `forward()` + status listener to reverse

### Exercise 3: Multi-Controller Widget [DONE]
Create a widget with:
- Fade animation (one controller)
- Slide animation (another controller)
- Control them independently with buttons
- Hint: Use `TickerProviderStateMixin` for 2 controllers

---

## Key Takeaways

1. **Animation = Value Stream**: An Animation is data, not a visual change
2. **AnimationController is the Engine**: It drives the value from 0.0 → 1.0
3. **vsync Prevents Jank**: Always provide a TickerProvider
4. **Always Dispose**: Animations hold resources that must be cleaned up
5. **Listeners Trigger Rebuilds**: Use `addListener()` with `setState()` or `AnimatedBuilder`

---

## Next Steps

- Complete all exercises before moving to Module 02
- Experiment with different durations and repeat counts
- Understand that you're always mapping 0.0–1.0 to meaningful values
- You now know the foundation—Tweens will build on this

---

## Resources

- [Flutter Animation Classes](https://api.flutter.dev/flutter/animation/Animation-class.html)
- [AnimationController](https://api.flutter.dev/flutter/animation/AnimationController-class.html)
- [TickerProvider](https://api.flutter.dev/flutter/scheduler/TickerProvider-class.html)
