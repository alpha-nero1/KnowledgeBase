# Module 04: Animated Widgets

## Overview

For simple animations, creating `AnimationController`, `Tween`, and `AnimatedBuilder` is tedious. Flutter provides **Animated widgets** that handle this internally—you just declare start/end values and they animate.

**Duration:** 2–3 hours  
**Prerequisites:** Modules 01–03  
**Learning Outcomes:**
- Use AnimatedOpacity, AnimatedScale, AnimatedPositioned, etc.
- Understand when implicit animations are appropriate
- Recognize performance differences
- Know the tradeoffs vs. explicit animations

---

## Key Concepts

### 1. What are Animated Widgets?

Animated widgets wrap a standard widget and automatically animate changes to their properties.

```dart
// Standard widget (no animation)
Opacity(opacity: 0.5, child: Text('Hi'))

// Animated version (changes are animated)
AnimatedOpacity(
  opacity: 0.5,
  duration: Duration(seconds: 1),
  child: Text('Hi'),
)
```

When you change `opacity: 0.5 → 0.9`, it smoothly animates instead of jumping.

### 2. Common Animated Widgets

| Widget | Animates |
|--------|----------|
| `AnimatedOpacity` | opacity |
| `AnimatedScale` | scale transform |
| `AnimatedRotation` | rotation angle |
| `AnimatedPositioned` | position (in Stack) |
| `AnimatedAlign` | alignment |
| `AnimatedContainer` | size, color, border, shadow, etc. |
| `AnimatedCrossFade` | cross-fade between two widgets |
| `AnimatedSwitcher` | animated widget replacement |
| `AnimatedDefaultTextStyle` | text style (color, size, weight) |
| `AnimatedPadding` | padding |
| `AnimatedSlide` | slide position |

### 3. Usage Pattern [DONE]

```dart
class MyWidget extends StatefulWidget {
  const MyWidget({Key? key}) : super(key: key);

  @override
  State<MyWidget> createState() => _MyWidgetState();
}

class _MyWidgetState extends State<MyWidget> {
  double _opacity = 1.0;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // When _opacity changes, this animates automatically
        AnimatedOpacity(
          opacity: _opacity,
          duration: const Duration(seconds: 1),
          child: const Text('Hello'),
        ),
        ElevatedButton(
          onPressed: () => setState(() => _opacity = _opacity == 1.0 ? 0.0 : 1.0),
          child: const Text('Toggle'),
        ),
      ],
    );
  }
}
```

That's it. No controller, no tween, no listener. The widget handles it internally.

---

## Practical Example: AnimatedContainer [DONE]

The most powerful implicit widget.

```dart
class AnimatedContainerExample extends StatefulWidget {
  const AnimatedContainerExample({Key? key}) : super(key: key);

  @override
  State<AnimatedContainerExample> createState() => _AnimatedContainerExampleState();
}

class _AnimatedContainerExampleState extends State<AnimatedContainerExample> {
  bool _expanded = false;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          // All of these animate when state changes
          AnimatedContainer(
            width: _expanded ? 200 : 100,
            height: _expanded ? 200 : 100,
            color: _expanded ? Colors.blue : Colors.red,
            margin: EdgeInsets.all(_expanded ? 20 : 0),
            padding: EdgeInsets.all(_expanded ? 16 : 8),
            duration: const Duration(seconds: 1),
            curve: Curves.easeInOut,
            child: Center(
              child: Text(
                _expanded ? 'Expanded' : 'Collapsed',
                style: const TextStyle(color: Colors.white),
              ),
            ),
          ),
          const SizedBox(height: 20),
          ElevatedButton(
            onPressed: () => setState(() => _expanded = !_expanded),
            child: const Text('Toggle'),
          ),
        ],
      ),
    );
  }
}
```

### What Animated Automatically:
- width / height
- color
- margin / padding
- border
- shadow
- decoration changes
- alignment

---

## Practical Example: AnimatedPositioned

Animate position inside a Stack:

```dart
class AnimatedPositionedExample extends StatefulWidget {
  const AnimatedPositionedExample({Key? key}) : super(key: key);

  @override
  State<AnimatedPositionedExample> createState() => _AnimatedPositionedExampleState();
}

class _AnimatedPositionedExampleState extends State<AnimatedPositionedExample> {
  bool _topLeft = true;

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        // Animates position when state changes
        AnimatedPositioned(
          left: _topLeft ? 10 : 200,
          top: _topLeft ? 10 : 300,
          duration: const Duration(seconds: 1),
          curve: Curves.easeInOut,
          child: Container(
            width: 50,
            height: 50,
            color: Colors.blue,
          ),
        ),
        Positioned(
          bottom: 20,
          left: 20,
          child: ElevatedButton(
            onPressed: () => setState(() => _topLeft = !_topLeft),
            child: const Text('Move'),
          ),
        ),
      ],
    );
  }
}
```

