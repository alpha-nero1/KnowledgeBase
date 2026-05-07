# Module 08: Custom Painters & Canvas Animations

## Overview

The widget tree can't do everything. For complex, performant graphics, you render directly to a canvas. This module teaches you `CustomPaint`, drawing primitives, and optimizing canvas animations for 60fps+.

**Duration:** 4–5 hours  
**Prerequisites:** Modules 01–07  
**Learning Outcomes:**
- Understand CustomPaint and CustomPainter
- Draw shapes, paths, text, and images
- Optimize canvas rendering
- Build complex animations with canvas

---

## Key Concepts

### 1. What is CustomPaint?

A widget that lets you draw arbitrary graphics using `Canvas`. Perfect for:
- Complex shapes and paths
- Smooth animations (no widget overhead)
- Real-time graphics
- Data visualizations

### 2. Basic Structure

```dart
class MyPainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    // Draw here
  }

  @override
  bool shouldRepaint(MyPainter oldDelegate) => false;
}

// Use it:
CustomPaint(
  painter: MyPainter(),
  size: Size(300, 300),
)
```

### 3. Drawing Primitives

```dart
// Rectangle
canvas.drawRect(
  Rect.fromLTWH(10, 10, 100, 100),
  Paint()..color = Colors.blue,
);

// Circle
canvas.drawCircle(
  Offset(150, 150),
  50,
  Paint()..color = Colors.red,
);

// Path (complex shapes)
final path = Path();
path.moveTo(0, 0);
path.lineTo(100, 0);
path.quadraticBezierTo(100, 100, 0, 100);
path.close();
canvas.drawPath(path, Paint()..color = Colors.green);

// Text
TextPainter(
  text: TextSpan(
    text: 'Hello',
    style: TextStyle(color: Colors.black, fontSize: 16),
  ),
  textDirection: TextDirection.ltr,
)
  ..layout()
  ..paint(canvas, Offset(10, 10));

// Image
canvas.drawImageRect(
  image,
  Rect.fromLTWH(0, 0, image.width.toDouble(), image.height.toDouble()),
  Rect.fromLTWH(10, 10, 100, 100),
  Paint(),
);
```

---

## Practical Example: Animated Circle

```dart
class AnimatedCirclePainter extends CustomPainter {
  final double radius;
  final Color color;

  AnimatedCirclePainter({
    required this.radius,
    required this.color,
  });

  @override
  void paint(Canvas canvas, Size size) {
    final center = Offset(size.width / 2, size.height / 2);
    canvas.drawCircle(
      center,
      radius,
      Paint()..color = color,
    );
  }

  @override
  bool shouldRepaint(AnimatedCirclePainter oldDelegate) =>
      oldDelegate.radius != radius || oldDelegate.color != color;
}

// Use it:
class AnimatedCircleWidget extends StatefulWidget {
  const AnimatedCircleWidget({Key? key}) : super(key: key);

  @override
  State<AnimatedCircleWidget> createState() => _AnimatedCircleWidgetState();
}

class _AnimatedCircleWidgetState extends State<AnimatedCircleWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _radiusAnimation;
  late Animation<Color?> _colorAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 2),
      vsync: this,
    )..repeat(reverse: true);

    _radiusAnimation = Tween<double>(begin: 20, end: 100).animate(_controller);
    _colorAnimation = ColorTween(begin: Colors.red, end: Colors.blue)
      .animate(_controller);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: Listenable.merge([_radiusAnimation, _colorAnimation]),
      builder: (context, _) {
        return CustomPaint(
          painter: AnimatedCirclePainter(
            radius: _radiusAnimation.value,
            color: _colorAnimation.value ?? Colors.red,
          ),
          size: const Size(300, 300),
        );
      },
    );
  }
}
```

---

## Practical Example: Progress Arc

