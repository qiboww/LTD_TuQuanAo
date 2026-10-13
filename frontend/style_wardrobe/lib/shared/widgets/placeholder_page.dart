import 'package:flutter/material.dart';

/// Temporary page used only to verify the app boots. Remove when real screens exist.
class PlaceholderPage extends StatelessWidget {
  const PlaceholderPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(child: Text('StyleWardrobe')),
    );
  }
}
