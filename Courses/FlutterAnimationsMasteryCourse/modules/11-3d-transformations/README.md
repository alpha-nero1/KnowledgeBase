# Module 11: 3D Transformations & Quaternions

## Overview

For truly advanced animations, you need 3D math. This module teaches Matrix4 transformations, perspective projection, and quaternions—the math behind complex spatial animations like the globe in your capstone project.

**Duration:** 4–5 hours  
**Prerequisites:** Modules 01–10  
**Learning Outcomes:**
- Understand Matrix4 and affine transformations
- Implement 3D rotations with quaternions
- Create perspective and depth effects
- Build complex spatial animations

---

## Key Concepts

### 1. Matrix4 Transformations

A 4×4 matrix defines a 3D transformation:

```dart
// Identity (no transformation)
Matrix4.identity()

// Translation
Matrix4.translationValues(10, 20, 0)

// Scale
Matrix4.diagonal3(Vector3(2, 2, 2))

// Rotation around Z axis
Matrix4.rotationZ(0.5)  // radians

// Perspective (camera setup)
Matrix4.identity()
  ..setEntry(3, 2, 0.001)  // Depth perspective

// Composition (combine transformations)
Matrix4.identity()
  ..translate(100, 50, 0)
  ..rotateZ(0.5)
  ..scale(2);
```

### 2. Why Quaternions?

**Problem with Euler angles (rotationX, rotationY, rotationZ):**
- Gimbal lock (loss of degree of freedom)
- Non-intuitive interpolation

**Solution: Quaternions:**
- Smooth interpolation
- No gimbal lock
- Compact representation

### 3. Quaternion Structure

```dart
// Quaternion: [x, y, z, w]
// w is scalar, [x, y, z] is vector part

List<double> quaternion = [0, 0, 0, 1];  // Identity

// From axis-angle
Quaternion.axisAngle(Vector3(0, 1, 0), 0.5);  // Rotate around Y axis by 0.5 rad

// To matrix
final matrix = Matrix4.identity();
// ... apply quaternion to matrix
```

---

## Practical Example: 3D Cube Rotation

```dart
class CubeRotationPage extends StatefulWidget {
  const CubeRotationPage({Key? key}) : super(key: key);

  @override
  State<CubeRotationPage> createState() => _CubeRotationPageState();
}

class _CubeRotationPageState extends State<CubeRotationPage>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _rotationXAnimation;
  late Animation<double> _rotationYAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: Duration(seconds: 4),
      vsync: this,
    )..repeat();

    _rotationXAnimation = Tween<double>(begin: 0, end: 2 * 3.14159)
      .animate(_controller);

    _rotationYAnimation = Tween<double>(begin: 0, end: 2 * 3.14159)
      .animate(
        CurvedAnimation(
          parent: _controller,
          curve: Interval(0.0, 1.0),
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
    return Center(
      child: AnimatedBuilder(
        animation: Listenable.merge([_rotationXAnimation, _rotationYAnimation]),
        builder: (context, child) {
          return Transform(
            alignment: Alignment.center,
            transform: Matrix4.identity()
              ..setEntry(3, 2, 0.001)  // Perspective
              ..rotateX(_rotationXAnimation.value)
              ..rotateY(_rotationYAnimation.value),
            child: Container(
              width: 200,
              height: 200,
              decoration: BoxDecoration(
                border: Border.all(color: Colors.blue, width: 2),
              ),
              child: Stack(
                children: [
                  // Front face
                  _buildFace(Colors.red, 'Front'),
                  // Other faces would go here
                ],
              ),
            ),
          );
        },
      ),
    );
  }

  Widget _buildFace(Color color, String label) {
    return Container(
      color: color.withOpacity(0.7),
      alignment: Alignment.center,
      child: Text(label, style: TextStyle(fontSize: 24)),
    );
  }
}
```

---

## Practical Example: Quaternion-Based Trackball

This is the core of your capstone project:

