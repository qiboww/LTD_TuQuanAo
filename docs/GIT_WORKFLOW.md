# GIT WORKFLOW

## Branch
- `main`: ổn định, chỉ merge qua Pull Request đã review.
- `develop` (tùy chọn, do Leader quyết định): nhánh tích hợp.
- Nhánh cá nhân/tính năng: `feature/<module>-<mô-tả-ngắn>`, ví dụ `feature/wardrobe-list`.
- Sửa lỗi: `fix/<module>-<mô-tả>`; tài liệu: `docs/<mô-tả>`.
- Mỗi thành viên làm trên branch riêng, chỉ sửa trong module của mình.

## Commit message
Định dạng: `<type>(<scope>): <mô tả ngắn>`
- type: `feat`, `fix`, `docs`, `refactor`, `test`, `chore`
- scope: `wardrobe`, `outfit`, `favorites`, `schedule`, `auth`, `backend`, `app`…
- Ví dụ: `feat(wardrobe): add clothes list page`

Commit nhỏ, mỗi commit một thay đổi logic.

## Pull Request
- Mô tả rõ thay đổi, phạm vi module, cách kiểm tra.
- Chạy `flutter analyze`/`flutter test` (frontend) hoặc `dotnet build` (backend) trước khi tạo PR.
- Cần ít nhất 1 review (Leader review PR thay đổi cấu trúc chung/API/model).
- Không tự merge PR của mình khi chưa được duyệt.
- Thay đổi API contract hoặc model dùng chung phải được Leader đồng ý trước.

## Review
- Kiểm tra đúng kiến trúc (MVC + GetX, phân lớp backend), phạm vi, không có secret, không file thừa.
- Phản hồi mang tính xây dựng; người tạo PR sửa và yêu cầu review lại.

## Xử lý xung đột
1. `git fetch` rồi cập nhật branch từ `main` (merge hoặc rebase theo thống nhất nhóm).
2. Giải quyết xung đột cục bộ, chạy lại kiểm tra.
3. Xung đột ở file chung (routes, theme, contract): liên hệ Leader, không tự quyết.

## Không commit secret
- Không commit API key, mật khẩu, token, chuỗi kết nối, `google-services.json`, keystore, `appsettings.*.json` chứa thông tin nhạy cảm, file `.env`.
- Dùng file mẫu (`*.example`) và `.gitignore`.
- Nếu lỡ commit secret: báo Leader ngay, thu hồi/đổi secret, không chỉ xóa ở commit sau.

## File dùng chung và migration
- Các file sau do **TV1 quản lý**: `Program.cs`, `DbContext`, thư mục migration, `core/`, `app/routes`, `app/theme`, `docs/API_CONTRACT.md`. Thành viên khác muốn sửa phải tạo PR và được TV1 duyệt.
- **Chỉ TV1 tạo và chạy migration.** Cần thêm/sửa bảng thì báo TV1 kèm mô tả cột; không tự tạo migration trên branch cá nhân.
- Mỗi thành viên chỉ thêm file trong module của mình (Flutter: `modules/<tên>`, backend: file theo tên module).
