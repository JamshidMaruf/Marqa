# 👨‍🏫 Teachers API Documentation

> **Base URL:** `/api/v1`  
> **Authentication:** Bearer Token (JWT)  
> **Content-Type:** `application/json`

---

## 📋 Table of Contents

- [Teachers API](#teachers-api)
  - [Create Teacher](#1-create-teacher)
  - [Update Teacher](#2-update-teacher)
  - [Delete Teacher](#3-delete-teacher)
  - [Get Teacher by ID](#4-get-teacher-by-id)
  - [Get Teacher for Update](#5-get-teacher-for-update)
  - [Get All Teachers by Company](#6-get-all-teachers-by-company)
  - [Get Teacher Payment Types](#7-get-teacher-payment-types)
  - [Get Teachers Statistics](#8-get-teachers-statistics)
- [Enums Reference](#enums-reference)
- [Error Handling](#error-handling)

---

# Teachers API

## 1. Create Teacher

Yangi o'qituvchi yaratish.

### Endpoint
```
POST /api/v1/teachers
```

### Request Body
```json
{
  "companyId": 1,
  "firstName": "Sardor",
  "lastName": "Karimov",
  "qualification": "Senior English Teacher",
  "info": "5 yillik tajribaga ega ingliz tili o'qituvchisi",
  "dateOfBirth": "1990-05-15",
  "phone": "+998901234567",
  "email": "sardor@example.com",
  "password": "SecurePassword123",
  "gender": 1,
  "type": 1,
  "status": 1,
  "joiningDate": "2024-01-15",
  "paymentType": 1,
  "fixSalary": 5000000,
  "salaryPercentPerStudent": null,
  "salaryAmountPerHour": null
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `companyId` | integer | ✅ | Kompaniya ID |
| `firstName` | string | ✅ | O'qituvchining ismi |
| `lastName` | string | ✅ | O'qituvchining familiyasi |
| `qualification` | string | ✅ | Malakasi/mutaxassisligi |
| `info` | string | ❌ | Qo'shimcha ma'lumot |
| `dateOfBirth` | date | ✅ | Tug'ilgan sana (YYYY-MM-DD) |
| `phone` | string | ✅ | Telefon raqami (+998XXXXXXXXX) |
| `email` | string | ✅ | Email manzili |
| `password` | string | ✅ | Parol |
| `gender` | integer | ✅ | Jinsi (1: Male, 2: Female) |
| `type` | integer | ✅ | O'qituvchi turi (1: Lead, 2: Assistant) |
| `status` | integer | ✅ | Holati (1: Active, 2: Left, 3: OnLeave) |
| `joiningDate` | date | ✅ | Ishga kirgan sana (YYYY-MM-DD) |
| `paymentType` | integer | ✅ | To'lov turi (1: Fixed, 2: Percentage, 3: Hourly, 4: Mixed) |
| `fixSalary` | decimal | ❌ | Qat'iy oylik (paymentType=1 yoki 4 bo'lganda) |
| `salaryPercentPerStudent` | decimal | ❌ | Foiz (paymentType=2 yoki 4 bo'lganda) |
| `salaryAmountPerHour` | decimal | ❌ | Soatlik narx (paymentType=3 bo'lganda) |

### Success Response
```json
{
  "statusCode": 201,
  "message": "Teacher created successfully"
}
```

### Error Responses

| Status Code | Message | Description |
|-------------|---------|-------------|
| 400 | Validation failed | So'rov ma'lumotlari noto'g'ri |
| 409 | Email already exists | Bu email bilan o'qituvchi mavjud |
| 409 | Phone already exists | Bu telefon raqami bilan o'qituvchi mavjud |

---

## 2. Update Teacher

Mavjud o'qituvchi ma'lumotlarini yangilash.

### Endpoint
```
PUT /api/v1/teachers/{id}
```

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | O'qituvchi ID |

### Request Body
```json
{
  "firstName": "Sardor",
  "lastName": "Karimov",
  "qualification": "Senior English Teacher - IELTS Expert",
  "info": "7 yillik tajribaga ega ingliz tili o'qituvchisi, IELTS mutaxassisi",
  "dateOfBirth": "1990-05-15",
  "phone": "+998901234567",
  "email": "sardor.updated@example.com",
  "gender": 1,
  "type": 1,
  "status": 1,
  "joiningDate": "2024-01-15",
  "paymentType": 4,
  "fixSalary": 3000000,
  "salaryPercentPerStudent": 10,
  "salaryAmountPerHour": null
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `firstName` | string | ✅ | O'qituvchining ismi |
| `lastName` | string | ✅ | O'qituvchining familiyasi |
| `qualification` | string | ✅ | Malakasi/mutaxassisligi |
| `info` | string | ❌ | Qo'shimcha ma'lumot |
| `dateOfBirth` | date | ✅ | Tug'ilgan sana (YYYY-MM-DD) |
| `phone` | string | ✅ | Telefon raqami (+998XXXXXXXXX) |
| `email` | string | ✅ | Email manzili |
| `gender` | integer | ✅ | Jinsi (1: Male, 2: Female) |
| `type` | integer | ✅ | O'qituvchi turi (1: Lead, 2: Assistant) |
| `status` | integer | ✅ | Holati (1: Active, 2: Left, 3: OnLeave) |
| `joiningDate` | date | ✅ | Ishga kirgan sana (YYYY-MM-DD) |
| `paymentType` | integer | ✅ | To'lov turi (1: Fixed, 2: Percentage, 3: Hourly, 4: Mixed) |
| `fixSalary` | decimal | ❌ | Qat'iy oylik |
| `salaryPercentPerStudent` | decimal | ❌ | Foiz |
| `salaryAmountPerHour` | decimal | ❌ | Soatlik narx |

### Success Response
```json
{
  "statusCode": 200,
  "message": "Teacher updated successfully"
}
```

### Error Responses

| Status Code | Message | Description |
|-------------|---------|-------------|
| 400 | Validation failed | So'rov ma'lumotlari noto'g'ri |
| 404 | Teacher not found | O'qituvchi topilmadi |
| 409 | Email already exists | Bu email bilan boshqa o'qituvchi mavjud |
| 409 | Phone already exists | Bu telefon raqami bilan boshqa o'qituvchi mavjud |

---

## 3. Delete Teacher

O'qituvchini o'chirish.

### Endpoint
```
DELETE /api/v1/teachers/{id}
```

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | O'qituvchi ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "Teacher deleted successfully"
}
```

### Error Responses

| Status Code | Message | Description |
|-------------|---------|-------------|
| 404 | Teacher not found | O'qituvchi topilmadi |
| 409 | Cannot delete teacher with active courses | Faol kurslari bor o'qituvchini o'chirib bo'lmaydi |

---

## 4. Get Teacher by ID

O'qituvchini ID bo'yicha olish (batafsil ko'rish uchun).

### Endpoint
```
GET /api/v1/teachers/{id}
```

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | O'qituvchi ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "id": 1,
    "firstName": "Sardor",
    "lastName": "Karimov",
    "phone": "+998901234567",
    "email": "sardor@example.com",
    "dateOfBirth": "1990-05-15",
    "gender": {
      "id": 1,
      "name": "Erkak"
    },
    "status": {
      "id": 1,
      "name": "Faol"
    },
    "joiningDate": "2024-01-15",
    "payment": {
      "id": 1,
      "name": "Qat'iy oylik",
      "fixSalary": 5000000,
      "salaryPercentPerStudent": null,
      "salaryAmountPerHour": null
    },
    "info": "5 yillik tajribaga ega ingliz tili o'qituvchisi",
    "qualification": "Senior English Teacher",
    "courses": [
      {
        "id": 1,
        "name": "English Intermediate"
      },
      {
        "id": 2,
        "name": "IELTS Preparation"
      }
    ],
    "typeInfo": {
      "id": 1,
      "type": "Asosiy o'qituvchi"
    }
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `id` | integer | O'qituvchi ID |
| `firstName` | string | O'qituvchining ismi |
| `lastName` | string | O'qituvchining familiyasi |
| `phone` | string | Telefon raqami |
| `email` | string | Email manzili |
| `dateOfBirth` | date | Tug'ilgan sana |
| `gender` | object | Jins ma'lumoti |
| `gender.id` | integer | Jins ID |
| `gender.name` | string | Jins nomi (Erkak/Ayol) |
| `status` | object | Holat ma'lumoti |
| `status.id` | integer | Holat ID |
| `status.name` | string | Holat nomi |
| `joiningDate` | date | Ishga kirgan sana |
| `payment` | object | To'lov ma'lumoti |
| `payment.id` | integer | To'lov turi ID |
| `payment.name` | string | To'lov turi nomi |
| `payment.fixSalary` | decimal | Qat'iy oylik |
| `payment.salaryPercentPerStudent` | decimal | Foiz |
| `payment.salaryAmountPerHour` | decimal | Soatlik narx |
| `info` | string | Qo'shimcha ma'lumot |
| `qualification` | string | Malakasi |
| `courses` | array | O'qitadigan kurslar ro'yxati |
| `typeInfo` | object | O'qituvchi turi ma'lumoti |

### Error Responses

| Status Code | Message | Description |
|-------------|---------|-------------|
| 404 | Teacher not found | O'qituvchi topilmadi |

---

## 5. Get Teacher for Update

O'qituvchini tahrirlash uchun ma'lumotlarini olish.

### Endpoint
```
GET /api/v1/teachers/{id}/update
```

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `id` | integer | ✅ | O'qituvchi ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "id": 1,
    "firstName": "Sardor",
    "lastName": "Karimov",
    "phone": "+998901234567",
    "email": "sardor@example.com",
    "dateOfBirth": "1990-05-15",
    "gender": {
      "id": 1,
      "name": "Erkak"
    },
    "status": {
      "id": 1,
      "name": "Faol"
    },
    "joiningDate": "2024-01-15",
    "payment": {
      "id": 1,
      "name": "Qat'iy oylik",
      "fixSalary": 5000000,
      "salaryPercentPerStudent": null,
      "salaryAmountPerHour": null
    },
    "info": "5 yillik tajribaga ega ingliz tili o'qituvchisi",
    "qualification": "Senior English Teacher",
    "type": {
      "id": 1,
      "name": "Asosiy o'qituvchi"
    }
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `id` | integer | O'qituvchi ID |
| `firstName` | string | O'qituvchining ismi |
| `lastName` | string | O'qituvchining familiyasi |
| `phone` | string | Telefon raqami |
| `email` | string | Email manzili |
| `dateOfBirth` | date | Tug'ilgan sana |
| `gender` | object | Jins ma'lumoti |
| `status` | object | Holat ma'lumoti |
| `joiningDate` | date | Ishga kirgan sana |
| `payment` | object | To'lov ma'lumoti |
| `info` | string | Qo'shimcha ma'lumot |
| `qualification` | string | Malakasi |
| `type` | object | O'qituvchi turi ma'lumoti |

### Error Responses

| Status Code | Message | Description |
|-------------|---------|-------------|
| 404 | Teacher not found | O'qituvchi topilmadi |

---

## 6. Get All Teachers by Company

Kompaniyaga tegishli barcha o'qituvchilarni olish (pagination, search, filter bilan).

### Endpoint
```
GET /api/v1/teachers/company/{companyId}
```

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `companyId` | integer | ✅ | Kompaniya ID |

### Query Parameters

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `pageNumber` | integer | ❌ | 1 | Sahifa raqami |
| `pageSize` | integer | ❌ | 10 | Sahifadagi elementlar soni |
| `search` | string | ❌ | null | Qidiruv (ism, familiya, telefon bo'yicha) |
| `status` | integer | ❌ | null | Holat bo'yicha filter (1: Active, 2: Left, 3: OnLeave) |

### Example Requests
```
GET /api/v1/teachers/company/1
GET /api/v1/teachers/company/1?pageNumber=1&pageSize=20
GET /api/v1/teachers/company/1?search=sardor
GET /api/v1/teachers/company/1?status=1
GET /api/v1/teachers/company/1?search=sardor&status=1&pageNumber=1&pageSize=10
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 1,
      "firstName": "Sardor",
      "lastName": "Karimov",
      "phone": "+998901234567",
      "gender": {
        "id": 1,
        "name": "Erkak"
      },
      "status": {
        "id": 1,
        "name": "Faol"
      },
      "type": {
        "id": 1,
        "name": "Asosiy o'qituvchi"
      },
      "courses": [
        {
          "id": 1,
          "name": "English Intermediate"
        },
        {
          "id": 2,
          "name": "IELTS Preparation"
        }
      ]
    },
    {
      "id": 2,
      "firstName": "Malika",
      "lastName": "Rahimova",
      "phone": "+998907654321",
      "gender": {
        "id": 2,
        "name": "Ayol"
      },
      "status": {
        "id": 1,
        "name": "Faol"
      },
      "type": {
        "id": 2,
        "name": "Yordamchi o'qituvchi"
      },
      "courses": [
        {
          "id": 1,
          "name": "English Intermediate"
        }
      ]
    }
  ]
}
```

### Response Headers (Pagination)

| Header | Type | Description |
|--------|------|-------------|
| `X-Pagination` | JSON | Pagination ma'lumotlari |

```json
{
  "TotalCount": 25,
  "PageSize": 10,
  "CurrentPage": 1,
  "TotalPages": 3,
  "HasNext": true,
  "HasPrevious": false
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `id` | integer | O'qituvchi ID |
| `firstName` | string | O'qituvchining ismi |
| `lastName` | string | O'qituvchining familiyasi |
| `phone` | string | Telefon raqami |
| `gender` | object | Jins ma'lumoti |
| `status` | object | Holat ma'lumoti |
| `type` | object | O'qituvchi turi ma'lumoti |
| `courses` | array | O'qitadigan kurslar ro'yxati |

---

## 7. Get Teacher Payment Types

O'qituvchi to'lov turlarini olish (dropdown uchun).

### Endpoint
```
GET /api/v1/teachers/teachers/payment-types
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 1,
      "name": "Qat'iy oylik"
    },
    {
      "id": 2,
      "name": "Foiz (o'quvchilardan)"
    },
    {
      "id": 3,
      "name": "Soatlik"
    },
    {
      "id": 4,
      "name": "Aralash (oylik + foiz)"
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `id` | integer | To'lov turi ID |
| `name` | string | To'lov turi nomi |

---

## 8. Get Teachers Statistics

Kompaniya o'qituvchilari statistikasini olish.

### Endpoint
```
GET /api/v1/teachers/company/{companyId}/statistics
```

### Path Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `companyId` | integer | ✅ | Kompaniya ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "totalTeachersCount": 25,
    "totalLeadTeachersCount": 15,
    "totalAssistantTeachersCount": 10,
    "totalActiveTeacherCount": 20,
    "totalLeftTeacherCount": 3,
    "totalOnLeaveTeacherCount": 2
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `totalTeachersCount` | integer | Jami o'qituvchilar soni |
| `totalLeadTeachersCount` | integer | Asosiy o'qituvchilar soni |
| `totalAssistantTeachersCount` | integer | Yordamchi o'qituvchilar soni |
| `totalActiveTeacherCount` | integer | Faol o'qituvchilar soni |
| `totalLeftTeacherCount` | integer | Ishdan ketgan o'qituvchilar soni |
| `totalOnLeaveTeacherCount` | integer | Ta'tildagi o'qituvchilar soni |

---

# Enums Reference

## Gender (Jins)

| ID | Value | Description |
|----|-------|-------------|
| 1 | Male | Erkak |
| 2 | Female | Ayol |

## TeacherType (O'qituvchi turi)

| ID | Value | Description |
|----|-------|-------------|
| 1 | Lead | Asosiy o'qituvchi |
| 2 | Assistant | Yordamchi o'qituvchi |

## TeacherStatus (O'qituvchi holati)

| ID | Value | Description |
|----|-------|-------------|
| 1 | Active | Faol |
| 2 | Left | Chiqib ketgan |
| 3 | OnLeave | Ta'tilda |

## TeacherPaymentType (To'lov turi)

| ID | Value | Description |
|----|-------|-------------|
| 1 | Fixed | Qat'iy oylik |
| 2 | Percentage | Foiz (o'quvchilardan) |
| 3 | Hourly | Soatlik |
| 4 | Mixed | Aralash (oylik + foiz) |

---

# Error Handling

## Standard Error Response Format

```json
{
  "statusCode": 400,
  "message": "Error message here",
  "errors": [
    {
      "field": "fieldName",
      "message": "Field specific error message"
    }
  ]
}
```

## Common HTTP Status Codes

| Status Code | Description |
|-------------|-------------|
| 200 | OK - So'rov muvaffaqiyatli bajarildi |
| 201 | Created - Yangi resurs yaratildi |
| 400 | Bad Request - So'rov noto'g'ri |
| 401 | Unauthorized - Autentifikatsiya talab qilinadi |
| 403 | Forbidden - Ruxsat yo'q |
| 404 | Not Found - Resurs topilmadi |
| 409 | Conflict - Ziddiyat mavjud (duplicate ma'lumot) |
| 500 | Internal Server Error - Server xatosi |

---

# Usage Examples

## cURL Examples

### Create Teacher
```bash
curl -X POST "https://api.example.com/api/v1/teachers" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "companyId": 1,
    "firstName": "Sardor",
    "lastName": "Karimov",
    "qualification": "Senior English Teacher",
    "info": "5 yillik tajriba",
    "dateOfBirth": "1990-05-15",
    "phone": "+998901234567",
    "email": "sardor@example.com",
    "password": "SecurePassword123",
    "gender": 1,
    "type": 1,
    "status": 1,
    "joiningDate": "2024-01-15",
    "paymentType": 1,
    "fixSalary": 5000000
  }'
```

### Get All Teachers with Filters
```bash
curl -X GET "https://api.example.com/api/v1/teachers/company/1?search=sardor&status=1&pageNumber=1&pageSize=10" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Get Teacher by ID
```bash
curl -X GET "https://api.example.com/api/v1/teachers/1" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

### Update Teacher
```bash
curl -X PUT "https://api.example.com/api/v1/teachers/1" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Sardor",
    "lastName": "Karimov",
    "qualification": "Senior English Teacher - IELTS Expert",
    "info": "7 yillik tajriba",
    "dateOfBirth": "1990-05-15",
    "phone": "+998901234567",
    "email": "sardor@example.com",
    "gender": 1,
    "type": 1,
    "status": 1,
    "joiningDate": "2024-01-15",
    "paymentType": 4,
    "fixSalary": 3000000,
    "salaryPercentPerStudent": 10
  }'
```

### Delete Teacher
```bash
curl -X DELETE "https://api.example.com/api/v1/teachers/1" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

---

## JavaScript/Fetch Examples

### Create Teacher
```javascript
const createTeacher = async (teacherData) => {
  const response = await fetch('/api/v1/teachers', {
    method: 'POST',
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(teacherData)
  });
  return response.json();
};
```

### Get All Teachers with Pagination
```javascript
const getTeachers = async (companyId, params = {}) => {
  const queryParams = new URLSearchParams({
    pageNumber: params.pageNumber || 1,
    pageSize: params.pageSize || 10,
    ...(params.search && { search: params.search }),
    ...(params.status && { status: params.status })
  });

  const response = await fetch(
    `/api/v1/teachers/company/${companyId}?${queryParams}`,
    {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    }
  );
  
  const pagination = JSON.parse(response.headers.get('X-Pagination'));
  const data = await response.json();
  
  return { data, pagination };
};
```

### Get Teacher Statistics
```javascript
const getStatistics = async (companyId) => {
  const response = await fetch(
    `/api/v1/teachers/company/${companyId}/statistics`,
    {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    }
  );
  return response.json();
};
```

---

> 📌 **Note:** Barcha so'rovlar uchun `Authorization` header'ida valid JWT token bo'lishi shart.

> 🔐 **Security:** Parol minimal 8 ta belgi, kamida 1 ta katta harf, 1 ta kichik harf va 1 ta raqamdan iborat bo'lishi kerak.

