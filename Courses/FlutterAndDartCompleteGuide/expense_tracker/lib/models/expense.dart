import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:uuid/uuid.dart';

const uuid = Uuid();
// Date format library!
final formatter = DateFormat('dd/MM/yyyy');

enum Category {
  food,
  travel,
  leisure,
  work
}

const categoryIcons = {
  Category.food: Icons.lunch_dining,
  Category.travel: Icons.flight_takeoff,
  Category.leisure: Icons.movie,
  Category.work: Icons.work,
};

/// Represents a logical Expense model object.
///
class Expense {
  final String id = uuid.v4();
  final String title;
  final double amount;
  final DateTime date;
  final Category category;

  Expense({ 
    required this.title, 
    required this.amount,
    required this.date,
    required this.category
  });

  String get formattedDate {
    return formatter.format(date);
  }
}