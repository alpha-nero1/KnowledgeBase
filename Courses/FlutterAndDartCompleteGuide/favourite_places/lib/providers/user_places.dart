import 'package:favourite_places/api/database.dart';
import 'package:favourite_places/models/place.dart';
import 'package:path/path.dart' as path;
import 'package:path_provider/path_provider.dart' as syspaths;
import 'package:riverpod/legacy.dart';

Future copyFileToPermanent(Place place) async {
  final appDir = await syspaths.getApplicationDocumentsDirectory();
  final image = place.image!;
  final filename = path.basename(image.path);
  final copiedImage = await image.copy('${appDir.path}/$filename');
  place.image = copiedImage;
}

class UserPlacesNotifier extends StateNotifier<List<Place>> {
  UserPlacesNotifier(): super(const []);

  Future loadPlaces() async {
    state = await getPlaces();
  }

  Future addPlace(Place place) async {
    await copyFileToPermanent(place);
    // Also shoot into sqlite.
    await savePlace(place);

    state = [...state, place];
  }
}

final userPlacesNotifier = StateNotifierProvider((ref) => UserPlacesNotifier());