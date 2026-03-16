import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class StartScreen extends StatelessWidget {
  const StartScreen({super.key, required this.startQuiz});

  final void Function() startQuiz;

  void startOnPressed() => startQuiz();

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          // Set colour on asset instead:
          Image.asset(
            'assets/images/quiz-logo.png',
            width: 300,
            color: Color.fromARGB(150, 255, 255, 255),
          ),
          // Compute intensive approach:
          // Opacity(opacity: 0.5, child: Image.asset('assets/images/quiz-logo.png', width: 300)),
          const SizedBox(height: 30),
          Text(
            'Learn Flutter the Fun Way',
            style: GoogleFonts.lato(fontSize: 24, color: Colors.white),
          ),
          const SizedBox(height: 30),
          // Icon button!! OutlinedButton.icon + Icons.arrow_right_alt
          OutlinedButton.icon(
            style: OutlinedButton.styleFrom(foregroundColor: Colors.white),
            onPressed: startOnPressed,
            icon: Icon(Icons.arrow_right_alt),
            label: Text('Start Quiz'),
          ),
        ],
      ),
    );
  }
}
