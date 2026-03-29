import 'package:favourite_places/api/api.dart';
import 'package:favourite_places/models/place.dart';
import 'package:favourite_places/screens/map.dart';
import 'package:flutter/material.dart';

class PlaceDetailScreen extends StatelessWidget {
  final Place place;

  const PlaceDetailScreen({super.key, required this.place});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(place.title)),
      body: Stack(
        children: [
          if (place.image != null)
            Image.file(
              place.image!,
              fit: BoxFit.cover,
              width: double.infinity,
              height: double.infinity,
            ),
          Positioned(
            top: 0,
            bottom: 0,
            left: 0,
            right: 0,
            child: Column(
              children: [
                if (place.location != null)
                  GestureDetector(
                    onTap: () => Navigator.of(context).push(MaterialPageRoute(builder: 
                      (ctx) => MapScreen(location: place.location!, isSelecting: false)
                    )),
                    child: CircleAvatar(
                      radius: 70,
                      backgroundImage: NetworkImage(
                        getStaticImage(
                          place.location!.latitude,
                          place.location!.longitude,
                        ),
                      ),
                    ),
                  ),
                Container(
                  alignment: Alignment.center,
                  padding: EdgeInsets.symmetric(horizontal: 24, vertical: 16),
                  decoration: const BoxDecoration(
                    gradient: LinearGradient(
                      colors: [Colors.transparent, Colors.black54],
                      begin: Alignment.topCenter,
                      end: Alignment.bottomCenter,
                    ),
                  ),
                  child: Text(
                    place.location!.address,
                    textAlign: TextAlign.center,
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onSurface,
                        ),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
