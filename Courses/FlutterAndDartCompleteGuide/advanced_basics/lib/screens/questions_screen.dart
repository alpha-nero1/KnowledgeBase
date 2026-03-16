import 'package:advanced_basics/widgets/answer_button.dart';
import 'package:flutter/material.dart';
import 'package:advanced_basics/data/questions.dart';
import 'package:google_fonts/google_fonts.dart';

/// Screen Widget displaying a series of questions.
/// 
class QuestionsScreen extends StatefulWidget {
  // Syntax for describing a function type.
  final void Function(String answer) answerOnSelect;
  const QuestionsScreen({super.key, required this.answerOnSelect });

  @override
  State<StatefulWidget> createState() {
    return _QuestionScreenState();
  }
}

class _QuestionScreenState extends State<QuestionsScreen> {
  var questionIndex = 0;

  void answerOnPress(String answer) {
    var question = questions[questionIndex];
    var isCorrect = answer == question.answers[0];
    var newIndex = questionIndex + 1;
    // Widget keyword!
    widget.answerOnSelect(answer);

    if (newIndex < questions.length) {
      setState(() {
        questionIndex = newIndex;
      });
    } else {
      // Finish the test!
    }
  }

  @override
  Widget build(context) {
    final currentQuestion = questions[questionIndex];

    return SizedBox(
      width: double.infinity,
      child: Container(
        margin: EdgeInsets.all(40),
        child: Column(
          // justifyContent
          mainAxisAlignment: MainAxisAlignment.center,
          // alignItems
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Text(
              currentQuestion.text,
              textAlign: TextAlign.center,
              style: GoogleFonts.lato(color: const Color.fromARGB(255, 168, 135, 245), fontSize: 24, fontWeight: FontWeight.bold)
            ),
            const SizedBox(height: 30),
            // Map!!
            ...currentQuestion
            // I don't actually like this, this is a display focused method,
            // that logic should actually be in this file.
            .shuffled
            .map(
              (answer) => AnswerButton(onPress: () => answerOnPress(answer), text: answer),
            ),
          ],
        ),
      ),
    );
  }
}
