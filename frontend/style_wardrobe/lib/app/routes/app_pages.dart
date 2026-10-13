import 'package:get/get.dart';

import '../../shared/widgets/placeholder_page.dart';
import 'app_routes.dart';

/// Route table. Only a placeholder page is registered at the init stage.
abstract class AppPages {
  static final pages = <GetPage<dynamic>>[
    GetPage(name: AppRoutes.initial, page: () => const PlaceholderPage()),
  ];
}
