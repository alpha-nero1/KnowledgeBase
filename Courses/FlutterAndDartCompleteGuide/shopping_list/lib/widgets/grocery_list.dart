import 'package:flutter/material.dart';
import 'package:shopping_list/api/api.dart';
import 'package:shopping_list/model/grocery_item.dart';
import 'package:shopping_list/widgets/new_item.dart';

class GroceryList extends StatefulWidget {
  const GroceryList({super.key});

  @override
  State<GroceryList> createState() => _State();
}

class _State extends State<GroceryList> {
  List<GroceryItem> _groceryItems = [];
  bool _isLoading = false;

  @override
  void initState() {
    super.initState();
    // Load data on init.
    load();
  }

  void _addItem() async {
    final newItem = await Navigator.of(context).push(
      MaterialPageRoute(
        builder: (ctx) => const NewItem(),
      ),
    );

    if (newItem == null) return;

    setState(() {
      // This is a grocery item with id from backend so all G!
      _groceryItems.add(newItem);
    });
  }

  void _removeItem(GroceryItem item) async {
    final index = _groceryItems.indexOf(item);
    try {
      await deleteItem(index.toString());
    } catch (e) {
      if (!context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(e.toString()),
        ),
      );
    } finally {
      setState(() {
        _groceryItems.remove(item);
      });
    }
  }

  void load() async {
    try {
      setState(() {
        _isLoading = true;
      });
      final data = await getList();
      setState(() {
        _groceryItems = data;
      });
    } catch (e) {
      final errorMessage = e.toString();
      if (!context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(errorMessage),
        ),
      );
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  Widget getContent() {
    if (_isLoading) {
      return const Center(child: CircularProgressIndicator());
    }
    if (_groceryItems.isEmpty) {
      return const Center(child: Text('No items added yet'));
    }
    return ListView.builder(
      itemCount: _groceryItems.length,
      itemBuilder: (ctx, index) => Dismissible(
        onDismissed: (direction) => _removeItem(_groceryItems[index]),
        key: ValueKey(_groceryItems[index].id),
        child: ListTile(
          title: Text(_groceryItems[index].name),
          leading: Container(
            width: 24,
            height: 24,
            color: _groceryItems[index].category.color,
          ),
          trailing: Text(
            _groceryItems[index].quantity.toString(),
          ),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Your Groceries'),
        actions: [
          IconButton(
            onPressed: _addItem,
            icon: const Icon(Icons.add),
          ),
        ],
      ),
      body: getContent(),
    );
  }
}