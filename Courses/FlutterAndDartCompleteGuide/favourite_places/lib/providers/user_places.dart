import 'package:favourite_places/models/place.dart';
import 'package:riverpod/legacy.dart';

class UserPlacesNotifier extends StateNotifier<List<Place>> {
  UserPlacesNotifier(): super(const []);

  void addPlace(Place place) {
    state = [...state, place];
  }
}

final userPlacesNotifier = StateNotifierProvider((ref) => UserPlacesNotifier());