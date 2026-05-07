# Module 09: Physics-Based Animations

## Overview

Animations driven by physics feel natural—they respond to forces, velocity, and momentum. This module teaches spring simulations, damping, and creating interactions that feel real.

**Duration:** 3–4 hours  
**Prerequisites:** Modules 01–08  
**Learning Outcomes:**
- Understand SpringSimulation and physics curves
- Implement momentum-based animations
- Use velocity to create responsive interactions
- Build natural, tactile feel

---

## Key Concepts

### 1. Springs vs. Linear

**Linear (Mechanical):**
```dart
Tween<double>(begin: 0, end: 1).animate(_controller)
```
Feels robotic. Uniform speed throughout.

**Spring (Natural):**
```dart
_controller.animateTo(
  1.0,
  duration: Duration(milliseconds: 500),
  curve: Curves.elasticOut,
)
```
Bounces and settles. Feels alive.

### 2. SpringSimulation

Low-level physics API for custom spring behavior:

```dart
final springDesc = SpringDescription(
  mass: 1.0,           // How heavy
  stiffness: 100.0,    // How stiff (higher = faster)
  damping: 10.0,       // How resistant (higher = less bouncy)
);

_controller.animateWith(
  SpringSimulation(springDesc, 0, 1, 0),
);
```

### 3. Easing Curves for Physics Feel

```dart
Curves.elasticOut      // Bounces at end
Curves.elasticInOut    // Bounces at both ends
Curves.bounceOut       // Cartoonish bounce
Curves.bounceInOut     // Bouncy entrance/exit
```

---

## Practical Example: Bouncy Button

```dart
class BouncyButtonPage extends StatefulWidget {
  const BouncyButtonPage({Key? key}) : super(key: key);

  @override
  State<BouncyButtonPage> createState() => _BouncyButtonPageState();
}

class _BouncyButtonPageState extends State<BouncyButtonPage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _scaleAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);

    _scaleAnimation = Tween<double>(begin: 1.0, end: 0.95).animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeInOut),
    );
  }

  void _onTapDown() {
    _controller.forward();
  }

  void _onTapUp() {
    _controller.animateTo(
      0,
      duration: Duration(milliseconds: 300),
      curve: Curves.elasticOut,
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
      child: GestureDetector(
        onTapDown: (_) => _onTapDown(),
        onTapUp: (_) => _onTapUp(),
        onTapCancel: _onTapUp,
        child: AnimatedBuilder(
          animation: _scaleAnimation,
          builder: (context, child) {
            return Transform.scale(
              scale: _scaleAnimation.value,
              child: Container(
                padding: EdgeInsets.symmetric(
                  horizontal: 32,
                  vertical: 16 * _scaleAnimation.value,
                ),
                decoration: BoxDecoration(
                  color: Colors.blue,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: const Text(
                  'Tap Me',
                  style: TextStyle(color: Colors.white),
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

## Practical Example: Momentum Scrolling

Simulating inertia when user stops dragging:

```dart
class MomentumAnimationPage extends StatefulWidget {
  const MomentumAnimationPage({Key? key}) : super(key: key);

  @override
  State<MomentumAnimationPage> createState() => _MomentumAnimationPageState();
}

class _MomentumAnimationPageState extends State<MomentumAnimationPage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _offsetAnimation;
  Offset _currentOffset = Offset.zero;
  Offset _dragStart = Offset.zero;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);
    
    _offsetAnimation = Tween<Offset>(
      begin: Offset.zero,
      end: Offset.zero,
    ).animate(
      CurvedAnimation(parent: _controller, curve: Curves.decelerate),
    );
  }

  void _onPanUpdate(DragUpdateDetails details) {
    // Track position during drag
    setState(() {
      _currentOffset = _currentOffset + details.delta;
    });
  }

  void _onPanEnd(DragEndDetails details) {
    // Apply momentum based on velocity
    final velocity = details.velocity.pixelsPerSecond;
    final unitsPerSecondToPixels = 100.0;
    final pixelsPerSecond = velocity.distance;
    final direction = velocity / pixelsPerSecond;
    
    // Decelerate over 500ms
    _offsetAnimation = Tween<Offset>(
      begin: _currentOffset,
      end: _currentOffset + direction * pixelsPerSecond * 0.1,
    ).animate(
      CurvedAnimation(parent: _controller, curve: Curves.decelerate),
    );

    _controller.forward(from: 0);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onPanUpdate: _onPanUpdate,
      onPanEnd: _onPanEnd,
      child: AnimatedBuilder(
        animation: _offsetAnimation,
        builder: (context, child) {
          return Transform.translate(
            offset: _offsetAnimation.value,
            child: Container(
              width: 100,
              height: 100,
              color: Colors.blue,
              alignment: Alignment.center,
              child: const Text('Drag Me'),
            ),
          );
        },
      ),
    );
  }
}
```

---

## Practical Example: Spring Simulation

Direct physics simulation (advanced):

```dart
class SpringSimulationPage extends StatefulWidget {
  const SpringSimulationPage({Key? key}) : super(key: key);

