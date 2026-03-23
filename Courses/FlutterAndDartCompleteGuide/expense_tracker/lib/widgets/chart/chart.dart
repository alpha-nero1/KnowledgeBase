import 'package:expense_tracker/models/expense_bucket.dart';
import 'package:flutter/material.dart';

import 'package:expense_tracker/widgets/chart/chart_bar.dart';
import 'package:expense_tracker/models/expense.dart';

class Chart extends StatelessWidget {
  const Chart({super.key, required this.expenses});

  final List<Expense> expenses;

  List<ExpenseBucket> getExpenseBuckets() {
    final expenseDict = <Category, List<Expense>>{};
    for (final expense in expenses) {
      if (!expenseDict.containsKey(expense.category)) {
        expenseDict[expense.category] = [];
      }
      expenseDict[expense.category]!.add(expense);
    }

    final buckets = expenseDict.entries
        .map((entry) => ExpenseBucket(category: entry.key, expenses: entry.value))
        .toList();

    // We gotta sort so the column render order is not different on each rerender.
    buckets.sort((a, b) => a.category.name.compareTo(b.category.name));
    return buckets;
  }

  double get maxTotalExpense {
    double maxTotalExpense = 0;

    for (final bucket in getExpenseBuckets()) {
      if (bucket.total > maxTotalExpense) {
        maxTotalExpense = bucket.total;
      }
    }

    return maxTotalExpense;
  }

  @override
  Widget build(BuildContext context) {
    // Detect the theme mode.
    final isDarkMode =
        MediaQuery.of(context).platformBrightness == Brightness.dark;
    return Container(
      margin: const EdgeInsets.all(16),
      padding: const EdgeInsets.symmetric(
        vertical: 16,
        horizontal: 8,
      ),
      width: double.infinity,
      height: 180,
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(8),
        gradient: LinearGradient(
          colors: [
            Theme.of(context).colorScheme.primary.withAlpha(90),
            Theme.of(context).colorScheme.primary.withAlpha(0)
          ],
          begin: Alignment.bottomCenter,
          end: Alignment.topCenter,
        ),
      ),
      child: Column(
        children: [
          Expanded(
            child: Row(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                // Alternative to map!!
                for (final bucket in getExpenseBuckets()) // alternative to map()
                  ChartBar(
                    fill: bucket.total == 0
                        ? 0
                        : bucket.total / maxTotalExpense,
                  )
              ],
            ),
          ),
          const SizedBox(height: 12),
          Row(
            children: getExpenseBuckets()
                .map(
                  (bucket) => Expanded(
                    child: Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 4),
                      child: Icon(
                        categoryIcons[bucket.category],
                        color: isDarkMode
                            ? Theme.of(context).colorScheme.secondary
                            : Theme.of(context)
                                .colorScheme
                                .primary
                                .withAlpha(200),
                      ),
                    ),
                  ),
                )
                .toList(),
          )
        ],
      ),
    );
  }
}