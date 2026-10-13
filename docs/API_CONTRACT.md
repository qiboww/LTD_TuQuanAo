# API CONTRACT — BẢN ĐỀ XUẤT v0.1

> [!WARNING]
> **PROPOSED — chờ nhóm duyệt.** Chưa có endpoint nào được triển khai. Sau khi cả nhóm đồng ý, Leader đổi trạng thái thành **APPROVED v1.0**. Từ đó, mọi thay đổi phải qua Leader. Mục nào còn **(OPEN)** nghĩa là chưa chốt.

## 0. Quy ước chung

| Mục | Quy ước đề xuất |
|---|---|
| Base path | `/api/v1` |
| Định dạng | JSON, UTF-8, tên field `camelCase` |
| ID | số nguyên (`int`), do SQL Server tự sinh |
| Thời gian | `createdAt`: ISO 8601 UTC, ví dụ `2026-10-13T08:00:00Z` |
| Ngày (lịch) | `yyyy-MM-dd`, ví dụ `2026-10-13` |
| Xác thực | Header `Authorization: Bearer <Firebase ID token>` trên **mọi** endpoint (trừ khi ghi khác) |
| Dữ liệu theo người dùng | Backend lấy `uid` từ token. Client **không** gửi `userId`. User chỉ thấy dữ liệu của mình |
| Lỗi | `{ "code": "string", "message": "string" }` |
| Mã HTTP | `200` OK, `201` tạo mới, `204` xóa thành công, `400` dữ liệu sai, `401` thiếu/sai token, `404` không tồn tại, `500` lỗi server |

Mã lỗi (`code`) đề xuất: `VALIDATION_ERROR`, `UNAUTHORIZED`, `NOT_FOUND`, `INTERNAL_ERROR`.

### Danh sách giá trị cố định (enum, đề xuất)

| Trường | Giá trị |
|---|---|
| `type` | `top`, `bottom`, `outerwear`, `dress`, `shoes`, `accessory` |
| `color` | `black`, `white`, `gray`, `red`, `orange`, `yellow`, `green`, `blue`, `purple`, `pink`, `brown`, `beige` |
| `occasion` | `school`, `casual`, `work` |
| `style` | `casual`, `formal`, `sporty`, `street` |

Giá trị gửi/nhận là chuỗi tiếng Anh ở trên; Flutter tự dịch sang tiếng Việt khi hiển thị.

## 1. Authentication / User

**Đăng ký, đăng nhập, quên mật khẩu do Flutter làm trực tiếp với Firebase Auth.** Backend **không** có endpoint đăng ký/đăng nhập.
Backend chỉ xác minh Firebase ID token ở mỗi request. Lần đầu một `uid` gọi API, backend tự tạo bản ghi `User`.

| Method | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/v1/users/me` | Lấy hồ sơ (tự tạo nếu chưa có) |
| PUT | `/api/v1/users/me` | Cập nhật hồ sơ |

`User`:
```json
{ "id": 1, "email": "a@b.com", "displayName": "An", "avatarUrl": "https://..." }
```
Request PUT: `{ "displayName": "An", "avatarUrl": "https://..." }` (`email` lấy từ token, không sửa).

## 2. Wardrobe / Clothes

| Method | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/v1/clothes?type=&color=&search=&page=1&pageSize=20` | Danh sách, có lọc/tìm kiếm |
| GET | `/api/v1/clothes/{id}` | Chi tiết |
| POST | `/api/v1/clothes` | Thêm |
| PUT | `/api/v1/clothes/{id}` | Sửa |
| DELETE | `/api/v1/clothes/{id}` | Xóa |
| POST | `/api/v1/uploads/images` | Upload ảnh (multipart), trả về URL |

`Clothes`:
```json
{
  "id": 1,
  "name": "Áo sơ mi trắng",
  "type": "top",
  "color": "white",
  "style": "formal",
  "imageUrl": "https://.../images/abc.jpg",
  "createdAt": "2026-10-13T08:00:00Z"
}
```

