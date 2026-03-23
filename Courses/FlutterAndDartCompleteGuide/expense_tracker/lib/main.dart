import 'package:expense_tracker/expenses_app.dart';
import 'package:expense_tracker/theme.dart';
import 'package:flutter/material.dart';
// import 'package:flutter/services.dart';

void main() {
  // TO FORCE PORTRAIT - MY PREFERED!
  // WidgetsFlutterBinding.ensureInitialized();
  // SystemChrome.setPreferredOrientations([
  //   DeviceOrientation.portraitUp
  // ]).then((fn) {
  //   runApp(
  //     MaterialApp(
  //       // Copy with means you do not need to set up the theme from scratch.
  //       theme: gTheme,
  //       darkTheme: gThemeDark,
  //       home: ExpensesApp(),
  //       themeMode: ThemeMode.dark,
  //     ),
  //   );
  // });
  runApp(
    MaterialApp(
      // Copy with means you do not need to set up the theme from scratch.
      theme: gTheme,
      darkTheme: gThemeDark,
      home: ExpensesApp(),
      themeMode: ThemeMode.dark,
    ),
  );
}
