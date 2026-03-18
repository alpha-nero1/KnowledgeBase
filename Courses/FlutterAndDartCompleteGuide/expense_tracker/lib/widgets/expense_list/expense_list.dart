import 'package:expense_tracker/models/expense.dart';
import 'package:expense_tracker/widgets/expense_list/expense_item.dart';
import 'package:flutter/material.dart';

/// Display the list of expenses that the user has.
///
class ExpenseList extends StatelessWidget {
  final List<Expense> _expenses;

  const ExpenseList({super.key, required List<Expense> expenses})
      : _expenses = expenses;

  @override
  Widget build(BuildContext context) {
    // Use a list for building a long list, this is memory efficient.
    return ListView.builder(
      itemCount: _expenses.length,
      itemBuilder: (ctx, index) => ExpenseItem(
        expense: _expenses[index]
      )
    );
  }
}