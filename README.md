# TodoList

Ứng dụng quản lý công việc (todo/kanban) gồm 2 project:

| Project           | Vai trò                     | Công nghệ                                   |
|-------------------|-----------------------------|---------------------------------------------|
| **TodoList.Api**  | REST API + truy cập dữ liệu | ASP.NET Core (.NET 10), EF Core, SQL Server |
| **TodoList.Web**  | Giao diện người dùng (SPA)  | Blazor WebAssembly, Bootstrap               |

Frontend gọi backend qua HTTP. Hai project chạy độc lập, mỗi cái một cổng riêng.

---

## Kiến trúc

**Backend** phân tầng rõ ràng:

```
Controller  →  Service  →  UnitOfWork  →  Repository  →  EF Core / SQL Server
   (HTTP)      (nghiệp vụ)   (gom transaction)  (truy vấn)
```

- **DTOs** — tách model vào/ra khỏi entity (`Create*`, `Update*`, `*ResponseDto`).
- **Exceptions** — `NotFoundException` (404), `BadRequestException` (400), `ConflictException` (409).
- **ExceptionMiddleware** — bắt mọi lỗi, map ra status code + JSON `{ "message": ... }` đồng nhất. Lỗi ngoài dự kiến (500) chỉ trả message chung, chi tiết được ghi vào log.

**Frontend**: mọi lời gọi API đi qua đúng một service `TodoApiService` (`Services/ITodoApiService.cs`); các trang (`Pages/`) chỉ dựng UI, không gọi `HttpClient` trực tiếp.

---

## Yêu cầu

- [.NET SDK 10](https://dotnet.microsoft.com/download) trở lên
- **SQL Server** (Express / LocalDB / Developer đều được)
- (khuyến nghị) Visual Studio 2022+ hoặc VS Code

---

## Cài đặt & chạy

### 1. Cấu hình chuỗi kết nối (bắt buộc)

`appsettings.json` **cố tình để trống** `ConnectionStrings:Default` — không commit chuỗi kết nối vào Git.
Chuỗi kết nối được nạp qua **User Secrets** (đã bật sẵn `UserSecretsId` trong `TodoList.Api.csproj`).

Chạy trong thư mục `TodoList.Api`:

```powershell
cd TodoList.Api
dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost;Database=TodoListDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

> Đổi `Server=...` cho đúng SQL Server của bạn. Nếu dùng LocalDB: `Server=(localdb)\\MSSQLLocalDB`.
> Nếu dùng SQL auth: thêm `User Id=...;Password=...;` thay cho `Trusted_Connection`.

### 2. Tạo database (chạy migration)

Cần EF Core CLI (chỉ cần cài 1 lần): `dotnet tool install --global dotnet-ef`

```powershell
cd TodoList.Api
dotnet ef database update
```

### 3. Chạy Backend API

```powershell
cd TodoList.Api
dotnet run
```
API chạy tại **https://localhost:7118** (OpenAPI ở `/openapi/v1.json` khi ở môi trường Development).

### 4. Chạy Frontend (terminal khác)

```powershell
cd TodoList.Web
dotnet watch run      # có hot reload
```
Web mở tại **https://localhost:7269** → vào đường dẫn **`/board`** để dùng giao diện chính.

> Frontend đang trỏ cứng tới API `https://localhost:7118` trong `TodoList.Web/Program.cs`.
> CORS ở backend (`Program.cs`) đã cho phép sẵn các origin `7269`/`5161` của Web.

---

## API endpoints

Tất cả trả JSON, enum trả dạng chuỗi.

### TodoItems — `api/todoitems`
| Method | Route                        | Mô tả                                        |
|--------|------------------------------|----------------------------------------------|
| GET    | `/api/todoitems`             | Lấy tất cả                                   |
| GET    | `/api/todoitems/{id}`        | Lấy 1 item                                   |
| POST   | `/api/todoitems`             | Tạo mới → 201 + object vừa tạo               |
| PUT    | `/api/todoitems/{id}`        | Cập nhật toàn phần                           |
| PUT    | `/api/todoitems/{id}/status` | Đổi trạng thái (body `{ "status": "Done" }`) |
| DELETE | `/api/todoitems/{id}`        | Xóa                                          |

**Status:** `Pending` · `InProgress` · `Done` · `Overdue`
(`Overdue` được suy ra ở UI từ `DueDate < hôm nay`, không lưu trong DB.)

### Tags — `api/tags` · Priorities — `api/priorities`
Đủ CRUD (`GET` all/by-id, `POST`, `PUT`, `DELETE`). Ràng buộc:
- **Tag** không cho trùng tên (trả **409** nếu trùng).
- **Priority** đang được task dùng thì không xóa được (trả **409**).

---

## Cấu trúc thư mục

```
TodoList.Api/
  Controllers/      # endpoint HTTP (mỏng)
  Services/         # logic nghiệp vụ
  Repositories/     # truy vấn EF Core (+ GenericRepository dùng chung)
  UnitOfWorks/      # gom repository + SaveChanges
  Models/Entities/  # entity map với DB
  DTOs/             # object vào/ra API
  Exceptions/       # NotFound / BadRequest / Conflict
  Middleware/       # ExceptionMiddleware
  Data/             # AppDbContext
  Migrations/       # migration EF Core

TodoList.Web/
  Pages/            # Board.razor là giao diện chính (Board/List/Week/Stats)
  Services/         # TodoApiService — nơi duy nhất gọi API
  Models/           # DTO phía client
  Layout/           # MainLayout / BlankLayout
  CLAUDE.md         # hướng dẫn & nhật ký phát triển frontend
```

---

## Ghi chú

- Chưa có xác thực/phân quyền — thiết kế cho một người dùng cục bộ.
- Chưa có project test.
- API `GET /api/todoitems` hiện trả toàn bộ danh sách (chưa phân trang) — hợp lý ở quy mô nhỏ.
