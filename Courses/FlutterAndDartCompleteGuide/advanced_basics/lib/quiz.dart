import 'package:advanced_basics/data/questions.dart';
import 'package:advanced_basics/screens/questions_screen.dart';
import 'package:advanced_basics/screens/results_screen.dart';
import 'package:advanced_basics/screens/start_screen.dart';
import 'package:flutter/material.dart';

/// Basically this is the main App class.
///
class Quiz extends StatefulWidget {
  const Quiz({super.key});

  @override
  State<StatefulWidget> createState() {
    return _QuizState();
  }
}

/// Leading _ makes this class private to the file and not be exposed.
/// can do the same to properties like _selectedAnswers
///
class _QuizState extends State<Quiz> {
  var activeScreen = 'start-screen';
  List<String> _selectedAnswers = [];

  // Called right after object was created. Post creation job.
  // @override
  // void initState() {
  //   // Can now reference instance members.
  //   activeScreen = StartScreen(startQuiz: navigate);
  //   // Lastly continue initState on the parent.
  //   super.initState();
  // }

  void answerOnSelect(String answer) {
    _selectedAnswers.add(answer);

    // Reset once we reach the end.
    print('Results ${_selectedAnswers.length} ${questions.length}');
    if (_selectedAnswers.length == questions.length) {
      setState(() {
        activeScreen = 'results-screen';
      });
    }
  }

  void restartQuiz() {
    setState(() {
      _selectedAnswers = [];
      activeScreen = 'start-screen';
    });
  }

  void navigate() {
    setState(() {
      activeScreen = 'questions-screen';
    });
  }

  Widget getActiveScreen() {
    if (activeScreen == 'results-screen') {
      return ResultsScreen(answers: _selectedAnswers, restartQuiz: restartQuiz);
    }
    if (activeScreen == 'questions-screen') {
      return QuestionsScreen(answerOnSelect: answerOnSelect);
    }
    return StartScreen(startQuiz: navigate);
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
          child: getActiveScreen(),
        ),
      ),
    );
  }
}
