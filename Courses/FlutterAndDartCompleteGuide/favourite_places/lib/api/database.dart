import 'dart:io';

import 'package:favourite_places/models/place.dart';
import 'package:favourite_places/models/place_location.dart';
import 'package:sqflite/sqflite.dart' as sql;
import 'package:path/path.dart' as path;
import 'package:sqflite/sqlite_api.dart';

/// File demonstrates connecting with a local sqlite DB.
///
final placesTable = 'user_places';

Future<Database> _getDatabase() async {
  final dbPath = await sql.getDatabasesPath();
  final db = await sql.openDatabase(
    path.join(dbPath, 'places.db'),
    onCreate: (db, version) {
      return db.execute(
          'CREATE TABLE $placesTable(id TEXT PRIMARY KEY, title TEXT, image TEXT, lat REAL, lng REAL, address TEXT)');
    },
    version: 1,
  );
  return db;
}

Future<List<Place>> getPlaces() async {
  final db = await _getDatabase();
  final data = await db.query(placesTable);
  return data
    .map(
      (row) => Place(
        id: row['id'] as String,
        title: row['title'] as String,
        // Reinstate as a File using the path that is actually stored in this field.
        image: File(row['image'] as String),
        location: PlaceLocation(
          latitude: row['lat'] as double,
          longitude: row['lng'] as double,
          address: row['address'] as String,
        ),
      ),
    )
    .toList();
}

Future savePlace(Place place) async {
  final db = await _getDatabase();
  await db.insert(placesTable, {
    'id': place.id,
    'title': place.title,
    'image': place.image!.path,
    'lat': place.location!.latitude,
    'lng': place.location!.longitude,
    'address': place.location!.address,
  });
}