---

## Practical Example: AnimatedCrossFade [DONE]

Smoothly swap between two widgets:

```dart
class AnimatedCrossFadeExample extends StatefulWidget {
  const AnimatedCrossFadeExample({Key? key}) : super(key: key);

  @override
  State<AnimatedCrossFadeExample> createState() => _AnimatedCrossFadeExampleState();
}

class _AnimatedCrossFadeExampleState extends State<AnimatedCrossFadeExample> {
  bool _showFirst = true;

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        AnimatedCrossFade(
          firstChild: Container(
            width: 200,
            height: 200,
            color: Colors.red,
            child: const Center(child: Text('First')),
          ),
          secondChild: Container(
            width: 200,
            height: 200,
            color: Colors.blue,
            child: const Center(child: Text('Second')),
          ),
          crossFadeState: _showFirst ? CrossFadeState.showFirst : CrossFadeState.showSecond,
          duration: const Duration(seconds: 1),
        ),
        const SizedBox(height: 20),
        ElevatedButton(
          onPressed: () => setState(() => _showFirst = !_showFirst),
          child: const Text('Swap'),
        ),
      ],
    );
  }
}
```

---

## Implicit vs. Explicit: When to Use Each

| Use Implicit (Animated*) | Use Explicit (Controller + Tween) |
|---|---|
| Simple state changes (on/off) | Complex choreography |
| One animation at a time | Multiple concurrent animations |
| No need to programmatically control | Need to pause/resume/reverse |
| Easier to read and maintain | Fine-grained control needed |
| Better for simple UI transitions | Gesture-responsive animations |

**Implicit is easier; Explicit is more powerful.**

---

## Common Mistakes

❌ **Overusing AnimatedContainer:**
```dart
// WRONG - rebuilds entire widget tree
AnimatedContainer(
  width: _width,
  duration: Duration(seconds: 1),
  child: ExpensiveWidget(),  // Rebuilds every frame!
)
```

✅ **Use const for expensive children:**
```dart
// RIGHT - child doesn't rebuild
AnimatedContainer(
  width: _width,
  duration: Duration(seconds: 1),
  child: const ExpensiveWidget(),
)
```

---

❌ **Forgetting curve parameter:**
```dart
// WRONG - linear, feels mechanical
AnimatedOpacity(
  opacity: _opacity,
  duration: Duration(seconds: 1),
  child: child,
)
```

✅ **Always add curves:**
```dart
// RIGHT
AnimatedOpacity(
  opacity: _opacity,
  duration: Duration(seconds: 1),
  curve: Curves.easeInOut,
  child: child,
)
```

---

## Exercises

### Exercise 1: Expandable Drawer [DONE]
Create a sidebar that:
- Slides in/out when button tapped
- Uses AnimatedPositioned
- Changes color while expanding
- Hint: Use AnimatedContainer for color, AnimatedPositioned for slide

### Exercise 2: Image Gallery [DONE]
Create a gallery that:
- Shows one image at a time
- CrossFade between images on tap
- Hint: Use AnimatedCrossFade with list of images

### Exercise 3: Loading Spinner [DONE]
Animate a loading state with:
- Rotating spinner (AnimatedRotation)
- Pulsing background (AnimatedScale)
- Fading text below (AnimatedOpacity)
- Hint: Combine multiple animated widgets

### Exercise 4: Theme Toggle [DONE]
Create a theme switcher that:
- Animates background color (AnimatedContainer)
- Animates text color (AnimatedDefaultTextStyle)
- Smooth transitions between light/dark
- Hint: Use setState to change theme colors

---

## Performance Considerations

**Implicit Animations:**
- ✅ Generally performant (optimized internally)
- ⚠️ Can cause rebuilds if not careful
- ⚠️ Less control over rebuild frequency

**Explicit Animations:**
- ✅ Fine-grained control over rebuilds
- ⚠️ More code
- ✅ Better for high-frequency updates

**Rule:**
- Use implicit for simple state changes
- Use explicit for complex choreography or performance-critical cases

---

## Key Takeaways

1. **Implicit Animations are Simpler**: Just change state, animation happens
2. **Common Animated Widgets**: Learn the main ones (Container, Opacity, Positioned)
3. **Curves Still Matter**: Even implicit animations should have good easing
4. **Know the Limits**: When you need more control, explicit animations are there
5. **Const Your Children**: Avoid unnecessary rebuilds - good point!

---

## Next Steps

- Complete all exercises
- Mix implicit with explicit (both can be in same widget)
- Module 05 will clarify implicit vs. explicit more deeply

---

## Resources

- [Animated Widgets](https://api.flutter.dev/flutter/widgets/AnimatedOpacity-class.html)
- [AnimatedContainer](https://api.flutter.dev/flutter/widgets/AnimatedContainer-class.html)
