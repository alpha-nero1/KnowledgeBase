import 'package:capstone_chatapp/widgets/message_bubble.dart';
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';

class ChatMessages extends StatelessWidget {
  const ChatMessages({super.key});

  @override
  Widget build(BuildContext context) {
    final currentUserId = FirebaseAuth.instance.currentUser!.uid;

    return StreamBuilder(
      // Will listen to the remote DB
      stream: FirebaseFirestore.instance
          .collection('messages')
          .orderBy('createdAt', descending: true)
          .snapshots(),
      builder: (ctx, snapshots) {
        if (snapshots.connectionState == ConnectionState.waiting) {
          return const Center(child: CircularProgressIndicator());
        }
        if (!snapshots.hasData || snapshots.data!.docs.isEmpty) {
          return const Center(child: Text('No messages yet'));
        }
        if (snapshots.hasError) {
          return const Center(child: Text('Something went wrong'));
        }

        final messages = snapshots.data!.docs;

        return ListView.builder(
          padding: EdgeInsets.only(bottom: 40, left: 8, right: 8),
          // so elements show from the bottom.
          reverse: true,
          itemCount: messages.length,
          itemBuilder: (ctxx, index) {
            final chatMessage = messages[index].data();
            final nextMessage = index + 1 < messages.length
                ? messages[index + 1]
                : null;
            final currentMessageUserId = chatMessage['userId'];
            final nextMessageUserId = nextMessage != null
                ? nextMessage['userId']
                : null;
            final nextMessageIsSameSender =
                currentMessageUserId == nextMessageUserId;

            if (nextMessageIsSameSender) {
              return MessageBubble.next(
                message: chatMessage['text'],
                isMe: currentMessageUserId == currentUserId,
              );
            }

            return MessageBubble.first(
              userImage: chatMessage['userImage'] , 
              username: chatMessage['username'], 
              message: chatMessage['text'], 
              isMe: currentMessageUserId == currentUserId
            );
          },
        );
      },
    );
  }
}