```dart
class QuaternionTrackballPage extends StatefulWidget {
  const QuaternionTrackballPage({Key? key}) : super(key: key);

  @override
  State<QuaternionTrackballPage> createState() => _QuaternionTrackballPageState();
}

class _QuaternionTrackballPageState extends State<QuaternionTrackballPage> {
  // Quaternion representation: [x, y, z, w]
  List<double> _quaternion = [0, 0, 0, 1];
  Offset _lastDragPosition = Offset.zero;

  /// Convert quaternion to Matrix4
  Matrix4 _quaternionToMatrix(List<double> q) {
    final x = q[0], y = q[1], z = q[2], w = q[3];

    return Matrix4(
      1 - 2 * (y * y + z * z), 2 * (x * y - w * z), 2 * (x * z + w * y), 0,
      2 * (x * y + w * z), 1 - 2 * (x * x + z * z), 2 * (y * z - w * x), 0,
      2 * (x * z - w * y), 2 * (y * z + w * x), 1 - 2 * (x * x + y * y), 0,
      0, 0, 0, 1,
    );
  }

  /// Multiply two quaternions
  List<double> _multiplyQuaternions(List<double> q1, List<double> q2) {
    return [
      q1[3] * q2[0] + q1[0] * q2[3] + q1[1] * q2[2] - q1[2] * q2[1],
      q1[3] * q2[1] - q1[0] * q2[2] + q1[1] * q2[3] + q1[2] * q2[0],
      q1[3] * q2[2] + q1[0] * q2[1] - q1[1] * q2[0] + q1[2] * q2[3],
      q1[3] * q2[3] - q1[0] * q2[0] - q1[1] * q2[1] - q1[2] * q2[2],
    ];
  }

  /// Create quaternion from axis-angle
  List<double> _axisAngleToQuaternion(Vector3 axis, double angle) {
    final halfAngle = angle / 2;
    final s = sin(halfAngle);
    return [
      axis.x * s,
      axis.y * s,
      axis.z * s,
      cos(halfAngle),
    ];
  }

  void _onPanUpdate(DragUpdateDetails details) {
    final delta = details.delta;
    final radius = 150.0;  // Trackball radius

    // Create rotation from drag
    final dragVector = Vector3(-delta.dy, delta.dx, 0).normalized();
    final angle = delta.distance / radius;

    final dragQuaternion = _axisAngleToQuaternion(dragVector, angle);
    
    setState(() {
      _quaternion = _multiplyQuaternions(_quaternion, dragQuaternion);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: GestureDetector(
        onPanUpdate: _onPanUpdate,
        child: Transform(
          alignment: Alignment.center,
          transform: _quaternionToMatrix(_quaternion)
            ..setEntry(3, 2, 0.001),  // Perspective
          child: Container(
            width: 300,
            height: 300,
            decoration: BoxDecoration(
              border: Border.all(color: Colors.blue, width: 2),
              color: Colors.blue.withOpacity(0.1),
            ),
            child: Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text('Drag to rotate'),
                  const SizedBox(height: 20),
                  Text(
                    'Quat: [${_quaternion[0].toStringAsFixed(2)}, '
                    '${_quaternion[1].toStringAsFixed(2)}, '
                    '${_quaternion[2].toStringAsFixed(2)}, '
                    '${_quaternion[3].toStringAsFixed(2)}]',
                    style: TextStyle(fontSize: 12),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

import 'dart:math';
```

---

## Matrix4 Operations

```dart
// Create custom matrix
final matrix = Matrix4.identity();

// Translate
matrix.translate(10, 20, 30);

// Rotate around axes
matrix.rotateX(0.5);  // Around X
matrix.rotateY(0.5);  // Around Y
matrix.rotateZ(0.5);  // Around Z

// Scale
matrix.scale(2, 2, 2);

// Multiply matrices
final result = matrix..multiply(otherMatrix);

// Apply to widget
Transform(
  transform: matrix,
  alignment: Alignment.center,
  child: child,
)
```

---

## Perspective & Depth

```dart
// Add perspective (camera effect)
final matrix = Matrix4.identity();
matrix.setEntry(3, 2, 0.001);  // 0.001 = strong perspective

// The further away an object, the smaller (3D illusion)
```

---

## Exercises

### Exercise 1: 3D Cube
Build a rotating 3D cube:
- 6 faces with different colors
- Rotates continuously
- Hint: Layer 6 transforms for each face

### Exercise 2: Quaternion Lerp
Interpolate between two quaternions:
- Create two orientations
- Smoothly transition between them
- Hint: Use slerp (spherical linear interpolation)

### Exercise 3: Trackball Widget
Create the quaternion trackball (similar to capstone):
- Drag to rotate
- Smooth continuous rotation
- Hint: Use axis-angle to quaternion conversion

### Exercise 4: 3D Graph Visualization
Create a 3D scatter plot:
- Points in 3D space
- Rotatable via gesture
- Z-axis depth sorting for painter order
- Hint: Project 3D → 2D, sort by Z

---

## Key Takeaways

1. **Matrix4 is Powerful**: Represents all 3D transformations
2. **Quaternions are Smooth**: Better for 3D rotations than Euler angles
3. **Perspective Adds Depth**: setEntry(3, 2, 0.001) creates 3D illusion
4. **Composition Matters**: Build complex transforms from primitives
5. **Math Underpins Everything**: Understanding geometry helps you build better animations

---

## Next Steps

- Complete all exercises
- Build interactive 3D elements
- Ready for Module 12 (Performance & Capstone)

---

## Resources

- [Matrix4 API](https://api.flutter.dev/flutter/vector_math_64/Matrix4-class.html)
- [Quaternion Math](https://en.wikipedia.org/wiki/Quaternion)
- [3D Graphics Primers](https://learnopengl.com/)
