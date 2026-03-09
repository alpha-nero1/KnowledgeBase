import 'package:advanced_basics/questions_screen.dart';
import 'package:advanced_basics/start_screen.dart';
import 'package:flutter/material.dart';

class Quiz extends StatefulWidget {
  const Quiz({super.key});

  @override
  State<StatefulWidget> createState() {
    return _QuizState();
  }
}

class _QuizState extends State<Quiz> {
  var activeScreen = 'start-screen';

  // Called right after object was created. Post creation job.
  // @override
  // void initState() {
  //   // Can now reference instance members.
  //   activeScreen = StartScreen(startQuiz: navigate);
  //   // Lastly continue initState on the parent.
  //   super.initState();
  // }

  void navigate() {
    setState(() {
      activeScreen = 'questions-screen';
    });
  }

  @override
  Widget build(context) {
    return MaterialApp(
      home: Scaffold(
        body: Container(
          decoration: const BoxDecoration(
            gradient: LinearGradient(
              colors: [Colors.deepPurple, Colors.deepPurpleAccent],
              begin: AlignmentGeometry.topLeft,
              end: AlignmentGeometry.bottomRight,
            ),
          ),
          child: (
            activeScreen == 'start-screen'
            ? StartScreen(startQuiz: navigate)
            : const QuestionsScreen()
          ),
        ),
      ),
    );
  }
}
