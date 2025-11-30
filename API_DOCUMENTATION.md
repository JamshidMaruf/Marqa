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

#### Example cURL
```bash
curl -X POST "https://localhost/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "phone": "+998901234567",
    "password": "SecurePassword123",
    "rememberMe": true
  }'
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

#### Example cURL
```bash
curl -X POST "https://localhost/api/auth/logout" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -d '{
    "token": "k8Fj2L9mNpQrStUvWxYz1A2B3C4D5E6F7G8H9I0J..."
  }'
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

#### Response - Error (401 Unauthorized)
```json
{
  "statusCode": 401,
  "message": "Invalid or expired refresh token",
  "data": null
}
```

#### Example cURL
```bash
curl -X POST "https://localhost/api/auth/refresh-token" \
  -H "Content-Type: application/json" \
  -d '{
    "token": "k8Fj2L9mNpQrStUvWxYz1A2B3C4D5E6F7G8H9I0J..."
  }'
```

#### Muhim Nuqtalar
- 🔄 Yangi access token + refresh token qaytariladi
- ⏰ Refresh token'ning muddati tugamagan bo'lishi kerak
- 🚫 Revoke qilingan token'dan foydalan bo'lmaydi
- 🔑 Permissions yangilangan bo'ladi

---

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

#### Example cURL
```bash
curl -X POST "https://localhost/api/auth/current-user" \
  -H "Content-Type: application/json" \
  -d '{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }'
```

#### Muhim Nuqtalar
- 👤 Foydalanuvchining hozirgi ma'lumotlarini olish
- 🔐 Token o'qirish va validatsiya qilish
- 📋 Roli va vakolatlarini qaytarish

---

## 🔑 JWT Token Tuzilishi

### Access Token
```
Header:
{
  "alg": "HS256",
  "typ": "JWT"
}

Payload:
{
  "nameid": "1",                          // User ID
  "unique_name": "Admin User",            // Full Name
  "phone": "+998901234567",               // Phone
  "role": "admin",                        // Role
  "jti": "550e8400-e29b-41d4-a716-446655440000", // JWT ID
  "exp": 1732889400,                      // Expiration Time
  "iss": "https://marqa.uz",              // Issuer
  "aud": "https://marqa.uz"               // Audience
}

Signature:
HMACSHA256(
  base64UrlEncode(header) + "." +
  base64UrlEncode(payload),
  secret
)
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

---

## ⚠️ Error Codes

| Code | Message | Tavsif |
|------|---------|--------|
| 200 | Success | Muvaffaqiyatli so'rov |
| 400 | Bad Request | Noto'g'ri request body |
| 401 | Unauthorized | Token noto'g'ri yoki muddati tugagan |
| 403 | Forbidden | Foydalanuvchi blok qilingan |
| 404 | Not Found | Foydalanuvchi topilmadi |
| 500 | Internal Server Error | Server xatosi |

---

## 📊 User Roles

| Role | Tavsif | Misol |
|------|--------|-------|
| `admin` | Tizim administratori | Barcha funksiyalar |
| `teacher` | O'qituvchi | Kurslar, darslar, imtihonlar |
| `student` | O'quvchi | Kurslar, darslar, vazifalar |
| `parent` | Ota-ona | Farzandi natijalarini ko'rish |

---

## 📝 Example Flows

### Flow 1: Login va API call
```
1. POST /auth/login
   ↓
2. Response: { accessToken, refreshToken }
   ↓
3. GET /api/courses (Header: Authorization: Bearer {accessToken})
   ↓
4. Response: Courses list
```

### Flow 2: Token refresh
```
1. Access token muddati tugadi (401 error)
   ↓
2. POST /auth/refresh-token { refreshToken }
   ↓
3. Response: { newAccessToken, newRefreshToken }
   ↓
4. GET /api/courses (Header: Authorization: Bearer {newAccessToken})
   ↓
5. Response: Courses list
```

### Flow 3: Logout
```
1. POST /auth/logout { refreshToken }
   ↓
2. Response: { success: true }
   ↓
3. POST /auth/refresh-token { refreshToken } ← Endi fail bo'ladi
   ↓
4. Response: { statusCode: 401, message: "Invalid token" }
```

---

## 🔄 Integration Guide

### Step 1: Login qilish
```javascript
const loginResponse = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    phone: '+998901234567',
    password: 'password123',
    rememberMe: true
  })
});

const { data } = await loginResponse.json();
localStorage.setItem('accessToken', data.token.accessToken);
localStorage.setItem('refreshToken', data.token.refreshToken);
```

### Step 2: API call qilish
```javascript
const response = await fetch('/api/courses', {
  headers: {
    'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
  }
});
```

### Step 3: Token refresh qilish (agar 401 bo'lsa)
```javascript
if (response.status === 401) {
  const refreshResponse = await fetch('/api/auth/refresh-token', {
    method: 'POST',
    body: JSON.stringify({
      token: localStorage.getItem('refreshToken')
    })
  });
  
  const { data } = await refreshResponse.json();
  localStorage.setItem('accessToken', data.token.accessToken);
  // Retry original request
}
```

---

## 💡 Best Practices

1. ✅ Access token'ni localStorage ga saqlamang, memory ga saqla
2. ✅ Refresh token'ni secure HttpOnly cookie'da saqla
3. ✅ Token expiration'ni frontend'da handle qil
4. ✅ Logout qilgandan keyin token'larni o'chir
5. ✅ Token'ni request header'ga qo'yishdan oldin validate qil
6. ✅ HTTPS dan foydalanish majburiy
7. ✅ CORS nazoratini to'g'ri sozla

---

## 🧪 Testing

### Postman Collection
```json
{
  "info": {
    "name": "Marqa Auth API",
    "version": "1.0.0"
  },
  "item": [
    {
      "name": "Login",
      "request": {
        "method": "POST",
        "url": "{{baseUrl}}/api/auth/login",
        "body": {
          "mode": "raw",
          "raw": "{\"phone\": \"+998901234567\", \"password\": \"password\", \"rememberMe\": true}"
        }
      }
    }
  ]
}
```