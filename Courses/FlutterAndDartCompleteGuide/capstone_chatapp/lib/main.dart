import 'package:capstone_chatapp/firebase_options.dart';
import 'package:capstone_chatapp/screens/auth_screen.dart';
import 'package:capstone_chatapp/screens/chat_screen.dart';
import 'package:capstone_chatapp/screens/splash_screen.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(options: DefaultFirebaseOptions.currentPlatform);
  runApp(const App());
}

class App extends StatelessWidget {
  const App({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'FlutterChat',
      theme: ThemeData().copyWith(
        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color.fromARGB(255, 63, 17, 177),
        ),
      ),
      home: StreamBuilder(
        // Listens to Firebase auth state changes (sign in, sign out, restore session).
        stream: FirebaseAuth.instance.authStateChanges(),
        builder: (ctx, snapshot) {
          // Show some midway loading state!
          if (snapshot.connectionState == ConnectionState.waiting) return const SplashScreen();
          // If we have a user in the snapshot, the user is authenticated.
          if (snapshot.hasData) return const ChatScreen();
          // Otherwise, show the authentication screen.
          return const AuthScreen();
        },
      ),
    );
  }
}
