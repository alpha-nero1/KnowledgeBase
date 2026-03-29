import 'package:favourite_places/providers/user_places.dart';
import 'package:favourite_places/screens/add_place.dart';
import 'package:favourite_places/widgets/places_list.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class PlacesScreen extends ConsumerStatefulWidget {
  const PlacesScreen({super.key});

  void _addPlaceOnPress(BuildContext context) {
    Navigator.of(
      context,
    ).push(MaterialPageRoute(builder: (ctx) => const AddPlaceScreen()));
  }

  @override
  ConsumerState<ConsumerStatefulWidget> createState() => _State();
}

class _State extends ConsumerState<PlacesScreen> {
  late Future _placesFuture;

  @override
  void initState() {
    super.initState();
    _placesFuture = ref.read(userPlacesNotifier.notifier).loadPlaces();
  }

  @override
  Widget build(BuildContext context) {
    final places = ref.watch(userPlacesNotifier);
    return Scaffold(
      appBar: AppBar(
        title: const Text('Your Places'),
        actions: [
          IconButton(
            icon: const Icon(Icons.add),
            onPressed: () => widget._addPlaceOnPress(context),
          ),
        ],
      ),
      body: FutureBuilder(
        future: _placesFuture,
        builder: (ctx, snapshot) =>
            snapshot.connectionState == ConnectionState.waiting
            ? const Center(child: CircularProgressIndicator())
            : PlacesList(places: places),
      ),
    );
  }
}
