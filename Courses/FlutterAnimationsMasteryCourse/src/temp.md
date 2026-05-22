import 'package:flutter/material.dart';

class DissipatingBouncyBall extends StatefulWidget {
  const DissipatingBouncyBall({super.key});

  @override
  State<DissipatingBouncyBall> createState() => _DissipatingBouncyBallState();
}

class _DissipatingBouncyBallState extends State<DissipatingBouncyBall>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _animation;

  // The floor line constraint (leaving a tiny bit of room for the 50px ball height)
  final double _floorY = 0.9;
  
  // Track our dynamic boundaries across cycles
  double _currentPeakY = -1.0; // Starts at the absolute top
  bool _isFalling = true;

  @override
  void initState() {
    super.initState();
    
    // Each single direction (down or up) takes 600ms
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 600),
    );

    // Set up a standard linear driver (we apply curves manually inside the builder)
    _animation = Tween<double>(begin: 0.0, end: 1.0).animate(_controller);

    // Listen for state changes to manage the loop lifecycle and energy loss
    _controller.addStatusListener((status) {
      if (status == AnimationStatus.completed) {
        if (_isFalling) {
          // HIT THE FLOOR: Switch directions to bounce back up
          _isFalling = false;
          
          // Energy Loss Calculation: Lose 40% of height capability each bounce
          // Moving toward 0.9 means we interpolate: currentPeak + (floor - currentPeak) * lossFactor
          _currentPeakY = _currentPeakY + (_floorY - _currentPeakY) * 0.40;

          // If the ball has lost almost all energy, stop the loop entirely
          if ((_floorY - _currentPeakY).abs() < 0.05) {
            _controller.stop();
            return;
          }

          _controller.forward(from: 0.0);
        } else {
          // HIT THE PEAK: Switch directions to fall back down to earth
          _isFalling = true;
          _controller.forward(from: 0.0);
        }
      }
    });
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void _startBouncing() {
    setState(() {
      _currentPeakY = -1.0; // Reset energy state to full height
      _isFalling = true;
    });
    _controller.forward(from: 0.0);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[900],
      body: SafeArea(
        child: Stack(
          children: [
            AnimatedBuilder(
              animation: _animation,
              builder: (context, child) {
                double currentY;
                
                if (_isFalling) {
                  // FALLING: Interpolate from the dynamic peak down to the floor
                  // Apply an easeIn curve to simulate accelerating gravity speed
                  double t = Curves.easeInQuad.transform(_animation.value);
                  currentY = Tween<double>(begin: _currentPeakY, end: _floorY).transform(t);
                } else {
                  // RISING: Interpolate from the floor back up to the reduced peak
                  // Apply an easeOut curve to simulate slowing down as it reaches apex
                  double t = Curves.easeOutQuad.transform(_animation.value);
                  currentY = Tween<double>(begin: _floorY, end: _currentPeakY).transform(t);
                }

                return Align(
                  alignment: Alignment(0.0, currentY),
                  child: child,
                );
              },
              child: Container(
                width: 50,
                height: 50,
                decoration: const BoxDecoration(
                  color: Colors.amber,
                  shape: BoxShape.circle,
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black38,
                      blurRadius: 8,
                      offset: Offset(0, 4),
                    )
                  ],
                ),
              ),
            ),
            
            // Trigger overlay panel
            Positioned(
              bottom: 40,
              left: 0,
              right: 0,
              child: Center(
                child: ElevatedButton(
                  style: ElevatedButton.styleFrom(backgroundColor: Colors.white),
                  onPressed: _startBouncing,
                  child: const Text('Drop Ball', style: TextStyle(color: Colors.black)),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}