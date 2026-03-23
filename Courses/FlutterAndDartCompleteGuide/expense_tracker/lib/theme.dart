import 'package:flutter/material.dart';

final gColourScheme = ColorScheme.fromSeed(
  seedColor: const Color.fromARGB(255, 96, 59, 181),
);

final gTheme = ThemeData().copyWith(
  colorScheme: gColourScheme,
  appBarTheme: const AppBarTheme().copyWith(
    backgroundColor: gColourScheme.onPrimaryContainer,
    foregroundColor: gColourScheme.primaryContainer,
  ),
  cardTheme: const CardThemeData().copyWith(
    color: gColourScheme.secondaryContainer,
    margin: EdgeInsets.symmetric(horizontal: 16, vertical: 8),
  ),
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      backgroundColor: gColourScheme.primaryContainer,
    ),
  ),
  textTheme: ThemeData().textTheme.copyWith(
    titleLarge: TextStyle(
      fontWeight: FontWeight.normal,
      color: gColourScheme.onSecondaryContainer,
      fontSize: 14
    ),
  ),
);

final gColourSchemeDark = ColorScheme.fromSeed(
  brightness: Brightness.dark,
  seedColor: const Color.fromARGB(255, 5, 99, 125),
);

final gThemeDark = ThemeData.dark().copyWith(
  colorScheme: gColourSchemeDark,
  cardTheme: const CardThemeData().copyWith(
    color: gColourSchemeDark.secondaryContainer,
    margin: EdgeInsets.symmetric(horizontal: 16, vertical: 8),
  ),
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      backgroundColor: gColourSchemeDark.primaryContainer,
      foregroundColor: gColourSchemeDark.onPrimaryContainer
    ),
  ),
);