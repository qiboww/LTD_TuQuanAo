-- ==============================================================================
-- Script: Seed Data mẫu cho cơ sở dữ liệu StyleWardrobe
-- Dành cho: Thành viên dự án StyleWardrobe (.NET 10 API & Flutter)
-- Cách chạy: Mở SQL Server Management Studio (SSMS), kết nối tới: (localdb)\MSSQLLocalDB
--            Chọn cơ sở dữ liệu StyleWardrobe và bấm Execute (F5).
-- ==============================================================================

USE [StyleWardrobe];
GO

-- 1. Thêm User mẫu (nếu chưa có)
-- Lưu ý: Khi test bằng Postman/Flutter, backend sẽ tự động tạo User dựa trên Firebase UID thật.
-- Dưới đây là 2 user mẫu cho môi trường local.
IF NOT EXISTS (SELECT 1 FROM [Users] WHERE [FirebaseUid] = 'sample-firebase-uid-001')
BEGIN
    INSERT INTO [Users] ([FirebaseUid], [Email], [DisplayName], [AvatarUrl], [CreatedAt])
    VALUES ('sample-firebase-uid-001', 'test1@sw.dev', N'Bảo Leader', 'https://images.unsplash.com/photo-1535713875002-d1d0cf377fde', SYSUTCDATETIME());
END
GO

IF NOT EXISTS (SELECT 1 FROM [Users] WHERE [FirebaseUid] = 'sample-firebase-uid-002')
BEGIN
    INSERT INTO [Users] ([FirebaseUid], [Email], [DisplayName], [AvatarUrl], [CreatedAt])
    VALUES ('sample-firebase-uid-002', 'test2@sw.dev', N'Kiên Wardrobe', 'https://images.unsplash.com/photo-1570295999919-56ceb5ecca61', SYSUTCDATETIME());
END
GO

-- Lấy ID của User 1 để liên kết khóa ngoại cho quần áo
DECLARE @UserId INT;
SELECT TOP 1 @UserId = [Id] FROM [Users] WHERE [FirebaseUid] = 'sample-firebase-uid-001';

-- 2. Thêm Quần Áo Mẫu (Clothes) cho User 1
-- Đảm bảo khớp các giá trị enum string:
-- Type: top, bottom, outerwear, dress, shoes, accessory
-- Color: black, white, gray, red, orange, yellow, green, blue, purple, pink, brown, beige
-- Style: casual, formal, sporty, street

