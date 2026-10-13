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
Flutter/Dart, GetX, ASP.NET Core Web API (C#, .NET 10), Android SDK API 36.

## Điều kiện để chạy bộ khung
- Flutter (đã kiểm tra với 3.47.7 / Dart 3.13.5), Android SDK Platform 36.
- .NET 10 SDK (project đang target `net10.0`).

```bash
# Frontend
cd frontend/style_wardrobe && flutter pub get && flutter run
# Backend
cd backend/StyleWardrobe.Api && dotnet build && dotnet run
```

## Thành viên
| TV | Tên | Phụ trách |
|---|---|---|
| 1 | Bảo (Leader) | Nền chung, auth, thuật toán gợi ý phối đồ |
| 2 | Kiên | Module `wardrobe` (quản lý tủ đồ) |
| 3 | Đảm | Module `outfit` (gợi ý phối đồ, thay món) |
| 4 | Duy | Module `favorites`, `schedule`, hồ sơ người dùng |

Chi tiết phân công xem [TASKS](docs/TASKS.md).

## Tài liệu — đọc theo thứ tự
1. [PROJECT_BRIEF](docs/PROJECT_BRIEF.md) — dự án làm gì.
2. [TASKS](docs/TASKS.md) — phân công và quy tắc phối hợp (**quan trọng**).
3. [GIT_WORKFLOW](docs/GIT_WORKFLOW.md) — branch, commit, Pull Request.
4. [ARCHITECTURE](docs/ARCHITECTURE.md) — kiến trúc Flutter (MVC + GetX) và backend phân lớp.
5. [API_CONTRACT](docs/API_CONTRACT.md) — request/response của API.

Sau khi đọc: cài môi trường, chạy `flutter doctor`, tạo branch `feature/<module>-<mô-tả>` rồi bắt đầu làm.

## Lưu ý
- Không commit secret hay file cấu hình nhạy cảm.
