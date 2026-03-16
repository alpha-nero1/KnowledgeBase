import 'package:advanced_basics/data/questions.dart';
import 'package:advanced_basics/widgets/answers_summary.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

/// Shows the results of the screen.
///
class ResultsScreen extends StatelessWidget {
  final List<String> answers;
  final void Function() restartQuiz;
  const ResultsScreen({super.key, required this.answers, required this.restartQuiz });

  List<Map<String, Object>> summariseResults() {
    final List<Map<String, Object>> summary = [];

    for (var i = 0; i < answers.length; i++) {
      summary.add({
        'index': i,
        'userAnswer': answers[i],
        'correctAnswer': questions[i].answers[0],
        'question': questions[i].text,
        'isCorrect': answers[i] == questions[i].answers[0]
      });
    }

    return summary;
  }

  /// example of a getter!
  List<Map<String, Object>> get resultsSummary {
    return summariseResults(); 
  }

  @override
  Widget build(BuildContext context) {
    final countCorrect = resultsSummary.where((s) => s['isCorrect'] as bool).length;

    return SizedBox(
      width: double.infinity,
      child: Container(
        margin: const EdgeInsets.all(40),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              'You answered $countCorrect out of ${resultsSummary.length} questions correctly!',
              style: GoogleFonts.lato(fontSize: 24, color: Colors.white, fontWeight: FontWeight.bold)
            ),
            const SizedBox(height: 30),
            AnswersSummary(summary: resultsSummary),
            const SizedBox(height: 30),
            TextButton.icon(
              onPressed: restartQuiz,
              style: TextButton.styleFrom(
                foregroundColor: Colors.white
              ),
              icon: const Icon(Icons.refresh),
              label: const Text('Start again')
            )
          ],
        ),
      ),
    );
  }
}
