import 'package:advanced_basics/models/question.dart';

const questions = [
  Question('What are the main building blocks of flutter UIs?', [
    'Widgets',
    'Components',
    'Blocks',
    'Functions'
  ]),
  Question('How are Flutter UIs built?', [
    'By combining widgets in code',
    'By compiling XML layouts',
    'By using only drag-and-drop editors',
    'By defining UI in CSS files'
  ]),
  Question('Which keyword is used to define an immutable variable in Dart?', [
    'final',
    'var',
    'dynamic',
    'mutable'
  ]),
  Question('Which widget should you use for layouts in a vertical direction?', [
    'Column',
    'Row',
    'Stack',
    'ListTile'
  ]),
  Question('What does setState() do in a StatefulWidget?', [
    'Triggers a rebuild with updated state',
    'Creates a new widget tree from scratch on disk',
    'Navigates to a different screen',
    'Disposes the current widget'
  ]),
  Question('Which command creates a new Flutter project?', [
    'flutter create',
    'flutter init',
    'dart new flutter',
    'flutter start'
  ])
];