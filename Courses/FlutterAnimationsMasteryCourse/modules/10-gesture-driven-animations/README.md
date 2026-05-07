# Module 10: Gesture-Driven & Interactive Animations

## Overview

Apps require response to user input: swipes, drags, pinches. This module teaches real-time animation control via gestures—the magic behind interactive, tactile interfaces.

**Duration:** 4–5 hours  
**Prerequisites:** Modules 01–09  
**Learning Outcomes:**
- Respond to pan, drag, swipe, pinch gestures
- Control animations in real-time via gesture data
- Calculate velocity from gesture deltas
- Build interactive, responsive UIs

---

## Key Concepts

### 1. Gesture Data

```dart
GestureDetector(
  onPanUpdate: (details) {
    // details.delta: change since last frame (pixels)
    // details.globalPosition: absolute position
    // details.localPosition: position relative to widget
  },
  onPanEnd: (details) {
    // details.velocity: velocity when user released
    // details.velocity.pixelsPerSecond: magnitude
  },
)
```

### 2. Real-Time Animation Control

Instead of using a controller's time-based progression, drive it directly with gesture:

```dart
@override
void _onPanUpdate(DragUpdateDetails details) {
  // Update controller value directly (0.0 → 1.0)
  _controller.value = (_controller.value + details.delta.dx / 300)
    .clamp(0.0, 1.0);
}
```

### 3. Velocity-Based Continuation

When gesture ends, use velocity to continue animation:

```dart
@override
void _onPanEnd(DragEndDetails details) {
  final velocity = details.velocity.pixelsPerSecond.dx;
  
  if (velocity.abs() > 200) {
    // User swiped fast—snap to nearest position
    _controller.animateTo(
      velocity > 0 ? 1.0 : 0.0,
      duration: Duration(milliseconds: 300),
      curve: Curves.easeOut,
    );
  } else {
    // Slow drag—snap to nearest
    _controller.animateTo(
      _controller.value > 0.5 ? 1.0 : 0.0,
      duration: Duration(milliseconds: 300),
      curve: Curves.easeOut,
    );
  }
}
```

---

## Practical Example: Draggable Slider

```dart
class DraggableSliderPage extends StatefulWidget {
  const DraggableSliderPage({Key? key}) : super(key: key);

  @override
  State<DraggableSliderPage> createState() => _DraggableSliderPageState();
}

class _DraggableSliderPageState extends State<DraggableSliderPage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  double _currentValue = 0.5;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);
    _currentValue = 0.5;
  }

  void _onPanUpdate(DragUpdateDetails details) {
    setState(() {
      final box = context.findRenderObject() as RenderBox;
      final localPosition = details.localPosition.dx;
      _currentValue = (localPosition / box.size.width).clamp(0.0, 1.0);
    });
  }

  void _onPanEnd(DragEndDetails details) {
    // Snap to nearest quarter
    final snapped = (_currentValue * 4).round() / 4;
    _controller.animateTo(
      snapped,
      duration: Duration(milliseconds: 200),
      curve: Curves.easeOut,
    );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          GestureDetector(
            onPanUpdate: _onPanUpdate,
            onPanEnd: _onPanEnd,
            child: Container(
              width: 300,
              height: 60,
              decoration: BoxDecoration(
                border: Border.all(),
                borderRadius: BorderRadius.circular(30),
              ),
              child: Stack(
                alignment: Alignment.center,
                children: [
                  // Background
                  Container(
                    height: 8,
                    margin: EdgeInsets.symmetric(
                      horizontal: 20,
                      vertical: 0,
                    ),
                    decoration: BoxDecoration(
                      color: Colors.grey[300],
                      borderRadius: BorderRadius.circular(4),
                    ),
                  ),
                  // Progress
                  Positioned(
                    left: 20,
                    child: Container(
                      width: 260 * _currentValue,
                      height: 8,
                      decoration: BoxDecoration(
                        color: Colors.blue,
                        borderRadius: BorderRadius.circular(4),
                      ),
                    ),
                  ),
                  // Thumb
                  Positioned(
                    left: 10 + 280 * _currentValue,
                    child: Container(
                      width: 40,
                      height: 40,
                      decoration: BoxDecoration(
                        color: Colors.blue,
                        shape: BoxShape.circle,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
          SizedBox(height: 30),
          Text('Value: ${(_currentValue * 100).toStringAsFixed(1)}%'),
        ],
      ),
    );
  }
}
```

---

## Practical Example: Swipeable Card

