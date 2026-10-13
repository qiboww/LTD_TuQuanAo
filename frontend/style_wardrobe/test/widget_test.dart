import 'package:flutter_test/flutter_test.dart';

import 'package:style_wardrobe/main.dart';

void main() {
  testWidgets('App boots to placeholder page', (WidgetTester tester) async {
    await tester.pumpWidget(const StyleWardrobeApp());
    await tester.pumpAndSettle();
    expect(find.text('StyleWardrobe'), findsOneWidget);
  });
}
