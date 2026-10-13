# StyleWardrobe

Ứng dụng di động quản lý tủ quần áo cá nhân và gợi ý phối đồ. Tên dự án tạm thời.

> **Trạng thái:** giai đoạn khởi tạo — chỉ có bộ khung và tài liệu. Chưa có tính năng nghiệp vụ, database, Firebase hay kết nối API.

## Cấu trúc repository
```
frontend/style_wardrobe/   Flutter + GetX (MVC theo module)
backend/StyleWardrobe.Api/ ASP.NET Core Web API (phân lớp)
docs/                      Tài liệu dự án
```

## Công nghệ
Flutter/Dart, GetX, ASP.NET Core Web API (C#, .NET 8), Android SDK API 36.

## Điều kiện để chạy bộ khung
- Flutter (đã kiểm tra với 3.47.4 / Dart 3.13.3), Android SDK Platform 36.
- .NET 8 SDK (hoặc mới hơn — nếu dùng bản mới hơn, chỉnh `TargetFramework`).

```bash
# Frontend
cd frontend/style_wardrobe && flutter pub get && flutter run
# Backend
cd backend/StyleWardrobe.Api && dotnet build && dotnet run
```

## Tài liệu
- [PROJECT_BRIEF](docs/PROJECT_BRIEF.md) · [ARCHITECTURE](docs/ARCHITECTURE.md) · [TASKS](docs/TASKS.md) · [API_CONTRACT](docs/API_CONTRACT.md) · [GIT_WORKFLOW](docs/GIT_WORKFLOW.md)

## Lưu ý
- Không commit secret hay file cấu hình nhạy cảm.
