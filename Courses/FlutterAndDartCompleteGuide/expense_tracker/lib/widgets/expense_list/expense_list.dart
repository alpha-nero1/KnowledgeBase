import 'package:expense_tracker/models/expense.dart';
import 'package:expense_tracker/widgets/expense_list/expense_item.dart';
import 'package:flutter/material.dart';

/// Display the list of expenses that the user has.
///
class ExpenseList extends StatelessWidget {
  final List<Expense> _expenses;
  final Function(Expense expense) _expenseOnRemove;

  const ExpenseList({
    super.key,
    required List<Expense> expenses,
    required Function(Expense expense) expenseOnRemove,
  }) : _expenses = expenses,
       _expenseOnRemove = expenseOnRemove;

  @override
  Widget build(BuildContext context) {
    // Use a list for building a long list, this is memory efficient.
    return ListView.builder(
      itemCount: _expenses.length,
      // Use dismissable for swipe dismissal.
      itemBuilder: (ctx, index) => Dismissible(
        // Use value key, it can take objects as key value!
        key: ValueKey(_expenses[index]),
        onDismissed: (direction) {
          _expenseOnRemove(_expenses[index]);
        },
        background: Container(
          color: Theme.of(context).colorScheme.error.withAlpha(90),
          margin: EdgeInsets.symmetric(horizontal: Theme.of(context).cardTheme.margin?.horizontal ?? 0),
        ),
        child: ExpenseItem(expense: _expenses[index]),
      ),
    );
  }
}
