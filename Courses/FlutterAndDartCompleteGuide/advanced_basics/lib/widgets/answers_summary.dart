import 'package:advanced_basics/widgets/answer_identifier.dart';
import 'package:advanced_basics/widgets/answer_summary_item.dart';
import 'package:flutter/material.dart';

class AnswersSummary extends StatelessWidget {
  final List<Map<String, Object>> summary;

  const AnswersSummary({super.key, required this.summary});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 400,
      child: SingleChildScrollView(
        child: Column(
          children: summary
              .map(
                (sum) => AnswerSummaryItem(sum: sum),
              )
              .toList(),
        ),
      ),
    ); 
  }
}
