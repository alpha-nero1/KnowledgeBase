import 'dart:io';

import 'package:expense_tracker/models/expense.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

final formatter = DateFormat('dd/MM/yyyy');

class NewExpenseBottomSheet extends StatefulWidget {
  final void Function(Expense expense) expenseOnAdd;

  const NewExpenseBottomSheet(this.expenseOnAdd, {super.key});

  @override
  State<StatefulWidget> createState() {
    return _NewExpenseState();
  }
}

class _NewExpenseState extends State<NewExpenseBottomSheet> {
  // Use TextEditingController() for storing value.
  // could use onChange too.
  // TextEditingController() is disposable, must dispose.
  final _titleController = TextEditingController();
  final _amountController = TextEditingController();
  DateTime? _selectedDate;
  Category? _selectedCategory;

  void cancelOnPressed() {
    // Removes this overlay from the screen.
    Navigator.pop(context);
  }

  void dateOnPressed() async {
    final now = DateTime.now();
    final newDate = await showDatePicker(
      context: context,
      firstDate: DateTime(now.year - 1, now.month, now.day),
      lastDate: now,
    );
    setState(() {
      _selectedDate = newDate;
    });
  }

  void showValidationDialog(String title) {
    // Platform.isIOS for OS specific code.
    if (Platform.isIOS) {
      // Cupertino = iOS look.
      showCupertinoDialog(context: context, builder: (ctx) => CupertinoAlertDialog(
        title: Text('Invalid $title'),
        content: Text('Please make sure you enter a valid $title'),
        actions: [
          TextButton(
            onPressed: () {
              Navigator.pop(context);
            }, 
            child: const Text('Okay')
          )
        ],
      ));
      return;
    }
    showDialog(context: context, builder: (ctx) => AlertDialog(
      title: Text('Invalid $title'),
      content: Text('Please make sure you enter a valid $title'),
      actions: [
        TextButton(
          onPressed: () {
            Navigator.pop(context);
          }, 
          child: const Text('Okay')
        )
      ],
    ));
  }

  void submitOnPressed() {
    if (_titleController.text.trim().isEmpty) {
      showValidationDialog('Expense title');
      return;
    }
    final enteredAmount = double.tryParse(_amountController.text.trim());
    if (enteredAmount == null || enteredAmount < 0) {
      showValidationDialog('Amount');
      return;
    }
    if (_selectedDate == null) {
      showValidationDialog('Date');
      return;
    }
    if (_selectedCategory == null) {
      showValidationDialog('Category');
      return;
    }
    // Submit!
    widget.expenseOnAdd(Expense(
      title: _titleController.text, 
      amount: enteredAmount, 
      date: _selectedDate!, 
      category: _selectedCategory!
    ));
    // Close dialog
    Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    // Row inside a row MUST use Expanded wrapper.
    final keyboardHeight = MediaQuery.of(context).viewInsets.bottom;

    return LayoutBuilder(builder: (ctx, constraints) {
      final width = constraints.maxWidth;

      return SizedBox(
        height: double.infinity,
        child: SingleChildScrollView(
          child: Padding(
            padding: EdgeInsetsGeometry.fromLTRB(16, 32, 16, keyboardHeight + 16),
            child: Column(
              children: [
                if (width > 600)
                  Row(children: [
                    // Don't have to set .text as it is default this is just instructive.
                    Expanded(child: TextField(
                      maxLength: 50,
                      keyboardType: TextInputType.text,
                      decoration: const InputDecoration(label: Text('Expense')),
                      controller: _titleController,
                    )),
                    const SizedBox(width: 24),
                    Expanded(
                      child: TextField(
                        keyboardType: TextInputType.number,
                        decoration: const InputDecoration(
                          prefixText: '\$ ',
                          label: Text('Amount'),
                        ),
                        controller: _amountController,
                      ),
                    ),
                  ],)
                else
                  Column(children: [
                    TextField(
                      maxLength: 50,
                      keyboardType: TextInputType.text,
                      decoration: const InputDecoration(label: Text('Expense')),
                      controller: _titleController,
                    ),
                  ],),
                Row(
                  children: [
                    Expanded(
                      child: TextField(
                        keyboardType: TextInputType.number,
                        decoration: const InputDecoration(
                          prefixText: '\$ ',
                          label: Text('Amount'),
                        ),
                        controller: _amountController,
                      ),
                    ),
                    const SizedBox(width: 16),
                    Expanded(
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.end,
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          Text(
                            _selectedDate != null
                                ? formatter.format(_selectedDate!)
                                : 'Select Date',
                          ),
                          IconButton(
                            onPressed: dateOnPressed,
                            icon: Icon(Icons.date_range),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 16),
                Row(
                  children: [
                    DropdownButton(
                      value: _selectedCategory,
                      items: Category.values
                          .map(
                            (val) =>
                                DropdownMenuItem(value: val, child: Text(val.name.toUpperCase())),
                          )
                          .toList(),
                      onChanged: (value) {
                        if (value == null) {
                          return;
                        }
                        setState(() {
                          _selectedCategory = value;
                        });
                      },
                    ),
                    const Spacer(),
                    TextButton(onPressed: cancelOnPressed, child: Text('Cancel')),
                    ElevatedButton(
                      onPressed: submitOnPressed,
                      child: Text('Save'),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      );
    })
  }

  /// Disposal lifecycle function.
  ///
  @override
  void dispose() {
    _titleController.dispose();
    _amountController.dispose();
    super.dispose();
  }
}
