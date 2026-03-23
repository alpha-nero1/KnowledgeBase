import 'package:expense_tracker/models/expense.dart';
import 'package:expense_tracker/models/expense_bucket.dart';
import 'package:expense_tracker/widgets/chart/chart.dart';
import 'package:expense_tracker/widgets/expense_list/expense_list.dart';
import 'package:expense_tracker/widgets/new_expense/new_expense_bottom_sheet.dart';
import 'package:flutter/material.dart';

/// Main expenses app entry point.
///
class ExpensesApp extends StatefulWidget {
  const ExpensesApp({super.key});

  @override
  State<ExpensesApp> createState() {
    return _ExpensesAppState();
  }
}

class _ExpensesAppState extends State<ExpensesApp> {
  final List<Expense> _expenses = [
    Expense(
      title: 'Chicken burger',
      amount: 19.99,
      date: DateTime.now(),
      category: Category.leisure,
    ),
    Expense(
      title: 'Flight to Paris',
      amount: 320.00,
      date: DateTime(2026, 3, 1),
      category: Category.travel,
    ),
    Expense(
      title: 'Grocery shopping',
      amount: 54.75,
      date: DateTime(2026, 3, 2),
      category: Category.food,
    ),
    Expense(
      title: 'Netflix subscription',
      amount: 15.99,
      date: DateTime(2026, 3, 3),
      category: Category.leisure,
    ),
    Expense(
      title: 'Office supplies',
      amount: 42.50,
      date: DateTime(2026, 3, 4),
      category: Category.work,
    ),
    Expense(
      title: 'Pizza night',
      amount: 28.00,
      date: DateTime(2026, 3, 5),
      category: Category.food,
    ),
    Expense(
      title: 'Train tickets',
      amount: 75.00,
      date: DateTime(2026, 3, 6),
      category: Category.travel,
    ),
    Expense(
      title: 'Cinema tickets',
      amount: 22.50,
      date: DateTime(2026, 3, 7),
      category: Category.leisure,
    ),
    Expense(
      title: 'Laptop stand',
      amount: 39.99,
      date: DateTime(2026, 3, 8),
      category: Category.work,
    ),
    Expense(
      title: 'Sushi dinner',
      amount: 65.00,
      date: DateTime(2026, 3, 9),
      category: Category.food,
    ),
    Expense(
      title: 'Hotel stay',
      amount: 210.00,
      date: DateTime(2026, 3, 10),
      category: Category.travel,
    ),
    Expense(
      title: 'Concert tickets',
      amount: 85.00,
      date: DateTime(2026, 3, 11),
      category: Category.leisure,
    ),
    Expense(
      title: 'Printing costs',
      amount: 12.00,
      date: DateTime(2026, 3, 12),
      category: Category.work,
    ),
    Expense(
      title: 'Breakfast café',
      amount: 9.50,
      date: DateTime(2026, 3, 13),
      category: Category.food,
    ),
    Expense(
      title: 'Car rental',
      amount: 130.00,
      date: DateTime(2026, 3, 14),
      category: Category.travel,
    ),
    Expense(
      title: 'Video game',
      amount: 59.99,
      date: DateTime(2026, 3, 15),
      category: Category.leisure,
    ),
    Expense(
      title: 'Conference fee',
      amount: 250.00,
      date: DateTime(2026, 3, 16),
      category: Category.work,
    ),
    Expense(
      title: 'Tacos lunch',
      amount: 14.99,
      date: DateTime(2026, 3, 17),
      category: Category.food,
    ),
    Expense(
      title: 'Bus pass',
      amount: 30.00,
      date: DateTime(2026, 3, 18),
      category: Category.travel,
    ),
    Expense(
      title: 'Desk lamp',
      amount: 34.99,
      date: DateTime(2026, 3, 19),
      category: Category.work,
    ),
  ];

  void _expenseOnAdd(Expense expense) {
    setState(() {
      _expenses.add(expense);
    });
  }

  void _expenseOnRemove(Expense expense) {
    final expenseIndex = _expenses.indexOf(expense);
    setState(() {
      _expenses.remove(expense);
    });
    // Clear any messages before showing new.
    ScaffoldMessenger.of(context).clearSnackBars();
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        duration: Duration(seconds: 3),
        action: SnackBarAction(
          label: 'Undo',
          onPressed: () => setState(() {
            // Use insert to insert at an index position, effectively undoing our action.
            _expenses.insert(expenseIndex, expense);
          }),
        ),
        content: Text('${expense.title} expense removed'),
      ),
    );
  }

  /// Open a bottom sheet to add an expense to the app
  ///
  void _addExpenseOnPressed() {
    // Can get access to the 'context' param if you subclass StatefulWidget!
    showModalBottomSheet(
      isScrollControlled: true,
      // Bottom sheet is not in Scaffold so for the modal
      // overlay you need to pass in useSafeArea to avoid
      // content showing underneath the camera.
      useSafeArea: true,
      context: context,
      builder: (ctx) => NewExpenseBottomSheet(_expenseOnAdd),
    );
  }

  /// Getter for the main content.
  ///
  Widget getMainContent() {
    if (_expenses.isEmpty) {
      return const Center(child: Text('No expenses found, start adding some'));
    }
    return ExpenseList(expenses: _expenses, expenseOnRemove: _expenseOnRemove);
  }

  @override
  Widget build(BuildContext context) {
    final width = MediaQuery.of(context).size.width;
    final isLandscape = width > 600;

    // Expanded makes sure that the children take up only
    // the available space and not infinity.
    // Expanded helps put into effect size contraints,
    // say incase the parent is Row and the child has
    // width = infinity.
    final bodyItems = [
      Expanded(child: Chart(expenses: _expenses)),
      Expanded(child: getMainContent()),
    ];

    // Scaffold does some base style work.
    return Scaffold(
      appBar: AppBar(
        title: const Text('Expense Tracker'),
        // List of widgets like buttons
        actions: [
          IconButton(
            onPressed: _addExpenseOnPressed,
            icon: const Icon(Icons.add),
          ),
        ],
      ),
      body: isLandscape 
      ? Row(children: bodyItems)
      : Column(children: bodyItems),
    );
  }
}
