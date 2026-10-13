# TASKS — Phân công 4 thành viên

Mỗi thành viên phụ trách **cả Flutter lẫn backend (ASP.NET Core)** cho module của mình. Leader dựng phần nền chung trước; chỉ sau khi nền xong mới bắt đầu làm module.

## Giai đoạn khởi tạo — đã làm
- Cấu trúc repo, bộ khung Flutter + GetX, bộ khung ASP.NET Core Web API.
- Tài liệu: PROJECT_BRIEF, ARCHITECTURE, TASKS, API_CONTRACT (đề xuất), GIT_WORKFLOW.

Chưa làm: mọi tính năng nghiệp vụ, database, Firebase, kết nối API.

## Các giai đoạn tiếp theo

| Giai đoạn | Nội dung | Người làm |
|---|---|---|
| **1. Nền chung** | Database, xác thực, network layer, auth, endpoint mẫu | TV1 (cả nhóm chờ) |
| **2. Module** | Mỗi người làm module của mình (Flutter + backend) | TV2, TV3, TV4, TV1 |
| **3. Tích hợp & kiểm thử** | Ghép module, sửa lỗi, kiểm thử luồng sử dụng | Cả nhóm |

Trong giai đoạn 1, TV2–4 chuẩn bị: cài môi trường, đọc tài liệu, tạo branch, làm UI bằng dữ liệu giả nếu muốn.

## Bảng phân công

| TV | Vai trò | Frontend (Flutter) | Backend (ASP.NET Core) |
|---|---|---|---|
| 1 | Leader — nền, auth, gợi ý | `core/network`, module `auth`, khung điều hướng chính | Nền chung, `User`, **thuật toán gợi ý phối đồ** |
| 2 | Quản lý tủ đồ | Module `wardrobe` | Clothes CRUD, upload ảnh |
| 3 | Gợi ý phối đồ | Module `outfit` | API `alternatives` (thay món) |
| 4 | Yêu thích, lịch, hồ sơ | Module `favorites`, `schedule`, hồ sơ người dùng | Favorites, Schedule |

### TV1 — Leader
- **Nền chung (giai đoạn 1, ưu tiên cao nhất):**
  - EF Core + SQL Server, bảng `User` và `Clothes`, quy trình migration.
  - Xác thực Firebase ID token, định dạng lỗi và response chung.
  - Một endpoint mẫu hoàn chỉnh (Controller → Service → Repository) để mọi người theo đó.
  - Hướng dẫn chạy SQL Server và backend ở máy local.
  - Flutter: network layer dùng `http` (gắn token, xử lý lỗi), module `auth` (đăng ký/đăng nhập Firebase Auth), khung điều hướng.
- **Giai đoạn 2:** thuật toán gợi ý phối đồ (backend, API `/outfits/suggestions`).
- **Luôn:** quản lý repo, chốt API contract, review PR, giải quyết xung đột, **sở hữu migration**.
- **Đầu ra:** nền chạy được, API gợi ý, build tích hợp.

### TV2 — Wardrobe
- **Flutter:** danh sách, thêm/sửa/xóa/chi tiết, chọn ảnh, phân loại theo loại và màu, tìm kiếm, lọc.
- **Backend:** Clothes CRUD, upload ảnh, lọc/tìm kiếm.
- **Đầu ra:** module `wardrobe` hoạt động độc lập.
- **Phụ thuộc:** nền chung (TV1). **Các module khác phụ thuộc vào Clothes của TV2** nên cần làm sớm nhất.

### TV3 — Outfit
- **Flutter:** chọn dịp, hiển thị bộ phối do backend gợi ý, lọc theo phong cách/màu, thay món (nếu có).
- **Backend:** API `alternatives` (gợi ý món thay thế cùng loại).
- **Lưu ý:** không tự xây thuật toán gợi ý chính; thuật toán do TV1 phụ trách.
- **Phụ thuộc:** nền chung (TV1); Clothes (TV2); API gợi ý (TV1). Làm UI bằng dữ liệu giả đến khi API sẵn sàng.

### TV4 — Favorites, Schedule, hồ sơ
- **Flutter:** module Favorites, Schedule, hồ sơ người dùng.
- **Backend:** Favorites, Schedule.
- **Phụ thuộc:** nền chung (TV1); Clothes (TV2); bộ phối (TV1/TV3).
- Hỗ trợ kiểm thử tích hợp. Chi tiết chức năng chốt ở giai đoạn phân tích tiếp theo.

## Sơ đồ phụ thuộc
`TV1 (nền) → TV2 (Clothes) → TV1 (gợi ý) → TV3 (Outfit) → TV4 (Favorites/Schedule)`

## Quy tắc phối hợp
- Mỗi thành viên làm trên branch riêng.
- Mỗi người chỉ thêm file trong module của mình, cả Flutter lẫn backend.
- **File dùng chung do TV1 quản lý:** `Program.cs`, `DbContext`, migration, `core/`, `app/routes`, `app/theme`, API contract. Muốn sửa thì báo TV1 hoặc tạo PR để TV1 duyệt.
- **Migration chỉ do TV1 tạo.** Thành viên gửi yêu cầu thay đổi bảng cho TV1.
- Không tự ý đổi model, API contract hoặc cấu trúc chung.
- Không sửa module của thành viên khác nếu chưa thống nhất.
- Khi backend chưa sẵn sàng có thể dùng dữ liệu giả ở giai đoạn sau (chưa tạo ở giai đoạn này).
- Thống nhất request/response trước khi kết nối API.
- Pull request phải được review trước khi merge.
- Không commit secret, API key, mật khẩu, file cấu hình nhạy cảm.
