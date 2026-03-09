class Question {
  final String text;
  final List<String> answers;

  const Question(this.text, this.answers);

  // shuffle mutates the original list so we must make a copy.
  List<String> getShuffledAnswers() {
    final copy = List.of(answers);
    copy.shuffle();
    return copy;
  }
}