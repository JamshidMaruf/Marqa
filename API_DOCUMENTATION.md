# Marqa.Admin.WebApi - AuthController API Documentation

## 📚 Umumiy Ma'lumot

**Base URL:** `https://localhost/api/auth`

**Authentication:** JWT Bearer Token (Access Token)

**Request/Response Format:** JSON

---

## 🔐 Endpoints

### 1. **Login Endpoint**

#### Endpoint
```
POST /api/auth/login
```

#### Tavsif
Foydalanuvchi hisobiga kirish. Telefon raqami va parol orqali authentication qiladi.

#### Request Body
```json
{
  "phone": "string",
  "password": "string",
  "rememberMe": boolean
}
```

#### Request Field'lari

| Field | Tur | To'liqlik | Tavsif |
|-------|-----|----------|--------|
| `phone` | string | ✅ Required | Foydalanuvchining telefon raqami (masalan: `+998901234567` yoki `901234567`) |
| `password` | string | ✅ Required | Foydalanuvchining paroli (hesh qilingan) |
| `rememberMe` | boolean | ❌ Optional | `true` bo'lsa: Refresh token 30 kun amal qiladi. `false` yoki qoldirsa: 7 kun |

#### Response - Success (200 OK)
```json
{
  "statusCode": 200,
  "message": "Login successfully",
  "data": {
    "user": {
      "id": 1,
      "firstName": "Admin",
      "lastName": "User",
      "phone": "+998901234567",
      "email": "admin@marqa.uz",
      "role": "admin",
      "permissions": [
        "create_course",
        "edit_course",
        "delete_course",
        "view_students",
        "manage_employees"
      ]
    },
    "token": {
      "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "refreshToken": "k8Fj2L9mNpQrStUvWxYz1A2B3C4D5E6F7G8H9I0J...",
      "expiresIn": "2025-11-29T14:30:00Z",
      "tokenType": "Bearer"
    }
  }
}
```

#### Response - Error (400 Bad Request)
```json
{
  "statusCode": 400,
  "message": "User not found with this phone",
  "data": null
}
```

#### Response - Error (401 Unauthorized)
```json
{
  "statusCode": 401,
  "message": "Password does not match",
  "data": null
}
```

#### Response - Error (403 Forbidden)
```json
{
  "statusCode": 403,
  "message": "User is not active",
  "data": null
}
```

#### Muhim Nuqtalar
- 🔒 Parol BCrypt bilan xotirlangan bo'ladi
- 📍 Client IP manzili database'ga saqlana oladi
- 🕐 Access Token standart 24 soat amal qiladi
- 📱 RememberMe: 30 kun yoki 7 kun refresh token vaqti

---

### 2. **Logout Endpoint**

#### Endpoint
```
POST /api/auth/logout
```

#### Tavsif
Foydalanuvchi akkauntidan chiqish. Refresh token'ni revoke qiladi (bekor qiladi).

#### Request Body
```json
{
  "token": "string"
}
```

#### Request Field'lari

| Field | Tur | To'liqlik | Tavsif |
|-------|-----|----------|--------|
| `token` | string | ✅ Required | Refresh token (login qaytar'gan token) |

#### Response - Success (200 OK)
```json
{
  "statusCode": 200,
  "message": "Revoked successfully",
  "data": true
}
```

#### Response - Error (400 Bad Request)
```json
{
  "statusCode": 400,
  "message": "Invalid or expired token",
  "data": false
}
```

#### Muhim Nuqtalar
- 🔓 Token revoke qilindi - yana ishlatilmaydi
- 📍 Client IP manzili database'ga saqlana oladi
- ⚠️ Logout qilgandan keyin refresh token'dan yangi access token olish mumkin emas

---

### 3. **Refresh Token Endpoint**

#### Endpoint
```
POST /api/auth/refresh-token
```

#### Tavsif
Yangi access token olish uchun refresh token'dan foydalanish. Access token'ning muddati tugagan bo'lsa, bu endpoint orqali yangi access token olish mumkin.

#### Request Body
```json
{
  "token": "string"
}
```

#### Request Field'lari

| Field | Tur | To'liqlik | Tavsif |
|-------|-----|----------|--------|
| `token` | string | ✅ Required | Refresh token (login qaytar'gan token) |

#### Response - Success (200 OK)
```json
{
  "statusCode": 200,
  "message": "Refresh token successfully",
  "data": {
    "user": {
      "id": 1,
      "firstName": "Admin",
      "lastName": "User",
      "phone": "+998901234567",
      "email": "admin@marqa.uz",
      "role": "admin",
      "permissions": [
        "create_course",
        "edit_course",
        "delete_course",
        "view_students",
        "manage_employees"
      ]
    },
    "token": {
      "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "refreshToken": "k8Fj2L9mNpQrStUvWxYz1A2B3C4D5E6F7G8H9I0J...",
      "expiresIn": "2025-11-29T14:30:00Z",
      "tokenType": "Bearer"
    }
  }
}
```

### 4. **Current User Endpoint**

#### Endpoint
```
POST /api/auth/current-user
```

#### Tavsif
Hozirgi login qilgan foydalanuvchining ma'lumotlarini olish.

#### Request Body
```json
{
  "token": "string"
}
```

#### Request Field'lari

| Field | Tur | To'liqlik | Tavsif |
|-------|-----|----------|--------|
| `token` | string | ✅ Required | Access token yoki Refresh token |

#### Response - Success (200 OK)
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "id": 1,
    "firstName": "Admin",
    "lastName": "User",
    "phone": "+998901234567",
    "email": "admin@marqa.uz",
    "role": "admin",
    "permissions": [
      "create_course",
      "edit_course",
      "delete_course",
      "view_students",
      "manage_employees"
    ]
  }
}
```

#### Response - Error (401 Unauthorized)
```json
{
  "statusCode": 401,
  "message": "Invalid token",
  "data": null
}
```

### Token Validity
- **Access Token:** 24 soat (86,400 soniya)
- **Refresh Token:** 
  - RememberMe = true: 30 kun
  - RememberMe = false: 7 kun

---

## 🛡️ Security Headers

Barcha request'larga bu header'larni qo'shing:

```
Authorization: Bearer {accessToken}
Content-Type: application/json
```

## 💡 Best Practices

1. ✅ Access token'ni localStorage ga saqlamang, memory ga saqla
2. ✅ Refresh token'ni secure HttpOnly cookie'da saqla
3. ✅ Token expiration'ni frontend'da handle qil
4. ✅ Logout qilgandan keyin token'larni o'chir
5. ✅ Token'ni request header'ga qo'yishdan oldin validate qil
6. ✅ HTTPS dan foydalanish majburiy
7. ✅ CORS nazoratini to'g'ri sozla