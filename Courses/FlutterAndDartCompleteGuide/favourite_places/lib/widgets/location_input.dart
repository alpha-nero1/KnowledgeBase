import 'package:favourite_places/api/api.dart';
import 'package:favourite_places/models/place_location.dart';
import 'package:favourite_places/screens/map.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:location/location.dart';

class LocationInput extends StatefulWidget {
  final void Function(PlaceLocation location) locationOnSelect;
  const LocationInput({super.key, required this.locationOnSelect});

  @override
  State<LocationInput> createState() => _LocationInputState();
}

class _LocationInputState extends State<LocationInput> {
  var _isLoading = false;
  PlaceLocation? _placeLocation;

  Future _savePlace(double lat, double long) async {
    setState(() {
      _isLoading = false;
    });

    String address = await reverseGeocodeAsync(lat, long);

    setState(() {
      _placeLocation = PlaceLocation(
        latitude: lat,
        longitude: long,
        address: address,
      );
      widget.locationOnSelect(_placeLocation!);
      _isLoading = false;
    });

  }

  /// Stock standard implementation recommended by the package itself.
  void _getCurrentLocation() async {
    Location location = Location();

    bool serviceEnabled;
    PermissionStatus permissionGranted;
    LocationData locationData;

    serviceEnabled = await location.serviceEnabled();
    if (!serviceEnabled) {
      serviceEnabled = await location.requestService();
      if (!serviceEnabled) {
        return;
      }
    }

    permissionGranted = await location.hasPermission();
    if (permissionGranted == PermissionStatus.denied) {
      permissionGranted = await location.requestPermission();
      if (permissionGranted != PermissionStatus.granted) {
        return;
      }
    }

    setState(() {
      _isLoading = true;
    });

    locationData = await location.getLocation();

    if (locationData.latitude == null || locationData.longitude == null) {
      return;
    }

    _savePlace(locationData.latitude!, locationData.longitude!);
  }

  void _selectMapLocation(BuildContext context) async {
    final pickedLoc = await Navigator.of(context).push<LatLng>(MaterialPageRoute(builder: (ctx) => MapScreen()));

    if (pickedLoc == null) {
      return;
    }

    _savePlace(pickedLoc.latitude, pickedLoc.longitude);
  }

  String _getLocationImage() => _placeLocation == null
    ? ''
    : getStaticImage(_placeLocation!.latitude, _placeLocation!.longitude);

  Widget _getPreviewContent(BuildContext context) {
    if (_placeLocation == null) {
      return Text(
        'No location chosen',
        textAlign: TextAlign.center,
        style: Theme.of(context).textTheme.bodyLarge!.copyWith(
              color: Theme.of(context).colorScheme.onBackground,
            ),
      );
    }

    return Image.network(
      _getLocationImage(),
      fit: BoxFit.cover,
      width: double.infinity,
      height: double.infinity,
    );
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return CircularProgressIndicator();
    }

    return Column(
      children: [
        // Map snapshot.
        Container(
          height: 170,
          width: double.infinity,
          decoration: BoxDecoration(
            border: Border.all(
              width: 1,
              color: Theme.of(context).colorScheme.primary.withAlpha(60),
            ),
          ),
          child: _getPreviewContent(context),
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            TextButton.icon(
              icon: const Icon(Icons.location_on),
              label: const Text('Get current location'),
              onPressed: _getCurrentLocation,
            ),
            TextButton.icon(
              icon: const Icon(Icons.map),
              label: const Text('Select on map'),
              onPressed: () => _selectMapLocation(context),
            ),
          ],
        ),
      ],
    );
  }
}
