# Marqa Project - Tahlili To'liq

## 📋 Loyiha Haqida

**Marqa** - bu onlayn ta'lim platformasi bo'lib, o'qituvchi, o'quvchi va ota-onalar uchun xizmat ko'rsatadi. U kompleks mikroservis arxitekturasiga asoslangan C# / ASP.NET Core bilan yozilgan.

---

## 🏗️ Loyihaning Arxitekturasi

Marqa loyihasi **Layered Architecture** (Qatlamli arxitektura) asosida qurilgan:

```
┌─────────────────────────────────────────┐
│        API Gatewaylar va Kontrollerlar   │
├─────────────────────────────────────────┤
│   Marqa.Admin.WebApi          - Bosh admin paneli
│   Marqa.Admin                 - Admin web interfeysi
│   Marqa.Teacher.WebApi        - O'qituvchilar API
│   Marqa.Student.WebApi        - O'quvchilar API  
│   Marqa.Mobile.Teacher.Api    - Mobil O'qituvchi
│   Marqa.Mobile.Student.Api    - Mobil O'quvchi
│   Marqa.Mobile.Parent.Api     - Mobil Ota-ona
├─────────────────────────────────────────┤
│         Service Layer (Marqa.Service)    │
│  - Biznes logikasi va validatsiya       │
├─────────────────────────────────────────┤
│     Data Access Layer (Marqa.DataAccess) │
│  - Repository pattern va Unit of Work   │
├─────────────────────────────────────────┤
│   Domain Layer (Marqa.Domain)            │
│  - Entity models va Enumlar             │
├─────────────────────────────────────────┤
│   Shared Layer (Marqa.Shared)            │
│  - Umumiy utilities va helpers          │
└─────────────────────────────────────────┘
```

---

## 📦 Loyihaning Asosiy Komponentlari

### 1. **Marqa.Domain** - Biznes Modellari
Barcha entity klasslari va enumlarini o'z ichiga oladi.

#### Asosiy Entitylar:
```
├── User           - Foydalanuvchi (asosiy)
├── Student        - O'quvchi
├── Employee       - Xodim (O'qituvchi, admin)
├── Course         - Kurs
├── Lesson         - Dars
├── Exam           - Imtihon
├── HomeTask       - Uy vazifasi
├── Subject        - Fanni
├── StudentCourse  - O'quvchi - Kurs bog'lanishi
├── StudentExamResult - O'quvchining imtihon natijalari
├── Order          - Buyurtma (To'lov)
├── Product        - Mahsulot (Kursi, kurslar paketi)
├── Basket         - Savat
├── Permission     - Ruxsat (Vakolat)
├── RolePermission - Rol - Ruxsat bog'lanishi
├── RefreshToken   - JWT refresh tokeni (Yangi!)
├── EmployeeRole   - Xodim roli
└── Asset          - Media/Rasm
```

**Barcha entitylar `Auditable` bazaviy klassdan voris:**
```csharp
public abstract class Auditable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
```

### 2. **Marqa.DataAccess** - Ma'lumotlar Bazi

#### Repository Pattern:
- **IRepository<TEntity>** - Umumiy interfeys
- **Repository<TEntity>** - Implementatsiya
- CRUD operatsiyalari: Insert, Update, Delete, Select

#### Unit of Work Pattern:
```csharp
IUnitOfWork unitOfWork
├── Users
├── Students
├── Courses
├── Lessons
├── Employees
├── Exams
├── Orders
├── RefreshTokens  // Yangi!
└── ... 50+ boshqa repository
```

#### Ma'lumotlar Bazasi:
- **PostgreSQL** - Asosiy database
- **Entity Framework Core** - ORM
- **Migrations** - Schema versioning
- **Soft Delete** - O'chirilgan entity'lar belgilanadi, o'chirilmaydi

### 3. **Marqa.Service** - Biznes Logikasi

