import 'package:advanced_basics/widgets/answer_button.dart';
import 'package:flutter/material.dart';
import 'package:advanced_basics/data/questions.dart';

class QuestionsScreen extends StatefulWidget {
  const QuestionsScreen({super.key});

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
              style: TextStyle(color: Colors.white, fontSize: 18),
            ),
            const SizedBox(height: 30),
            // Map!!
            ...currentQuestion
            .getShuffledAnswers()
            .map(
              (answer) => AnswerButton(onPress: () => answerOnPress(answer), text: answer),
            ),
          ],
        ),
      ),
    );
  }
}
