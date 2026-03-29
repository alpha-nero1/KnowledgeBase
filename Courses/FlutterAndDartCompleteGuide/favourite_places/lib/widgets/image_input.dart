import 'dart:io';

import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';

class ImageInput extends StatefulWidget {
  final void Function(File? image) onPickImage;

  const ImageInput({ super.key, required this.onPickImage });
  
  @override
  State<StatefulWidget> createState() => _State();
}

class _State extends State<ImageInput> {
  File? _selectedImage;
  
  void _takePicture() async {
    final picker = ImagePicker();
    final pickedImage = await picker.pickImage(source: ImageSource.camera, maxWidth: 600);

    if (pickedImage == null) {
      setState(() {
        _selectedImage = null;
        widget.onPickImage(null);
      });
      return;
    }

    setState(() {
      _selectedImage = File(pickedImage.path);
      widget.onPickImage(_selectedImage);
    });
  }

  Widget getContent() {
    if (_selectedImage == null) {
      return TextButton.icon(
        icon: const Icon(Icons.camera),
        label: const Text('Take Picture'),
        onPressed: _takePicture,
      );
    }

    // Simply wrap with GestureDetector to pick up on taps or press on things
    // but does not need to be a button.
    return GestureDetector(
      onTap: _takePicture,
      child: Image.file(
        _selectedImage!,
        fit: BoxFit.cover,
        width: double.infinity,
        height: double.infinity,
      ),
    );
  }
  
  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(
          width: 1,
          color: Theme.of(context).colorScheme.primary.withAlpha(60),
        ),
      ),
      height: 250,
      width: double.infinity,
      alignment: Alignment.center,
      child: getContent(),
    );
  }
}