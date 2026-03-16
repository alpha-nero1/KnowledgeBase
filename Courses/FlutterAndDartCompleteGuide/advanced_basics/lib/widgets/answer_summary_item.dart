import 'package:advanced_basics/widgets/answer_identifier.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class AnswerSummaryItem extends StatelessWidget {
  final Map<String, Object> sum;
  const AnswerSummaryItem({super.key, required this.sum});

  @override
  Widget build(BuildContext context) {
    return Row(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // int is the integer datatype.
        AnswerIdentifier(id:(sum['index'] as int) + 1, isSuccess: sum['isCorrect'] as bool),
        // Expanded allows the child to not overflow,
        // basically allowing wrap.
        const SizedBox(width: 10),
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(sum['question'] as String, style: GoogleFonts.lato(color: Colors.white, fontSize: 16)),
              const SizedBox(height: 5),
              Text(sum['userAnswer'] as String, style: GoogleFonts.lato(color: Color.fromARGB(255, 202, 171, 252))),
              const SizedBox(height: 5),
              Text(sum['correctAnswer'] as String, style: GoogleFonts.lato(color: Color.fromARGB(255, 181, 254, 246))),
            ],
          ),
        ),
      ],
    );
  }

}