```dart
class SwipeableCardPage extends StatefulWidget {
  const SwipeableCardPage({Key? key}) : super(key: key);

  @override
  State<SwipeableCardPage> createState() => _SwipeableCardPageState();
}

class _SwipeableCardPageState extends State<SwipeableCardPage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _offsetAnimation;
  late Animation<double> _rotateAnimation;
  double _dragX = 0;
  double _dragRotation = 0;
  bool _dismissed = false;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);
    _offsetAnimation = Tween<Offset>(begin: Offset.zero, end: Offset.zero)
      .animate(_controller);
    _rotateAnimation = Tween<double>(begin: 0, end: 0).animate(_controller);
  }

  void _onPanUpdate(DragUpdateDetails details) {
    setState(() {
      _dragX = details.globalPosition.dx - 200;  // Center = 200
      _dragRotation = _dragX / 300;  // Rotate based on drag
    });
  }

  void _onPanEnd(DragEndDetails details) {
    final velocity = details.velocity.pixelsPerSecond.dx;
    final shouldDismiss = _dragX.abs() > 100 || velocity.abs() > 500;

    if (shouldDismiss) {
      final targetX = _dragX > 0 ? 500.0 : -500.0;
      _offsetAnimation = Tween<Offset>(
        begin: Offset(_dragX, 0),
        end: Offset(targetX, 0),
      ).animate(
        CurvedAnimation(parent: _controller, curve: Curves.easeOut),
      );
      _controller.forward().then((_) {
        setState(() => _dismissed = true);
      });
    } else {
      // Snap back
      _offsetAnimation = Tween<Offset>(
        begin: Offset(_dragX, 0),
        end: Offset.zero,
      ).animate(
        CurvedAnimation(parent: _controller, curve: Curves.elasticOut),
      );
      _controller.forward(from: 0);
    }
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    if (_dismissed) {
      return const Center(child: Text('Card dismissed!'));
    }

    return Center(
      child: GestureDetector(
        onPanUpdate: _onPanUpdate,
        onPanEnd: _onPanEnd,
        child: AnimatedBuilder(
          animation: _offsetAnimation,
          builder: (context, child) {
            final position = Offset(
              _dragX,
              _dragX.abs() * 0.1,  // Move up/down based on horizontal drag
            );

            return Transform(
              transform: Matrix4.identity()
                ..translate(position.dx, position.dy)
                ..setEntry(3, 2, 0.001)  // Perspective
                ..rotateZ(_dragRotation / 10),
              alignment: Alignment.center,
              child: Container(
                width: 300,
                height: 400,
                decoration: BoxDecoration(
                  color: Colors.blue,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.2),
                      blurRadius: 8,
                      offset: Offset(0, 4),
                    ),
                  ],
                ),
                child: Center(
                  child: Text(
                    'Swipe Me',
                    style: TextStyle(color: Colors.white, fontSize: 24),
                  ),
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}
```

---

## Practical Example: Pinch-to-Zoom

```dart
class PinchZoomPage extends StatefulWidget {
  const PinchZoomPage({Key? key}) : super(key: key);

  @override
  State<PinchZoomPage> createState() => _PinchZoomPageState();
}

class _PinchZoomPageState extends State<PinchZoomPage> {
  double _scale = 1.0;
  Offset _offset = Offset.zero;

  void _onScaleStart(ScaleStartDetails details) {
    // Store initial state
  }

  void _onScaleUpdate(ScaleUpdateDetails details) {
    setState(() {
      _scale = details.scale;
      _offset = details.focalPoint;
    });
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onScaleStart: _onScaleStart,
      onScaleUpdate: _onScaleUpdate,
      child: Transform(
        transform: Matrix4.identity()
          ..translate(_offset.dx, _offset.dy)
          ..scale(_scale),
        alignment: Alignment.center,
        child: Container(
          width: 300,
          height: 300,
          color: Colors.blue,
          child: const Center(
            child: Text('Pinch to zoom'),
          ),
        ),
      ),
    );
  }
}
```

---

## Velocity Calculations

```dart
// Get velocity in any direction
final velocity = details.velocity.pixelsPerSecond;

// Magnitude
final speed = velocity.distance;

// Direction
final direction = velocity.direction;

// Component
final horizontalVelocity = velocity.dx;
final verticalVelocity = velocity.dy;
```

---

## Exercises

### Exercise 1: Dismissible List Item
Create a list item that:
- Slides left/right on drag
- Shows delete button behind
- Dismisses if swiped far enough
- Snaps back if released early
- Hint: Use Transform.translate with gesture

### Exercise 2: Drawer Animation
Create a slide-out menu that:
- Opens/closes on swipe
- Closes on outside tap
- Responds to gesture velocity
- Hint: Use velocity thresholds to decide snap direction

### Exercise 3: Image Carousel
Create a carousel that:
- Swipes between images
- Uses velocity to choose next image
- Snaps when released
- Hint: Calculate velocity threshold

### Exercise 4: Rotatable Wheel
Create a spinning wheel that:
- Responds to drag
- Continues spinning after release
- Decelerates naturally
- Hint: Use velocity as initial angular velocity

---

## Performance Considerations

- **Avoid Expensive Rebuilds**: Use `RepaintBoundary` around interactive areas
- **Frame Rate Matters**: 60fps minimum for smooth gestures
- **Profile Under Load**: Test with multiple gestures

---

## Key Takeaways

1. **Gestures Drive Animations**: Real-time control via touches
2. **Velocity Continues Motion**: Snap or coast based on speed
3. **Thresholds Define Behavior**: When to snap vs. dismiss
4. **Perspective Adds Depth**: Use Matrix4 for 3D effects
5. **Test on Real Devices**: Gesture feel varies by hardware

---

## Next Steps

- Complete all exercises
- Build several interactive components
- Module 11 adds 3D transformations to gesture-driven animations

---

## Resources

- [GestureDetector](https://api.flutter.dev/flutter/widgets/GestureDetector-class.html)
- [Velocity](https://api.flutter.dev/flutter/physics/Velocity-class.html)
- [Gesture Events](https://flutter.dev/docs/development/ui/advanced/gestures)
