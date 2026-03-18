import 'package:expense_tracker/models/expense.dart';
import 'package:flutter/material.dart';

class ExpenseItem extends StatelessWidget {
  final Expense _expense;

  const ExpenseItem({super.key, required Expense expense}) : _expense = expense;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 16),
        child: Column(children: [
          Text(_expense.title),
          SizedBox(height: 5),
          Row(children: [
            Text('\$${_expense.amount.toStringAsFixed(2)}'),
            // Take up the most width possible.
            const Spacer(),
            Row(children: [
              Icon(categoryIcons[_expense.category]),
              const SizedBox(width: 8),
              Text(_expense.formattedDate)
            ],)
          ],)
        ]),
      ),
    );
  }
}