- `search` tìm theo `name`. Tất cả tham số lọc đều tùy chọn.
- Response GET danh sách: `{ "items": [Clothes], "total": 42, "page": 1, "pageSize": 20 }`.
- Request POST/PUT: các field `name`, `type`, `color`, `style`, `imageUrl` (không có `id`, `createdAt`). `name` bắt buộc, tối đa 100 ký tự.
- **Upload ảnh:** `POST /uploads/images` với `multipart/form-data`, field `file` (jpg/png, tối đa 5 MB). Trả về `{ "imageUrl": "..." }`. Flutter gọi endpoint này trước, rồi dùng `imageUrl` khi POST/PUT clothes.
- Xóa món đồ: các bản ghi yêu thích/lịch chứa món đó **(OPEN)** — đề xuất: tự gỡ món khỏi các bộ đó.

## 3. Outfit Suggestions

Thuật toán gợi ý chạy ở **backend**. Flutter chỉ hiển thị.

| Method | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/v1/outfits/suggestions?occasion=&style=&color=` | Lấy các bộ phối gợi ý từ tủ đồ của user |
| GET | `/api/v1/outfits/alternatives?replaceClothesId=&clothesIds=1,2,3&occasion=` | Gợi ý món thay thế (chỉ làm nếu có chức năng thay món) |

- `occasion` bắt buộc; `style`, `color` tùy chọn.
- Response suggestions:
```json
{
  "items": [
    {
      "occasion": "school",
      "style": "casual",
      "clothes": [ { "...Clothes..." }, { "...Clothes..." } ]
    }
  ]
}
```
Một bộ phối không có `id` (tạo tạm thời, không lưu). Chỉ khi user yêu thích hoặc lên lịch thì mới lưu danh sách `clothesIds`.
- Response alternatives: `{ "items": [Clothes] }` — các món cùng `type` với món bị thay.
- Nếu tủ đồ không đủ để tạo bộ: trả `items: []`, không phải lỗi.

## 4. Favorites

| Method | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/v1/favorites` | Danh sách bộ phối yêu thích |
| POST | `/api/v1/favorites` | Thêm vào yêu thích |
| DELETE | `/api/v1/favorites/{id}` | Bỏ yêu thích |

`Favorite`:
```json
{
  "id": 5,
  "occasion": "school",
  "style": "casual",
  "clothes": [ { "...Clothes..." } ],
  "createdAt": "2026-10-13T08:00:00Z"
}
```
Request POST: `{ "occasion": "school", "style": "casual", "clothesIds": [1, 2, 3] }`.

## 5. Schedule

Mỗi ngày có tối đa một bộ phối (đề xuất).

| Method | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/v1/schedules?from=2026-10-01&to=2026-10-31` | Lịch trong khoảng ngày |
| PUT | `/api/v1/schedules/{date}` | Gán/thay bộ phối cho một ngày |
| DELETE | `/api/v1/schedules/{date}` | Xóa lịch của ngày đó |

`ScheduleEntry`:
```json
{ "date": "2026-10-13", "clothes": [ { "...Clothes..." } ] }
```
Request PUT: `{ "clothesIds": [1, 2, 3] }`.

## 6. Quyết định còn chưa chốt (OPEN)

| # | Nội dung | Đề xuất |
|---|---|---|
| 1 | Nơi lưu file ảnh | Backend lưu vào thư mục cục bộ khi dev. Không dùng Firebase Storage để tránh phải trả phí/thẻ |
| 2 | Xóa món đồ đang nằm trong favorites/schedule | Tự gỡ món khỏi các bộ đó |
| 3 | Danh sách enum `style`, `type`, `color` | Dùng bảng ở mục 0, nhóm duyệt lại |
| 4 | Có làm chức năng thay món (`alternatives`) không | Chỉ làm nếu còn thời gian |
| 5 | Thuật toán gợi ý cụ thể (quy tắc phối màu, chọn món) | Leader thiết kế sau, không ảnh hưởng contract |
| 6 | Cách deploy backend để các thành viên gọi được | Mỗi người chạy backend local, hoặc Leader deploy một bản chung |
| 7 | Có cần tách bảng riêng cho bộ phối hay không | Quyết định khi thiết kế database |