  @override
  State<SpringSimulationPage> createState() => _SpringSimulationPageState();
}

class _SpringSimulationPageState extends State<SpringSimulationPage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _scaleAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);
    
    _scaleAnimation = Tween<double>(begin: 1.0, end: 2.0).animate(_controller);
  }

  void _animateWithSpring() {
    // Define spring physics
    final springDesc = SpringDescription(
      mass: 1.0,         // Object mass
      stiffness: 100.0,  // Spring stiffness (higher = faster)
      damping: 10.0,     // Damping (higher = less bouncy)
    );

    // Create simulation: spring from current to target
    final simulation = SpringSimulation(
      springDesc,
      _controller.value,  // Current value
      1.0,                // Target value
      0.0,                // Initial velocity
    );

    _controller.animateWith(simulation);
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
          AnimatedBuilder(
            animation: _scaleAnimation,
            builder: (context, child) {
              return Transform.scale(
                scale: _scaleAnimation.value,
                child: Container(
                  width: 100,
                  height: 100,
                  color: Colors.blue,
                ),
              );
            },
          ),
          const SizedBox(height: 40),
          ElevatedButton(
            onPressed: _animateWithSpring,
            child: const Text('Spring!'),
          ),
        ],
      ),
    );
  }
}
```

---

## Tuning Springs

Three parameters control spring behavior:

| Parameter | Effect | Value Range |
|-----------|--------|-------------|
| **mass** | How heavy | 0.1 (light) to 10 (heavy) |
| **stiffness** | How fast | 10 (slow) to 1000 (fast) |
| **damping** | How bouncy | 0 (very bouncy) to 30 (no bounce) |

**Presets:**
```dart
// Snappy, responsive
SpringDescription(mass: 1.0, stiffness: 100, damping: 10);

// Smooth, flowing
SpringDescription(mass: 2.0, stiffness: 50, damping: 15);

// Bouncy, playful
SpringDescription(mass: 1.0, stiffness: 200, damping: 5);

// Sluggish, heavy
SpringDescription(mass: 5.0, stiffness: 50, damping: 20);
```

---

## Common Curves for Physics Feel

```dart
Curves.elasticOut      // Bounce at end (settling)
Curves.elasticInOut    // Bounce at both ends
Curves.bounceOut       // Cartoonish bounce
Curves.decelerate      // Slows down (momentum)
Curves.easeOutBack     // Slight overshoot
```

---

## Exercises

### Exercise 1: Springy Slider
Create a slider that:
- Bounces when you release it
- Springs back to position
- Hint: Use SpringSimulation on drag end

### Exercise 2: Bouncy List
Create a list where:
- Items bounce when scrolling
- Use elasticOut curve
- Hint: Animate items as they appear

### Exercise 3: Momentum Plate
Create a draggable plate that:
- Moves smoothly when dragged
- Continues moving (momentum) when released
- Decelerates naturally
- Hint: Use decelerate curve with velocity

### Exercise 4: Custom Spring Tuner
Create an interactive spring tuner:
- Sliders for mass, stiffness, damping
- Visual preview of animation
- Shows how parameters affect motion
- Hint: Use SpringDescription with slider values

---

## Physics Formula Insight

If you're curious, springs follow:
```
F = -kx + -cv
where:
  k = stiffness
  x = displacement
  c = damping
  v = velocity
```

The controller integrates this force over time.

---

## Key Takeaways

1. **Springs Feel Natural**: Use them for interactive elements
2. **Tune Parameters Carefully**: Small changes have big effects
3. **Velocity Matters**: Momentum makes animations responsive
4. **Curves Approximate Physics**: Use for simple cases
5. **SpringSimulation for Complex**: When you need precise control

---

## Next Steps

- Complete all exercises
- Experiment with spring parameters
- Module 10 combines physics with gestures for interactive animations

---

## Resources

- [SpringSimulation](https://api.flutter.dev/flutter/physics/SpringSimulation-class.html)
- [Simulation](https://api.flutter.dev/flutter/physics/Simulation-class.html)
- [Spring Physics](https://en.wikipedia.org/wiki/Hooke%27s_law)