IF NOT EXISTS (SELECT 1 FROM [Clothes] WHERE [UserId] = @UserId AND [Name] = N'Áo sơ mi Oxford trắng')
BEGIN
    INSERT INTO [Clothes] ([UserId], [Name], [Type], [Color], [Style], [ImageUrl], [CreatedAt])
    VALUES 
    (@UserId, N'Áo sơ mi Oxford trắng', 'top', 'white', 'formal', 'https://images.unsplash.com/photo-1596755094514-f87e34085b2c', SYSUTCDATETIME()),
    (@UserId, N'Áo phông Cotton đen', 'top', 'black', 'casual', 'https://images.unsplash.com/photo-1521572267360-ee0c2909d518', SYSUTCDATETIME()),
    (@UserId, N'Quần jean ống đứng xanh', 'bottom', 'blue', 'casual', 'https://images.unsplash.com/photo-1542272604-780c96856592', SYSUTCDATETIME()),
    (@UserId, N'Quần tây âu xám', 'bottom', 'gray', 'formal', 'https://images.unsplash.com/photo-1624378439575-d8705ad7ae80', SYSUTCDATETIME()),
    (@UserId, N'Áo khoác Blazer đen', 'outerwear', 'black', 'formal', 'https://images.unsplash.com/photo-1507679799987-c73779587ccf', SYSUTCDATETIME()),
    (@UserId, N'Áo khoác Bomber xanh rêu', 'outerwear', 'green', 'street', 'https://images.unsplash.com/photo-1495105787522-5334e3ffa0ef', SYSUTCDATETIME()),
    (@UserId, N'Giày Sneaker trắng', 'shoes', 'white', 'casual', 'https://images.unsplash.com/photo-1549298916-b41d501d3772', SYSUTCDATETIME()),
    (@UserId, N'Giày da Oxford nâu', 'shoes', 'brown', 'formal', 'https://images.unsplash.com/photo-1614252235316-8c857d38b5f4', SYSUTCDATETIME()),
    (@UserId, N'Áo thun polo xám', 'top', 'gray', 'casual', 'https://images.unsplash.com/photo-1593998066526-65fcab3021a2', SYSUTCDATETIME()),
    (@UserId, N'Áo len cổ lọ đen', 'top', 'black', 'casual', 'https://images.unsplash.com/photo-1620799140408-edc6dcb6d633', SYSUTCDATETIME()),
    (@UserId, N'Áo crop top hồng', 'top', 'pink', 'street', 'https://images.unsplash.com/photo-1503342217505-b0a15ec3261c', SYSUTCDATETIME()),
    (@UserId, N'Áo tank top thể thao xanh', 'top', 'blue', 'sporty', 'https://images.unsplash.com/photo-1574680096145-d05b474e2155', SYSUTCDATETIME()),
    (@UserId, N'Quần short kaki be', 'bottom', 'beige', 'casual', 'https://images.unsplash.com/photo-1591195853828-11db59a44f6b', SYSUTCDATETIME()),
    (@UserId, N'Quần jogger xám', 'bottom', 'gray', 'sporty', 'https://images.unsplash.com/photo-1582552938357-32b906df40cb', SYSUTCDATETIME()),
    (@UserId, N'Chân váy xếp ly đen', 'bottom', 'black', 'casual', 'https://images.unsplash.com/photo-1582142407894-ec85a1260a46', SYSUTCDATETIME()),
    (@UserId, N'Quần baggy jeans xanh', 'bottom', 'blue', 'street', 'https://images.unsplash.com/photo-1542272604-780c96856592', SYSUTCDATETIME()),
    (@UserId, N'Áo khoác denim xanh', 'outerwear', 'blue', 'street', 'https://images.unsplash.com/photo-1576871337632-b9aef4c17ab9', SYSUTCDATETIME()),
    (@UserId, N'Áo cardigan len be', 'outerwear', 'beige', 'casual', 'https://images.unsplash.com/photo-1623835687702-861c8a14b0b1', SYSUTCDATETIME()),
    (@UserId, N'Áo khoác dạ nâu', 'outerwear', 'brown', 'formal', 'https://images.unsplash.com/photo-1591047139829-d91aecb6caea', SYSUTCDATETIME()),
    (@UserId, N'Áo khoác phao đen', 'outerwear', 'black', 'sporty', 'https://images.unsplash.com/photo-1551028719-00167b16eac5', SYSUTCDATETIME()),
    (@UserId, N'Váy liền công sở đen', 'dress', 'black', 'formal', 'https://images.unsplash.com/photo-1595777457583-95e059d581b8', SYSUTCDATETIME()),
    (@UserId, N'Váy hoa nhí vàng', 'dress', 'yellow', 'casual', 'https://images.unsplash.com/photo-1572804013309-59a88b7e92f1', SYSUTCDATETIME()),
    (@UserId, N'Giày chạy bộ đen', 'shoes', 'black', 'sporty', 'https://images.unsplash.com/photo-1606107557195-0e29a4b5b4aa', SYSUTCDATETIME()),
    (@UserId, N'Giày lười slip-on xanh', 'shoes', 'blue', 'casual', 'https://images.unsplash.com/photo-1507006225603-5ccbf2f4e9fa', SYSUTCDATETIME()),
    (@UserId, N'Boots da đen', 'shoes', 'black', 'street', 'https://images.unsplash.com/photo-1520639888713-7851133b1ed0', SYSUTCDATETIME()),
    (@UserId, N'Giày cao gót đỏ', 'shoes', 'red', 'formal', 'https://images.unsplash.com/photo-1543163521-1bf539c55dd2', SYSUTCDATETIME()),
    (@UserId, N'Mũ lưỡi trai trắng', 'accessory', 'white', 'sporty', 'https://images.unsplash.com/photo-1588850561407-ed78c282e89b', SYSUTCDATETIME()),
    (@UserId, N'Kính râm đen', 'accessory', 'black', 'street', 'https://images.unsplash.com/photo-1511499767150-a48a237f0083', SYSUTCDATETIME());
END
GO

PRINT N'Đã khởi tạo xong dữ liệu mẫu (Seed Data) thành công cho StyleWardrobe!';
