import 'package:favourite_places/providers/user_places.dart';
import 'package:favourite_places/screens/add_place.dart';
import 'package:favourite_places/widgets/places_list.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class PlacesScreen extends ConsumerWidget {
  const PlacesScreen({super.key});

  void _addPlaceOnPress(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (ctx) => const AddPlaceScreen()));
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final places = ref.watch(userPlacesNotifier);
    return Scaffold(
      appBar: AppBar(
        title: const Text('Your Places'),
        actions: [
          IconButton(
            icon: const Icon(Icons.add),
            onPressed: () => _addPlaceOnPress(context),
          ),
        ],
      ),
      body: PlacesList(
        places: places,
      ),
    );
  }
}