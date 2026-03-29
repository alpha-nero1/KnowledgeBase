import 'dart:convert';
import 'package:http/http.dart' as http;

// Okay to have maps key in code as it is tied down in google console based on app creds so not just anyone can use it.
// However on app load these credentials should be requested via API so that keys can be cycled safely without the need
// for an app release.
String mapsKey = 'AIzaSyBoH_8nfMJKcLkU6AaGPXks-7eJKfOckzE';

Future<String> reverseGeocodeAsync(double lat, double long) async {
  final url = Uri.parse(
        'https://maps.googleapis.com/maps/api/geocode/json?latlng=$lat,$long&key=$mapsKey');
  final response = await http.get(url);
  final resData = json.decode(response.body);
  final address = resData['results'][0]['formatted_address'] as String;
  return address;
}

String getStaticImage(double lat, double long) {
  return 'https://maps.googleapis.com/maps/api/staticmap?center=$lat,$long=&zoom=16&size=600x300&maptype=roadmap&markers=color:red%7Clabel:A%7C$lat,$long&key=$mapsKey';
}