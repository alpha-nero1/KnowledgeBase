import 'package:capstone_chatapp/widgets/chat_messages.dart';
import 'package:capstone_chatapp/widgets/message_input.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';

class ChatScreen extends StatelessWidget {
  const ChatScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('FlutterChat'),
        actions: [
          IconButton(
            onPressed: () {
              // Signout will sign the user out of this session.
              // FirebaseAuth seems to be a kind of singleton that is managing the user's session
              // and stream under the hood so you need not build, maintain and ensure that kind of logic is already working
              // you get to use ironclad, battletested logic out the gate. Always hated managing login state.
              FirebaseAuth.instance.signOut();
            },
            icon: Icon(
              Icons.exit_to_app,
              color: Theme.of(context).colorScheme.primary,
            ),
          ),
        ],
      ),
      body: Column(children: const [
        Expanded(child: ChatMessages()),
        MessageInput()
      ],)
    );
  }
}