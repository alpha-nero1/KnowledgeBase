import 'dart:math';

import 'package:flutter/material.dart';

enum Curve {
  sin,
  cos,
  tan
}

class CurvePainter extends CustomPainter {
  final Curve curve;
  final double? powFactor;
  CurvePainter({ required this.curve, this.powFactor });

  @override
  void paint(Canvas canvas, Size size) {
    _drawBaseline(canvas, size);
    final paint = Paint()
      ..color = Colors.black
      ..style = PaintingStyle.stroke
      ..strokeWidth = 10
      ..isAntiAlias = true;

    final path = Path();

    double baseline = size.height / 2;
    double amplitude = 100;

    // Crazy! loop every pixel of the width and draw a line to the
    // projected y point.
    for (double x = 0; x <= size.width; x++) {
      double normalisedX = x / size.width;
      (double, double) rawY = _calculateRawY(baseline, normalisedX);
      final adjustedY = powFactor != null
        ? pow(rawY.$1, powFactor!) * rawY.$2
        : rawY.$1 * rawY.$2;

      final actaulY = baseline - (adjustedY * amplitude);
      /// Magic: this is the magic right here, you use path.moveTo()
      /// to move your pointer to a start position, and a lineTo() call
      /// to draw a line from existing coordinate to the next (x, y) one
      /// you provide!
      ///
      if (x == 0) {
        path.moveTo(0, actaulY);
      } else {
        path.lineTo(x, actaulY);
      }
    }

    canvas.drawPath(path, paint);
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) {
    return false;
  }

  void _drawBaseline(Canvas canvas, Size size) {
    final paint = Paint()
      ..color = Colors.grey
      ..style = PaintingStyle.stroke
      ..strokeWidth = 4
      ..isAntiAlias = true;

    final path = Path();

    final baseline = size.height / 2;

    path.moveTo(0, baseline);
    path.lineTo(size.width, baseline);

    canvas.drawPath(path, paint);
  }

  (double, double) _calculateRawY(double baseline, double normalisedX) {
    if (curve == Curve.tan) {
      final rawTan = tan(normalisedX * pi);
      final sign = rawTan.sign;
      final absoluteTan= rawTan.abs();
      return (absoluteTan, sign);
    } 
    if (curve == Curve.cos) {
      final rawCos = cos(normalisedX * pi);
      final sign = rawCos.sign;
      final absoluteCos = rawCos.abs();
      return (absoluteCos, sign);
    }
    final rawSin = sin(normalisedX * pi);
    final sign = rawSin.sign;
    final absoluteSin = rawSin.abs();
    return (absoluteSin, sign);
  }
}

class Seven01CurveCity extends StatelessWidget {
  const Seven01CurveCity({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(backgroundColor: Colors.blue,),
      body: SafeArea(
      child: Container(
        color: Colors.blue,
        height: double.infinity,
        child: SingleChildScrollView(
          child: Column(
            children: [
              Text('Sin'),
              CustomPaint(
                size: Size(double.infinity, 300),
                painter:  CurvePainter(curve: Curve.sin),),
              SizedBox(height: 20),
              Text('Sin flattened'),
              CustomPaint(
                size: Size(double.infinity, 300),
                painter:  CurvePainter(curve: Curve.sin, powFactor: 0.4),),
              SizedBox(height: 20),
              Text('Sin peak'),
              CustomPaint(
                size: Size(double.infinity, 300),
                painter:  CurvePainter(curve: Curve.sin, powFactor: 2),),
              SizedBox(height: 20),
              Text('Cos'),
              CustomPaint(
                size: Size(double.infinity, 300),
                painter:  CurvePainter(curve: Curve.cos),),
              SizedBox(height: 20),
              Text('Cos flattened'),
              CustomPaint(
                size: Size(double.infinity, 300),
                painter:  CurvePainter(curve: Curve.cos, powFactor: 0.4)),
              SizedBox(height: 20),
              Text('Cos peak'),
              CustomPaint(
                size: Size(double.infinity, 300),
                painter:  CurvePainter(curve: Curve.cos, powFactor: 2)),
              SizedBox(height: 20),
              Text('Tan'),
              // CustomPaint(
              //   size: Size(double.infinity, 300),
              //   painter:  CurvePainter(curve: Curve.tan),),
            ],
          ),
        )),
    ),);
  }
}