#### Auth Service (Yangi Refresh Token xususiyati):
```
Services/
├── Auth/
│   ├── AuthService        - Login/Logout
│   ├── JwtService         - JWT va Refresh Token yaratish
│   ├── IAuthService
│   ├── IJwtService
│   └── Models/
│       └── LogoutModel
├── Students/              - O'quvchilar haqida
├── Courses/               - Kurs haqida
├── Employees/             - Xodimlar haqida
├── Exams/                 - Imtihonlar haqida
├── HomeTasks/             - Uy vazifasi haqida
├── Lessons/               - Darslar haqida
├── Products/              - Mahsulotlar haqida
├── Orders/                - Buyurtmalar haqida
├── Permissions/           - Ruxsatlar haqida
├── Settings/              - Sozlamalar haqida
└── Messages/              - Email/SMS xizmati
```

### 4. **API Layers** - Qabul Qatlamlar

#### Admin WebApi (`Marqa.Admin.WebApi`)
- Admin paneli uchun barcha API endpointi
- Foydalanuvchi boshqarish
- Kurslar boshqarish
- Xodimlar boshqarish

#### Teacher WebApi (`Marqa.Teacher.WebApi`)
- O'qituvchilar uchun xizmat
- Darslar yaratish va boshqarish
- Imtihonlar yaratish
- O'quvchilar evaluatsiyasi

#### Student WebApi (`Marqa.Student.WebApi`)
- O'quvchilar uchun xizmat
- Mavjud kurslarni ko'rish
- Kursga yozilish
- Uy vazifasini yuborish

#### Mobil API'lar
- `Marqa.Mobile.Teacher.Api` - O'qituvchilar uchun mobil
- `Marqa.Mobile.Student.Api` - O'quvchilar uchun mobil
- `Marqa.Mobile.Parent.Api` - Ota-onalar uchun mobil

---

## 🔐 Authentication & Authorization (Yangi!)

