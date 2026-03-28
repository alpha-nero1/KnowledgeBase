import 'package:flutter/material.dart';
import 'package:shopping_list/data/categories.dart';
import 'package:shopping_list/model/category.dart';
import 'package:shopping_list/model/grocery_item.dart';

class NewItem extends StatefulWidget {
  const NewItem({super.key});

  @override
  State<NewItem> createState() {
    return _NewItemState();
  }
}

class _NewItemState extends State<NewItem> {
  /// Form key ensures that Form maintains its INTERNAL STATE through reloads!
  final _formKey = GlobalKey<FormState>();
  var _name = '';
  var _quantity = 1;
  var _selectedCategory = categories[Categories.vegetables]!;

  void _submit() {
    /// Force validation on submission!
    if (_formKey.currentState!.validate()) {
      // Lock it in eddy! Works because all the inputs have an onSaved callback defined.
      _formKey.currentState!.save();
      Navigator.of(context).pop(GroceryItem(id: id, name: name, quantity: quantity, category: category))
      print(_name);
      print(_quantity);
      print(_selectedCategory);
      Na
    }
  }

  void _reset() {
    /// Reset the form!
    _formKey.currentState!.reset();
  }

  /// Validator for the name field.
  String? validateName(String? value) {
    if (value == null ||
      value.isEmpty ||
      value.trim().length <= 1 ||
      value.trim().length > 50
    ) {
      return 'Must be between 1 and 50 characters.';
    }
    return null;
  }

  String? validateQuantity(String? value) {
    if (value == null ||
      value.isEmpty ||
      int.tryParse(value) == null ||
      int.tryParse(value)! <= 0
    ) {
      return 'Must be a valid, positive number.';
    }
    return null;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Add a new item')),
      body: Padding(
        padding: const EdgeInsets.all(12),
        child: Form(
          key: _formKey,
          child: Column(
            children: [
              // TextFormField instead of simple TextField for forms!
              TextFormField(
                maxLength: 50,
                initialValue: _name,
                decoration: const InputDecoration(label: Text('Name')),
                validator: validateName,
                onSaved: (value) => _name = value!,
              ),
              Row(
                crossAxisAlignment: CrossAxisAlignment.end,
                children: [
                  Expanded(
                    child: TextFormField(
                      initialValue: _quantity.toString(),
                      validator: validateQuantity,
                      keyboardType: TextInputType.number,
                      decoration: const InputDecoration(
                        label: Text('Quantity'),
                      ),
                      onSaved: (value) => _quantity = int.parse(value!)
                    ),
                  ),
                  const SizedBox(width: 8),
                  Expanded(
                    child: DropdownButtonFormField(
                      initialValue: _selectedCategory,
                      items: [
                        for (final category in categories.entries)
                          DropdownMenuItem(
                            value: category.value,
                            child: Row(
                              children: [
                                Container(
                                  width: 16,
                                  height: 16,
                                  color: category.value.color,
                                ),
                                const SizedBox(width: 8),
                                Text(category.value.title),
                              ],
                            ),
                          ),
                      ],
                      onChanged: (value) {
                        // Drop down is a little different it requires state change.
                        setState(() {
                          _selectedCategory = value!;
                        });
                      },
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 16),
              Row(
                mainAxisAlignment: MainAxisAlignment.end,
                // Submission buttons!
                children: [
                  TextButton(onPressed: _reset, child: const Text('Reset')),
                  ElevatedButton(onPressed: _submit, child: const Text('Submit')),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}
