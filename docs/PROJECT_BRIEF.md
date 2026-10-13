# PROJECT BRIEF — StyleWardrobe (tên tạm thời)

## Mục tiêu
Ứng dụng di động giúp người dùng quản lý tủ quần áo cá nhân và nhận gợi ý phối đồ từ những món đồ đang sở hữu.

## Người dùng mục tiêu
- Sinh viên, người đi làm muốn sắp xếp tủ đồ và tiết kiệm thời gian chọn trang phục.
- Người quan tâm đến phối đồ theo dịp (đi học, đi chơi, đi làm).

## Phạm vi chức năng dự kiến (CHƯA triển khai)
| Nhóm | Mô tả dự kiến |
|---|---|
| Auth / User | Đăng ký, đăng nhập, hồ sơ người dùng (chưa chốt cơ chế) |
| Wardrobe | Thêm/sửa/xóa/xem món đồ, chọn ảnh, phân loại, màu sắc, tìm kiếm, lọc |
| Outfit | Chọn dịp, nhận bộ phối từ backend, lọc phong cách/màu, thay món (nếu có) |
| Favorites | Lưu bộ phối yêu thích |
| Schedule | Lưu bộ phối theo ngày |

Chi tiết sẽ được chốt ở giai đoạn phân tích tiếp theo.

## Công nghệ
- **Frontend:** Flutter/Dart, GetX (state, routing, DI), MVC. Android SDK API 36 (Android 16).
- **Backend:** ASP.NET Core Web API (C#), RESTful, kiến trúc phân lớp.
- **Dịch vụ có thể xem xét sau:** Firebase Authentication, Firebase Storage.
- **Cơ sở dữ liệu:** SQL Server (EF Core). Xác thực: Firebase Auth.

## Giới hạn của giai đoạn khởi tạo
Chỉ có bộ khung dự án và tài liệu. Chưa có: CRUD, thuật toán gợi ý, yêu thích, lịch, giao diện hoàn chỉnh, database/migration, Firebase, authentication, API nghiệp vụ, kết nối REST, dữ liệu giả.
