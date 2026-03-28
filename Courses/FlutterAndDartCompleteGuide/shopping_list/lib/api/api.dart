import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:shopping_list/data/categories.dart';
import 'package:shopping_list/model/grocery_item.dart';

const location = 'shoping-list.json';
const firebaseDb = 'shopping-list-55669.firebaseio.com';

Future<List<GroceryItem>> getList() async {
  final url = Uri.https(firebaseDb, location);
  final res = await http.get(url);
  if (res.statusCode >= 400) {
    throw Exception('Failed to fetch data. Please try again later. Status code ${res.statusCode}');
  }
  final Map<String, dynamic> listData = json.decode(res.body);
  final List<GroceryItem> data = [];
  for (final item in listData.entries) {
    final category = categories.entries
      .firstWhere((catItem) => catItem.value.title == item.value['category'])
      .value;
    data.add(GroceryItem(
      id: item.key, 
      name: item.value['name'], 
      quantity: item.value['quantity'], 
      category: category));
  }

  return data;
}

Future<GroceryItem> saveItem(Map<String, dynamic> data) async {
  final url = Uri.https(firebaseDb, location);
  // Post will push an item to this json file.
  final res = await http.post(
    url,
    headers: {
      'Content-Type': 'application/json'
    },
    body: json.encode(data)
  );
  if (res.statusCode >= 400) {
    throw Exception('Failed to save data. Please try again later. Status code ${res.statusCode}');
  }
  final Map<String, dynamic> resData = json.decode(res.body);
  final category = categories.entries
      .firstWhere((catItem) => catItem.value.title == resData['category'])
      .value;
  return GroceryItem(
    id: resData['name'], 
    name: resData['name'], 
    quantity: resData['quantity'], 
    category: category);
}


Future deleteItem(String itemId) async {
  final url = Uri.https(firebaseDb, 'shopping-list/$itemId.json');
  final res = await http.delete(url);
  if (res.statusCode >= 400) {
    throw Exception('Failed to delete data. Please try again later. Status code ${res.statusCode}');
  }
}
