import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';

class MessageInput extends StatefulWidget {
  const MessageInput({super.key});

  @override
  State<MessageInput> createState() {
    return _MessageInputState();
  }
}

class _MessageInputState extends State<MessageInput> {
  final _messageController = TextEditingController();

  @override
  void dispose() {
    _messageController.dispose();
    super.dispose();
  }

  void _submitMessage() async {
    final enteredMessage = _messageController.text;

    if (enteredMessage.trim().isEmpty) {
      return;
    }

    // This is how we can terminate the keyboard.
    FocusScope.of(context).unfocus();
    _messageController.clear();

    var currrentUserId = FirebaseAuth.instance.currentUser!.uid;

    // We need complete info from firestore
    var user = await FirebaseFirestore.instance.collection('users').doc(currrentUserId).get();
    var userData = user.data()!;

    // send to Firebase
    FirebaseFirestore.instance
      .collection('messages')
      // Add adds a doc id for us, and do not need to set.
      .add({
        'text': enteredMessage,
        'createdAt': Timestamp.now(),
        'userId': currrentUserId,
        'username': userData['username'],
        'userImage': userData['imageUrl']
      });
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(left: 15, right: 1, bottom: 14),
      child: Row(
        children: [
          Expanded(
            child: TextField(
              controller: _messageController,
              textCapitalization: TextCapitalization.sentences,
              autocorrect: true,
              enableSuggestions: true,
              decoration: const InputDecoration(labelText: 'Send a message...'),
            ),
          ),
          IconButton(
            color: Theme.of(context).colorScheme.primary,
            icon: const Icon(
              Icons.send,
            ),
            onPressed: _submitMessage,
          ),
        ],
      ),
    );
  }
}