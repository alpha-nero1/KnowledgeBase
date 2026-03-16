import 'package:flutter/material.dart';

/// The coloured numbered id showing next to the answer summary set.
///
class AnswerIdentifier extends StatelessWidget {
  final int id;
  final bool isSuccess;
  const AnswerIdentifier({super.key, required this.id, required this.isSuccess});

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 30,
      height: 30,
      alignment: Alignment.center,
      decoration: BoxDecoration(
        color: isSuccess 
          ? const Color.fromARGB(255, 150, 198, 241)
          : const Color.fromARGB(255, 249, 133, 241),
        borderRadius: BorderRadius.circular(100)

      ),
      child: Text(
        style: TextStyle(
          fontWeight: FontWeight.bold,
          color: Color.fromARGB(255, 22, 2, 56)
        ),
        id.toString()
      )
      );
  }

}