### JWT Token Sistema:
1. **Access Token** - Qisqa vaqt (me'yori 15-30 daqiqa)
2. **Refresh Token** - Uzoq vaqt (7-30 kun)

### Token Yaratish Jarayoni:
```csharp
// Login
var accessToken = jwtService.GenerateJwtToken(user, role);
var refreshToken = jwtService.GenerateRefreshToken();

// Refresh Token saqlash
var refreshTokenEntity = new RefreshToken
{
    UserId = userId,
    Token = refreshToken,
    ExpiresAt = DateTime.UtcNow.AddDays(7),
    CreatedByIp = clientIp
};
unitOfWork.RefreshTokens.Insert(refreshTokenEntity);
await unitOfWork.SaveAsync();
```

### Claims (Dastavallari):
- `NameIdentifier` - Foydalanuvchining ID
- `Name` - To'liq ismi
- `MobilePhone` - Telefon raqami
- `Role` - Roli (student, teacher, admin, ...)
- `Jti` - Unique token ID

---

## 🚀 Joriy Feature Branch - `feature/add-refresh-token`

### O'zgarishlar:
1. ✅ `RefreshToken` entity qo'shildi
2. ✅ JWT Service ga `GenerateRefreshToken()` metodi qo'shildi
3. ✅ Auth Service ga refresh token logikasi qo'shildi
4. ✅ Database migration qo'shildi (2,680+ satr)
5. ✅ Entity modellari yangilandi
6. ✅ `.vscode/launch.json` qo'shildi
7. ✅ Barcha API kontrollerlari refresh token bilan ishlashiga tayyorland

### Statusi:
- ✅ Barcha o'zgarishlar saqlandi
- 📊 34 file o'zgartirildi
- 📝 5,076 satr qo'shildi

---

## 📊 Database Sxemasi (Muhim Jadvallar)

```
Users (Foydalanuvchilar)
├── RefreshTokens (Refresh tokenlar) ← YANGI
├── Employees (Xodimlar)
│   ├── EmployeeRoles (Rol)
│   └── EmployeeSalaries (Maosh)
├── Students (O'quvchilar)
│   ├── StudentDetails (Qo'shimcha ma'lumotlar)
│   ├── StudentCourses (Kurslar)
│   ├── StudentExamResults (Imtihon natijalari)
│   ├── StudentHomeTasks (Uy vazifasi)
│   └── StudentPointHistories (Ballar tarixi)
├── RolePermissions (Vakolatlar)
└── Assets (Media)

Courses (Kurslar)
├── Lessons (Darslar)
│   ├── LessonAttendances (Davomilik)
│   ├── LessonFiles (Fayllar)
│   └── LessonVideos (Videolar)
├── Exams (Imtihonlar)
│   └── ExamSettings (Sozlamalar)
├── HomeTasks (Uy vazifasi)
│   └── HomeTaskFiles (Fayllar)
└── CourseWeekdays (Haftalik jadval)

Orders (Buyurtmalar)
├── OrderItems (Buyurtma elementlari)
└── Baskets (Savatlar)
    └── BasketItems (Savat elementlari)

Products (Mahsulotlar)
├── Subjects (Fanlar)
├── Companies (Kompaniyalar)
├── Banners (Bannered)
├── Settings (Sozlamalar)
└── PointSettings (Ball sozlamalari)
```

---

## 🛠️ Texnologiyalar va Framework'lar

| Qatlam | Texnologiya |
|--------|------------|
| **Runtime** | .NET 9.0 |
| **Tillar** | C# |
| **Web Framework** | ASP.NET Core |
| **ORM** | Entity Framework Core 9.0 |
| **Database** | PostgreSQL |
| **Authentication** | JWT + Refresh Token |
| **Validation** | FluentValidation |
| **Password Hashing** | BCrypt.Net-Next |
| **Logging** | Serilog |
| **API Documentation** | Swagger/OpenAPI |
| **Encryption** | Custom encryption service |

---

## 📝 Asosiy Flow'lar

### 1. **Login Flow**
```
Client → POST /auth/login (phone, password)
    ↓
AuthService: Verify user & password
    ↓
JwtService: Generate Access + Refresh Token
    ↓
Save RefreshToken to Database
    ↓
Return: { accessToken, refreshToken, user, permissions }
```

### 2. **Refresh Token Flow**
```
Client → POST /auth/refresh (refreshToken)
    ↓
AuthService: Validate refresh token
    ↓
Check expiration & revocation status
    ↓
Generate new Access Token
    ↓
Return: { newAccessToken }
```

### 3. **Logout Flow**
```
Client → POST /auth/logout (token)
    ↓
Mark token as revoked in database
    ↓
Return: Success
```

---

## 🔒 Xavfsizlik Xususiyatlari

1. **Soft Delete** - Bazadan o'chirilmaydi, faqat belgilanadi
2. **Password Hashing** - BCrypt bilan xotirlash
3. **JWT Token** - Imzoli va vaqtli cheklashli
4. **Refresh Token** - IP manzili va vaqt cheklashi
5. **Rate Limiting** - (Middleware'da)
6. **CORS** - Cross-origin requests nazorati
7. **Role-Based Access Control** - Rol-asosiy kirish nazorati

---

## 📂 Loyihaning Ko'lami

```
Total Files: 100+
Total Lines of Code: 50,000+
Projects: 11 asosiy + 1 test
Entities: 41
Services: 20+
Controllers: 50+
```

---

## 🔄 Git Branching Strategy

```
main (production)
├── feature/add-refresh-token (JORIY) ← 34 file o'zgarishi
└── other features...
```

---

## 🎯 Keyingi Qadam'lar (Tavsiyalar)

1. ✅ **Token Refresh Endpoint** - `/auth/refresh` qo'shish
2. ✅ **Token Logout** - `/auth/logout` qo'shish
3. ✅ **Token Validation Middleware** - Har request'da token tekshirish
4. ✅ **Token Expiration Handler** - Muddati o'tgan token'larni boshqarish
5. ✅ **Database Indexing** - RefreshToken jadvali uchun
6. ✅ **Unit Tests** - Auth service uchun test yozish

---

## 📖 Tushunchalar Glossariy

| Term | Ma'no |
|------|-------|
| **Entity** | Database jadvali uchun model |
| **Repository** | Ma'lumotlar bazasi operatsiyalari |
| **Unit of Work** | Bir nechta repository'larni boshqarish |
| **Service** | Biznes logikasi |
| **Controller** | API endpointi |
| **JWT** | JSON Web Token - stateless authentication |
| **Refresh Token** | Yangi access token olish uchun |
| **Middleware** | Request/Response interceptor |
| **Migration** | Database sxemasi o'zgarishi |
| **Soft Delete** | Belgilanish orqali o'chirish |

---

**Tayyorlagan:** GitHub Copilot  
**Sana:** 27 Noyabr 2025  
**Filial:** feature/add-refresh-token