```dart
class ProgressArcPainter extends CustomPainter {
  final double progress;
  final Color color;
  final double strokeWidth;

  ProgressArcPainter({
    required this.progress,
    required this.color,
    this.strokeWidth = 4,
  });

  @override
  void paint(Canvas canvas, Size size) {
    final center = Offset(size.width / 2, size.height / 2);
    final radius = size.width / 2 - strokeWidth;

    // Background arc (light gray)
    canvas.drawArc(
      Rect.fromCircle(center: center, radius: radius),
      0,
      2 * 3.14159,
      false,
      Paint()
        ..color = Colors.grey[300]!
        ..strokeWidth = strokeWidth
        ..style = PaintingStyle.stroke,
    );

    // Progress arc (colored)
    canvas.drawArc(
      Rect.fromCircle(center: center, radius: radius),
      -3.14159 / 2,  // Start at top
      progress * 2 * 3.14159,  // Sweep based on progress
      false,
      Paint()
        ..color = color
        ..strokeWidth = strokeWidth
        ..style = PaintingStyle.stroke
        ..strokeCap = StrokeCap.round,
    );

    // Percentage text
    TextPainter(
      text: TextSpan(
        text: '${(progress * 100).toStringAsFixed(0)}%',
        style: const TextStyle(color: Colors.black, fontSize: 20),
      ),
      textDirection: TextDirection.ltr,
    )
      ..layout()
      ..paint(
        canvas,
        center - Offset(textPainter.width / 2, textPainter.height / 2),
      );
  }

  @override
  bool shouldRepaint(ProgressArcPainter oldDelegate) =>
      oldDelegate.progress != progress || oldDelegate.color != color;
}

// Use:
class ProgressArcWidget extends StatefulWidget {
  const ProgressArcWidget({Key? key}) : super(key: key);

  @override
  State<ProgressArcWidget> createState() => _ProgressArcWidgetState();
}

class _ProgressArcWidgetState extends State<ProgressArcWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _progressAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(seconds: 3),
      vsync: this,
    );

    _progressAnimation = Tween<double>(begin: 0, end: 1).animate(
      CurvedAnimation(parent: _controller, curve: Curves.easeInOut),
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
      animation: _progressAnimation,
      builder: (context, _) {
        return CustomPaint(
          painter: ProgressArcPainter(
            progress: _progressAnimation.value,
            color: Colors.blue,
          ),
          size: const Size(200, 200),
        );
      },
    );
  }
}
```

---

## Optimization: shouldRepaint

Critical for performance. Tells Flutter when to redraw.

```dart
@override
bool shouldRepaint(MyPainter oldDelegate) {
  // Return true only if painting changed
  // This check runs EVERY frame—keep it fast
  
  // ✅ Good
  return oldDelegate.value != value;
  
  // ❌ Bad (always repaints)
  return true;
}
```

### Semantic Repaint Boundaries

Wrap expensive painters in `RepaintBoundary`:

```dart
RepaintBoundary(
  child: CustomPaint(
    painter: ExpensivePainter(),
  ),
)
```

This caches the painter output, only repainting when needed.

---

## Practical Example: Waveform Visualization

```dart
class WaveformPainter extends CustomPainter {
  final List<double> samples;
  final Color color;

  WaveformPainter({
    required this.samples,
    required this.color,
  });

  @override
  void paint(Canvas canvas, Size size) {
    if (samples.isEmpty) return;

    final path = Path();
    final width = size.width / samples.length;
    
    path.moveTo(0, size.height / 2);

    for (int i = 0; i < samples.length; i++) {
      final x = i * width;
      final y = size.height / 2 + samples[i] * (size.height / 2);
      path.lineTo(x, y);
    }

    canvas.drawPath(
      path,
      Paint()
        ..color = color
        ..strokeWidth = 2
        ..style = PaintingStyle.stroke
        ..strokeCap = StrokeCap.round,
    );
  }

  @override
  bool shouldRepaint(WaveformPainter oldDelegate) =>
      oldDelegate.samples != samples || oldDelegate.color != color;
}
```

---

## Exercises

### Exercise 1: Animated Pie Chart
Draw a pie chart that animates:
- Slices appear one at a time
- Each slice has a different color
- Hint: Use `drawArc()` with interval timing

### Exercise 2: Sine Wave Animation
Draw an animating sine wave:
- Wave moves horizontally
- Amplitude increases/decreases
- Hint: Calculate points along sine function

### Exercise 3: Particle System
Create a simple particle animation:
- Multiple circles moving and fading
- Collide with boundaries
- Hint: Update particle positions each frame

### Exercise 4: Custom Gauge
Create a speedometer-style gauge:
- Background arc
- Needle rotates based on value
- Labels at intervals
- Hint: Use Transform.rotate for needle

---

## Performance Tips

1. **Use shouldRepaint**: Only redraw when necessary
2. **RepaintBoundary**: Cache expensive painters
3. **Clip Unnecessary Areas**: Use `canvas.clipRect()`
4. **Reduce Path Complexity**: Simplify curves
5. **Profile with DevTools**: Check frame rendering times

---

## Key Takeaways

1. **Canvas is Powerful**: Render anything you can imagine
2. **shouldRepaint is Critical**: Directly impacts performance
3. **Optimize Early**: Profile before optimizing
4. **Layers Help**: Use RepaintBoundary strategically
5. **Math Matters**: Understand geometry for complex shapes

---

## Next Steps

- Complete all exercises
- Build a custom visualization or game element
- Module 09 will add physics to your canvas animations

---

## Resources

- [Canvas API](https://api.flutter.dev/flutter/dart-ui/Canvas-class.html)
- [CustomPaint](https://api.flutter.dev/flutter/widgets/CustomPaint-class.html)
- [Paint Class](https://api.flutter.dev/flutter/dart-ui/Paint-class.html)
