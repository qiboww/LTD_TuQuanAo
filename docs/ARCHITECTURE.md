# ARCHITECTURE

## 1. Tổng quan

```
Flutter App (View ⇄ Controller ⇄ Repository ⇄ Network) ──HTTPS/REST──▶ ASP.NET Core Web API ──▶ SQL Server (EF Core)
                                                                              └─▶ Firebase Authentication (chỉ xác thực)
```

- **Backend là nơi xử lý nghiệp vụ** (bao gồm thuật toán gợi ý phối đồ) và cung cấp REST API cho Flutter.
- Frontend chỉ hiển thị, nhận input và gọi API; không chứa nghiệp vụ gợi ý.

## 2. Frontend: Flutter + GetX + MVC

Cấu trúc `frontend/style_wardrobe/lib/`:

| Thư mục | Vai trò |
|---|---|
| `app/routes` | Tên route và bảng `GetPage` |
| `app/bindings` | GetX Binding (dependency injection) |
| `app/theme` | Theme dùng chung |
| `core/constants` | Hằng số (base URL, key…) |
| `core/network` | HTTP client, interceptor, xử lý lỗi chung (giai đoạn sau) |
| `core/services` | Service dùng chung (vd: lưu token cục bộ) |
| `core/utils` | Hàm tiện ích |
| `data/models`, `data/repositories` | Model và Repository dùng chung nhiều module |
| `modules/<tên>/{models,views,controllers}` | Mỗi module một bộ MVC |
| `shared/widgets` | Widget dùng chung |

### Quy ước MVC
- **Model**: lớp dữ liệu thuần (`fromJson`/`toJson`), không chứa logic UI hay gọi mạng.
- **View**: widget Flutter, chỉ hiển thị và chuyển sự kiện cho Controller. Không gọi API trực tiếp, không chứa nghiệp vụ.
- **Controller** (`GetxController`): giữ trạng thái (`Rx`), xử lý logic giao diện, gọi Repository. Không dùng `BuildContext`/widget.
- **Repository**: lớp duy nhất nói chuyện với Network layer; trả Model cho Controller. Controller không gọi HTTP trực tiếp.
- **Binding**: đăng ký Controller/Repository cho mỗi route (`Get.lazyPut`).
- **Network layer**: bọc HTTP client, gắn header, chuyển lỗi HTTP thành exception thống nhất. Package HTTP: `http` (đã chốt; chưa thêm vào pubspec, sẽ thêm ở giai đoạn tích hợp).
- Model đặt trong `modules/<tên>/models` nếu chỉ module đó dùng; đặt vào `data/models` nếu dùng chung.

## 3. Backend: ASP.NET Core Web API (phân lớp)

`backend/StyleWardrobe.Api/`:

| Lớp | Vai trò |
|---|---|
| `Controllers` | Nhận HTTP request, validate cơ bản, gọi Service, trả response. Không chứa nghiệp vụ. |
| `DTOs` | Cấu trúc request/response trao đổi với client |
| `Models` | Entity miền nghiệp vụ |
| `Services` | Xử lý nghiệp vụ (gợi ý phối đồ…) |
| `Repositories` | Truy cập dữ liệu, ẩn chi tiết lưu trữ |
| `Data` | Cấu hình truy cập dữ liệu (DbContext… nếu chọn ORM) |
| `Middleware` | Xử lý lỗi, logging, xác thực chung |

Luồng: `Controller → Service → Repository → Data`. Phụ thuộc đi một chiều, đăng ký qua DI để dễ kiểm thử.

## 4. Ranh giới trách nhiệm

| Thành phần | Trách nhiệm | Không làm |
|---|---|---|
| Flutter | UI, trạng thái giao diện, gọi API | Nghiệp vụ, thuật toán gợi ý, truy cập DB |
| Backend | Nghiệp vụ, validate, thuật toán, truy cập dữ liệu | Render UI |
| Database | Lưu dữ liệu nghiệp vụ (SQL Server) | — |
| Firebase | Chỉ Authentication | Lưu/xử lý dữ liệu nghiệp vụ song song với backend |


## 5. Quyết định đã chốt

| Mục | Quyết định |
|---|---|
| Xác thực | Firebase Authentication. Flutter đăng ký/đăng nhập trực tiếp; backend chỉ xác minh Firebase ID token (`Authorization: Bearer ...`) |
| Cơ sở dữ liệu | SQL Server, truy cập bằng EF Core (do TV1 thiết lập) |
| HTTP client (Flutter) | `http` |
| Upload ảnh | Qua backend (multipart/form-data), lưu file trong thư mục `wwwroot/uploads/` với tên GUID, phục vụ bằng `UseStaticFiles()`. SQL Server chỉ lưu đường dẫn (ví dụ `/uploads/abc.jpg`). Không dùng Firebase Storage. Thư mục `uploads` phải nằm trong `.gitignore` |
| Dữ liệu nghiệp vụ | Chỉ nằm ở backend + SQL Server; Firebase chỉ dùng cho Auth |

## 6. Cách làm việc trên backend

- Mỗi module có Controller/Service/Repository/DTO riêng, đặt tên theo module (ví dụ `ClothesController`, `ClothesService`).
- TV1 dựng nền (DbContext, xác thực, định dạng lỗi, endpoint mẫu); các thành viên làm theo mẫu.
- File dùng chung (`Program.cs`, `DbContext`, migration) chỉ TV1 sửa; migration chỉ TV1 tạo.
