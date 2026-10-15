# Hướng dẫn Chạy & Phát triển Backend (StyleWardrobe API)

Tài liệu này dành cho các thành viên trong nhóm phát triển Backend (.NET 10 Web API).

---

## 1. Yêu cầu môi trường
- **.NET 10 SDK** (kiểm tra bằng `dotnet --version`).
- **Entity Framework Core CLI**: cài đặt bằng `dotnet tool install --global dotnet-ef` (nếu chưa có).
- **SQL Server LocalDB**: mặc định có sẵn khi cài Visual Studio hoặc SQL Server Express. Connection string đã được cấu hình sẵn trong `appsettings.json`:
  ```json
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\MSSQLLocalDB;Database=StyleWardrobe;Trusted_Connection=True;TrustServerCertificate=True"
  }
  ```

---

## 2. Khởi tạo Cơ sở dữ liệu
Tại thư mục gốc của repository, chạy lệnh sau để áp dụng migration tạo các bảng vào SQL Server LocalDB:
```bash
dotnet ef database update --project backend/StyleWardrobe.Api
```

---

## 3. Chạy ứng dụng Backend
```bash
cd backend/StyleWardrobe.Api
dotnet run
```
API sẽ lắng nghe tại: `http://localhost:5080`.

---

## 4. Xác thực Firebase & Test bằng Postman

Đã có sẵn bộ file cấu hình Postman trong thư mục `docs/postman/`:
1. Mở Postman -> Chọn nút **Import**.
2. Chọn cả 2 file:
   - [docs/postman/StyleWardrobe_API.postman_collection.json]
   - [docs/postman/StyleWardrobe_Local.postman_environment.json]
3. Chọn môi trường ở góc trên bên phải là: **StyleWardrobe (Local)**.
4. Mở folder **0. Auth (Firebase)** -> Chạy request `Firebase - Đăng nhập lấy ID Token (test1)`.
   - Script test sẽ **tự động lưu `idToken` vào Environment**.
5. Giờ bạn có thể gọi ngay các request `GET /api/v1/users/me` hoặc `PUT /api/v1/users/me`.

---

## 5. Dữ liệu Mẫu (Seed Data) cho Cơ sở Dữ liệu

Để các thành viên khác có sẵn dữ liệu test (quần áo, người dùng) mà không phải gõ tay:
- Chạy script SQL mẫu đã chuẩn bị sẵn trong `docs/scripts/seed_data.sql`:
  ```bash
  sqlcmd -S "(localdb)\MSSQLLocalDB" -i docs/scripts/seed_data.sql
  ```
  *(Hoặc mở SSMS kết nối tới `(localdb)\MSSQLLocalDB`, mở file `seed_data.sql` và bấm Execute/F5).*

---

## 6. Định dạng Lỗi Chuẩn (Unified Error Format)

Tất cả các lỗi trả về từ API đều có chung định dạng JSON:
```json
{
  "code": "ERROR_CODE",
  "message": "Mô tả chi tiết lỗi"
}
```
Các mã lỗi chính:
- `VALIDATION_ERROR`: Dữ liệu gửi lên không đúng định dạng hoặc vi phạm ràng buộc DTO.
- `UNAUTHORIZED`: Thiếu token, token sai hoặc token hết hạn.
- `NOT_FOUND`: Tài nguyên hoặc đường dẫn không tồn tại.
- `INTERNAL_ERROR`: Lỗi hệ thống ngoài dự kiến.

---

## 6. Mẫu Phát triển Module Mới (Cho TV2, TV3, TV4)

Khi phát triển module mới (ví dụ: `Clothes` của TV2), hãy tuân thủ kiến trúc phân lớp theo mẫu `Users`:

```
Controllers/ClothesController.cs      --> Nhận request, gọi Service
Services/Clothes/ClothesService.cs    --> Logic nghiệp vụ, gọi Repository, dùng ICurrentUserService lấy UserId
Repositories/ClothesRepository.cs     --> Thao tác DbContext với bảng Clothes
DTOs/Clothes/                         --> Request/Response DTOs
```

### Các bước thực hiện:
1. **Model & Migration**:
   - Model `Clothes` đã được tạo sẵn trong `backend/StyleWardrobe.Api/Models/Clothes.cs`.
   - Nếu cần thay đổi bảng, **liên hệ Leader (TV1)** để tạo migration mới. Không tự ý tạo migration trên branch cá nhân.
2. **Repository**:
   - Tạo Interface `IClothesRepository` trong `Repositories/`.
   - Cài đặt `ClothesRepository` kế thừa interface, inject `AppDbContext`.
   - Đăng ký DI trong `Program.cs` (hoặc nhờ Leader gộp PR).
3. **Service**:
   - Tạo `IClothesService` và `ClothesService` trong `Services/Clothes/`.
   - Inject `ICurrentUserService` để lấy ID người dùng hiện tại một cách an toàn từ token, **tuyệt đối không nhận `userId` từ client gửi lên**.
4. **Controller**:
   - Tạo `ClothesController` trong `Controllers/`, gắn thuộc tính `[ApiController]`, `[Authorize]`, `[Route("api/v1/clothes")]`.
   - Validate input bằng DataAnnotations trong DTO.
