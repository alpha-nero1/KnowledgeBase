import 'dart:io';

import 'package:favourite_places/models/place.dart';
import 'package:favourite_places/models/place_location.dart';
import 'package:favourite_places/providers/user_places.dart';
import 'package:favourite_places/widgets/image_input.dart';
import 'package:favourite_places/widgets/location_input.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class AddPlaceScreen extends ConsumerStatefulWidget {
  const AddPlaceScreen({super.key});

  @override
  ConsumerState<AddPlaceScreen> createState() => _State();
}

class _State extends ConsumerState<AddPlaceScreen> {
  final titleController = TextEditingController();
  File? _selectedImage;
  PlaceLocation? _placeLocation;

  void _saveOnPress() {
    final title = titleController.text;
    if (title.isEmpty || _placeLocation == null) return;
    final newPlace = Place(title: title, image: _selectedImage, location: _placeLocation);
    ref.read(userPlacesNotifier.notifier).addPlace(newPlace);
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Add new Place'),
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(12),
        child: Column(
          children: [
            TextField(
              decoration: const InputDecoration(labelText: 'Title'),
              controller: titleController,
              style: TextStyle(
                color: Theme.of(context).colorScheme.onSurface,
              ),
            ),
            const SizedBox(height: 16),
            ImageInput(onPickImage: (image) => _selectedImage = image),
            const SizedBox(height: 16),
            LocationInput(locationOnSelect: (location) => _placeLocation = location,),
            const SizedBox(height: 16),
            ElevatedButton.icon(
              onPressed: _saveOnPress,
              icon: const Icon(Icons.add),
              label: const Text('Add Place'),
            ),
          ],
        ),
      ),
    );
  }

  @override
  void dispose() {
    super.dispose();
    titleController.dispose();
  }
}