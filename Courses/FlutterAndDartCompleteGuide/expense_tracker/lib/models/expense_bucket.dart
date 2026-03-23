import 'package:expense_tracker/models/expense.dart';

class ExpenseBucket {
  final Category category;
  final List<Expense> expenses;

  const ExpenseBucket({ required this.category, required this.expenses });

  double get total => expenses.fold(0.0, (sum, expense) => sum + expense.amount);
}