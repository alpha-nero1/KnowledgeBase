import 'dart:io';

import 'package:favourite_places/models/place_location.dart';
import 'package:uuid/uuid.dart';

const uuid = Uuid();

class Place {
  final String id;
  final String title;
  final PlaceLocation? location;
  File? image;

  Place({required this.title, this.image, this.location, String? id})
    : id = id ?? uuid.v4();
}
