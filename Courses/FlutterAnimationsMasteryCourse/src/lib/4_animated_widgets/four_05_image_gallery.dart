import 'package:flutter/material.dart';
import 'package:src/widets/core/action_button.dart';

class Four05ImageGallery extends StatefulWidget {
  const Four05ImageGallery({super.key});

  @override
  State<Four05ImageGallery> createState() => _Four05ImageGalleryState();
}

class _Four05ImageGalleryState extends State<Four05ImageGallery> {
  final images = ['Image one', 'Image two', 'Image three', 'Image four'];
  int displayIndex = 0;

  void incrementIndex() {
    setState(() {
      displayIndex += 1;
      if (displayIndex >= images.length) {
        displayIndex = 0;
      }
    });
  }

  Widget buildImage() {
    return Container(color: Colors.blue, child: Text(images[displayIndex]));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Column(
          children: [
            AnimatedContainer(
              duration: Duration(milliseconds: 300),
              child: buildImage(), 
            ),
            Spacer(),
            ActionButton('Show next!', incrementIndex)
        ],),
      ),
    );
  }
}
