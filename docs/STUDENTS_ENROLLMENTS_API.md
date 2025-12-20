# 📚 Students & Enrollments API Documentation

> **Base URL:** `/api/v1`  
> **Authentication:** Bearer Token (JWT)  
> **Content-Type:** `application/json`

---

## 📋 Table of Contents

- [Students API](#students-api)
  - [Create Student](#1-create-student)
  - [Update Student](#2-update-student)
  - [Delete Student](#3-delete-student)
  - [Get Student by ID](#4-get-student-by-id)
  - [Get Student for Update](#5-get-student-for-update)
  - [Get All Students](#6-get-all-students)
  - [Get Student Courses](#7-get-student-courses)
  - [Get Unenrolled Courses](#8-get-unenrolled-courses)
  - [Update Student Course Status](#9-update-student-course-status)
- [Enrollments API](#enrollments-api)
  - [Attach Student to Course](#1-attach-student-to-course)
  - [Detach Student from Course](#2-detach-student-from-course)
  - [Freeze Enrollment](#3-freeze-enrollment)
  - [Unfreeze Enrollment](#4-unfreeze-enrollment)
  - [Transfer Student](#5-transfer-student)
  - [Get Active Courses](#6-get-active-courses)
  - [Get Frozen Courses](#7-get-frozen-courses)
  - [Get Enrollment Statuses](#8-get-enrollment-statuses)
- [Enums Reference](#enums-reference)
- [Error Handling](#error-handling)

---

# Students API

## 1. Create Student

Yangi talaba yaratish.

### Endpoint
```
POST /api/v1/students
```

### Request Body
```json
{
  "firstName": "Ali",
  "lastName": "Valiyev",
  "phone": "+998901234567",
  "email": "ali@example.com",
  "dateOfBirth": "2005-03-15",
  "gender": 1,
  "companyId": 1,
  "studentDetailCreateModel": {
    "fatherFirstName": "Vali",
    "fatherLastName": "Valiyev",
    "fatherPhone": "+998901111111",
    "motherFirstName": "Malika",
    "motherLastName": "Valiyeva",
    "motherPhone": "+998902222222",
    "guardianFirstName": null,
    "guardianLastName": null,
    "guardianPhone": null
  }
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `firstName` | string | ✅ | Talabaning ismi |
| `lastName` | string | ✅ | Talabaning familiyasi |
| `phone` | string | ✅ | Telefon raqami (+998XXXXXXXXX) |
| `email` | string | ❌ | Email manzili |
| `dateOfBirth` | date | ✅ | Tug'ilgan sana (YYYY-MM-DD) |
| `gender` | integer | ✅ | Jinsi (0: Male, 1: Female) |
| `companyId` | integer | ✅ | Kompaniya ID |
| `studentDetailCreateModel` | object | ❌ | Ota-ona ma'lumotlari |

### StudentDetailCreateModel Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `fatherFirstName` | string | ❌ | Otaning ismi |
| `fatherLastName` | string | ❌ | Otaning familiyasi |
| `fatherPhone` | string | ❌ | Otaning telefoni |
| `motherFirstName` | string | ❌ | Onaning ismi |
| `motherLastName` | string | ❌ | Onaning familiyasi |
| `motherPhone` | string | ❌ | Onaning telefoni |
| `guardianFirstName` | string | ❌ | Vasiyning ismi |
| `guardianLastName` | string | ❌ | Vasiyning familiyasi |
| `guardianPhone` | string | ❌ | Vasiyning telefoni |

### Success Response
```json
{
  "statusCode": 200,
  "message": "Student created successfully",
  "data": null
}
```

### Error Responses

| Status | Description |
|--------|-------------|
| 400 | Validation error (noto'g'ri ma'lumot) |
| 409 | Student already exists (telefon raqami mavjud) |
| 500 | Server error |

---

## 2. Update Student

Mavjud talaba ma'lumotlarini yangilash.

### Endpoint
```
PUT /api/v1/students/{id}
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Talaba ID |

### Request Body
```json
{
  "firstName": "Ali",
  "lastName": "Valiyev",
  "dateOfBirth": "2005-03-15",
  "gender": 1,
  "phone": "+998901234567",
  "email": "ali@example.com",
  "courses": [
    {
      "courseId": 1,
      "courseStatusId": 1
    }
  ],
  "studentDetailUpdateModel": {
    "fatherFirstName": "Vali",
    "fatherLastName": "Valiyev",
    "fatherPhone": "+998901111111",
    "motherFirstName": "Malika",
    "motherLastName": "Valiyeva",
    "motherPhone": "+998902222222",
    "guardianFirstName": null,
    "guardianLastName": null,
    "guardianPhone": null
  }
}
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "Student updated successfully",
  "data": null
}
```

### Error Responses

| Status | Description |
|--------|-------------|
| 400 | Validation error |
| 404 | Student not found |
| 409 | Phone number already exists |
| 500 | Server error |

---

## 3. Delete Student

Talabani o'chirish (soft delete).

### Endpoint
```
DELETE /api/v1/students/{id}
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Talaba ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "Student deleted successfully",
  "data": null
}
```

### Error Responses

| Status | Description |
|--------|-------------|
| 404 | Student not found |
| 500 | Server error |

---

## 4. Get Student by ID

Talaba ma'lumotlarini to'liq olish.

### Endpoint
```
GET /api/v1/students/{id}
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Talaba ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "id": 1,
    "firstName": "Ali",
    "lastName": "Valiyev",
    "dateOfBirth": "2005-03-15",
    "gender": 1,
    "status": 1,
    "phone": "+998901234567",
    "email": "ali@example.com",
    "balance": 500000.00,
    "totalPoints": 150.5,
    "detail": {
      "fatherFirstName": "Vali",
      "fatherLastName": "Valiyev",
      "fatherPhone": "+998901111111",
      "motherFirstName": "Malika",
      "motherLastName": "Valiyeva",
      "motherPhone": "+998902222222",
      "guardianFirstName": null,
      "guardianLastName": null,
      "guardianPhone": null
    },
    "courses": [
      {
        "courseName": "English Beginner",
        "subject": "English",
        "teacherFullName": "John Smith",
        "courseStatusName": "Active",
        "courseLevel": "Beginner"
      }
    ],
    "examResults": [
      {
        "title": "Monthly Test - January",
        "score": 85.5
      }
    ],
    "paymentOperations": [
      {
        "paymentNumber": "PAY-001",
        "paymentMethod": 1,
        "amount": 500000.00,
        "dateTime": "2024-01-15T10:30:00Z",
        "description": "Monthly fee",
        "paymentOperationType": 1,
        "coursePrice": 500000.00
      }
    ],
    "pointHistories": [
      {
        "givenPoint": 10,
        "currentPoint": 150,
        "note": "Perfect attendance",
        "operation": 1,
        "givenDate": "2024-01-20T14:00:00Z"
      }
    ]
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `id` | integer | Talaba ID |
| `firstName` | string | Ism |
| `lastName` | string | Familiya |
| `dateOfBirth` | date | Tug'ilgan sana |
| `gender` | integer | Jins (0: Male, 1: Female) |
| `status` | integer | Talaba holati ([StudentStatus](#studentstatus)) |
| `phone` | string | Telefon |
| `email` | string | Email |
| `balance` | decimal | Balans (so'm) |
| `totalPoints` | number | Jami ballar |
| `detail` | object | Ota-ona ma'lumotlari |
| `courses` | array | Kurslar ro'yxati |
| `examResults` | array | Imtihon natijalari |
| `paymentOperations` | array | To'lov tarixi |
| `pointHistories` | array | Ball tarixi |

---

## 5. Get Student for Update

Talaba ma'lumotlarini tahrirlash uchun olish.

### Endpoint
```
GET /api/v1/students/{id}/update
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Talaba ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "id": 1,
    "firstName": "Ali",
    "lastName": "Valiyev",
    "phone": "+998901234567",
    "email": "ali@example.com",
    "gender": 1,
    "status": 1,
    "dateOfBirth": "2005-03-15",
    "fatherFirstName": "Vali",
    "fatherLastName": "Valiyev",
    "fatherPhone": "+998901111111",
    "motherFirstName": "Malika",
    "motherLastName": "Valiyeva",
    "motherPhone": "+998902222222",
    "guardianFirstName": null,
    "guardianLastName": null,
    "guardianPhone": null,
    "courses": [
      {
        "courseId": 1,
        "courseName": "English Beginner",
        "courseStatusId": 1,
        "courseStatusName": "Active"
      }
    ]
  }
}
```

---

## 6. Get All Students

Barcha talabalar ro'yxatini filtrlash bilan olish.

### Endpoint
```
GET /api/v1/students
```

### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `companyId` | integer | ✅ | Kompaniya ID |
| `searchText` | string | ❌ | Qidiruv (ism, familiya, telefon) |
| `courseId` | integer | ❌ | Kurs bo'yicha filter |
| `status` | integer | ❌ | Holat bo'yicha filter ([StudentFilteringStatus](#studentfilteringstatus)) |

### Example Request
```
GET /api/v1/students?companyId=1&searchText=Ali&courseId=5&status=1
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 1,
      "firstName": "Ali",
      "lastName": "Valiyev",
      "phone": "+998901234567",
      "balance": 500000.00,
      "status": 1,
      "courses": [
        {
          "courseId": 1,
          "courseName": "English Beginner",
          "courseStatus": "Active"
        }
      ]
    }
  ]
}
```

---

## 7. Get Student Courses

Talabaning barcha kurslarini olish.

### Endpoint
```
GET /api/v1/students/{studentId}/courses
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `studentId` | integer | Talaba ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 1,
      "name": "English Beginner"
    },
    {
      "id": 2,
      "name": "Math Advanced"
    }
  ]
}
```

---

## 8. Get Unenrolled Courses

Talaba ro'yxatdan o'tmagan kurslarni olish.

### Endpoint
```
GET /api/v1/students/{studentId}/unenrolled-courses
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `studentId` | integer | Talaba ID |

### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `companyId` | integer | ✅ | Kompaniya ID |

### Example Request
```
GET /api/v1/students/1/unenrolled-courses?companyId=1
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 3,
      "name": "Physics Intermediate",
      "teachersFullName": ["Dr. Johnson", "Prof. Williams"],
      "maxStudentCount": 20,
      "enrolledStudentCount": 15,
      "coursePrice": 600000.00
    }
  ]
}
```

---

## 9. Update Student Course Status

Talabaning kurs holatini yangilash.

### Endpoint
```
PUT /api/v1/students/{studentId}/courses/{courseId}/status/{statusId}
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `studentId` | integer | Talaba ID |
| `courseId` | integer | Kurs ID |
| `statusId` | integer | Yangi holat ID ([EnrollmentStatus](#enrollmentstatus)) |

### Success Response
```json
{
  "statusCode": 200,
  "message": "Student Course status updated successfully",
  "data": null
}
```

---

# Enrollments API

## 1. Attach Student to Course

Talabani kursga biriktirish.

### Endpoint
```
POST /api/v1/enrollments/attach
```

### Request Body
```json
{
  "studentId": 1,
  "courseId": 5,
  "enrollmentDate": "2024-01-15T00:00:00Z",
  "status": 1,
  "paymentType": 1,
  "amount": 500000.00
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `studentId` | integer | ✅ | Talaba ID |
| `courseId` | integer | ✅ | Kurs ID |
| `enrollmentDate` | datetime | ✅ | Ro'yxatdan o'tish sanasi |
| `status` | integer | ✅ | Holat ([EnrollmentStatus](#enrollmentstatus)) |
| `paymentType` | integer | ✅ | To'lov turi ([CoursePaymentType](#coursepaymenttype)) |
| `amount` | decimal | ✅ | To'lov miqdori |

### Success Response
```json
{
  "statusCode": 201,
  "message": "Enrollment has been created successfully",
  "data": null
}
```

### Error Responses

| Status | Description |
|--------|-------------|
| 400 | Validation error |
| 404 | Student or Course not found |

---

## 2. Detach Student from Course

Talabani kursdan ajratish.

### Endpoint
```
POST /api/v1/enrollments/detach
```

### Request Body
```json
{
  "studentId": 1,
  "courseIds": [5, 6],
  "reason": "O'qishni to'xtatdi",
  "deactivatedDate": "2024-03-15T00:00:00Z"
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `studentId` | integer | ✅ | Talaba ID |
| `courseIds` | array[integer] | ✅ | Kurs ID'lar ro'yxati |
| `reason` | string | ✅ | Sabab |
| `deactivatedDate` | datetime | ✅ | Ajratilish sanasi |

### Success Response
```json
{
  "statusCode": 201,
  "message": "Enrollment has been deleted successfully",
  "data": null
}
```

---

## 3. Freeze Enrollment

Talabaning kursini muzlatish.

### Endpoint
```
POST /api/v1/enrollments/freeze
```

### Request Body
```json
{
  "studentId": 1,
  "courseIds": [5],
  "startDate": "2024-02-01T00:00:00Z",
  "endDate": "2024-03-01T00:00:00Z",
  "isInDefinite": false,
  "reason": "Kasallik sababli"
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `studentId` | integer | ✅ | Talaba ID |
| `courseIds` | array[integer] | ✅ | Muzlatiladigan kurs ID'lar |
| `startDate` | datetime | ✅ | Muzlatish boshlanish sanasi |
| `endDate` | datetime | ❌ | Muzlatish tugash sanasi |
| `isInDefinite` | boolean | ✅ | Muddatsiz muzlatish (true bo'lsa endDate null) |
| `reason` | string | ✅ | Sabab |

### Success Response
```json
{
  "statusCode": 201,
  "message": "Enrollment has been frozen successfully",
  "data": null
}
```

---

## 4. Unfreeze Enrollment

Muzlatilgan kursni qayta faollashtirish.

### Endpoint
```
POST /api/v1/enrollments/unfreeze
```

### Request Body
```json
{
  "studentId": 1,
  "courseIds": [5],
  "activateDate": "2024-03-01T00:00:00Z"
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `studentId` | integer | ✅ | Talaba ID |
| `courseIds` | array[integer] | ✅ | Faollashtirilgan kurs ID'lar |
| `activateDate` | datetime | ✅ | Faollashtirish sanasi |

### Success Response
```json
{
  "statusCode": 201,
  "message": "Enrollment has been unfrozen successfully",
  "data": null
}
```

---

## 5. Transfer Student

Talabani bir kursdan boshqasiga o'tkazish.

### Endpoint
```
POST /api/v1/enrollments/transfer
```

### Request Body
```json
{
  "studentId": 1,
  "fromCourseId": 5,
  "toCourseId": 6,
  "dateOfTransfer": "2024-02-15T00:00:00Z",
  "status": 1,
  "paymentType": 1,
  "amount": 0,
  "reason": "Darajasi oshdi"
}
```

### Request Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `studentId` | integer | ✅ | Talaba ID |
| `fromCourseId` | integer | ✅ | Joriy kurs ID |
| `toCourseId` | integer | ✅ | Yangi kurs ID |
| `dateOfTransfer` | datetime | ✅ | O'tkazish sanasi |
| `status` | integer | ✅ | Yangi holat ([EnrollmentStatus](#enrollmentstatus)) |
| `paymentType` | integer | ✅ | To'lov turi |
| `amount` | decimal | ✅ | Qo'shimcha to'lov (0 bo'lishi mumkin) |
| `reason` | string | ✅ | Sabab |

### Success Response
```json
{
  "statusCode": 201,
  "message": "Student has been transferred successfully",
  "data": null
}
```

---

## 6. Get Active Courses

Talabaning faol kurslarini olish.

### Endpoint
```
GET /api/v1/enrollments/{studentId}/active-courses
```

### Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `studentId` | integer | Talaba ID |

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 1,
      "name": "English Beginner",
      "level": "Beginner"
    },
    {
      "id": 2,
      "name": "Math Intermediate",
      "level": "Intermediate"
    }
  ]
}
```

---

## 7. Get Frozen Courses

Talabaning muzlatilgan kurslarini olish.

### Endpoint
```
GET /api/v1/enrollments/frozen-courses
```

### Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `studentId` | integer | ✅ | Talaba ID |

### Example Request
```
GET /api/v1/enrollments/frozen-courses?studentId=1
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": [
    {
      "id": 3,
      "name": "Physics Advanced",
      "level": "Advanced",
      "frozenDate": "2024-02-01T00:00:00Z",
      "endDate": "2024-03-01T00:00:00Z",
      "reason": "Kasallik sababli"
    }
  ]
}
```

---

## 8. Get Enrollment Statuses

Mavjud enrollment statuslarni olish (dropdown uchun).

### Endpoint
```
GET /api/v1/enrollments/specific-enrollment-statuses
```

### Success Response
```json
{
  "statusCode": 200,
  "message": "success",
  "data": {
    "statuses": [
      { "id": 1, "name": "Active" },
      { "id": 2, "name": "Frozen" },
      { "id": 3, "name": "Completed" },
      { "id": 4, "name": "Cancelled" }
    ]
  }
}
```

---

# Enums Reference

## Gender
```
0 = Male (Erkak)
1 = Female (Ayol)
```

## StudentStatus
```
1 = Active (Faol)
2 = Inactive (Faol emas)
3 = Graduated (Bitirgan)
4 = Expelled (Chiqarilgan)
```

## StudentFilteringStatus
```
1 = All (Hammasi)
2 = Active (Faol)
3 = Inactive (Faol emas)
4 = HasDebt (Qarzdor)
```

## EnrollmentStatus
```
1 = Active (Faol)
2 = Frozen (Muzlatilgan)
3 = Completed (Tugatilgan)
4 = Cancelled (Bekor qilingan)
```

## CoursePaymentType
```
1 = Monthly (Oylik)
2 = PerLesson (Dars uchun)
3 = Full (To'liq)
```

## PaymentMethod
```
1 = Cash (Naqd)
2 = Card (Karta)
3 = Transfer (O'tkazma)
```

## PaymentOperationType
```
1 = Income (Kirim)
2 = Expense (Chiqim)
3 = Refund (Qaytarish)
```

## PointHistoryOperation
```
1 = Add (Qo'shish)
2 = Subtract (Ayirish)
```

---

# Error Handling

## Standard Error Response
```json
{
  "statusCode": 400,
  "message": "Error description",
  "data": null
}
```

## HTTP Status Codes

| Code | Description |
|------|-------------|
| 200 | OK - Muvaffaqiyatli |
| 201 | Created - Yaratildi |
| 400 | Bad Request - Noto'g'ri so'rov |
| 401 | Unauthorized - Avtorizatsiya talab qilinadi |
| 403 | Forbidden - Ruxsat yo'q |
| 404 | Not Found - Topilmadi |
| 409 | Conflict - Ziddiyat (masalan, mavjud telefon) |
| 500 | Server Error - Server xatosi |

## Validation Errors
```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": {
    "firstName": ["First name is required"],
    "phone": ["Phone number is not valid"]
  }
}
```

---

# Usage Examples

## TypeScript/JavaScript (Axios)

```typescript
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://api.marqa.uz/api/v1',
  headers: {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  }
});

// Get all students
const getStudents = async (companyId: number, search?: string) => {
  const { data } = await api.get('/students', {
    params: { companyId, searchText: search }
  });
  return data.data;
};

// Create student
const createStudent = async (student: StudentCreateModel) => {
  const { data } = await api.post('/students', student);
  return data;
};

// Attach to course
const attachToCourse = async (enrollment: EnrollmentCreateModel) => {
  const { data } = await api.post('/enrollments/attach', enrollment);
  return data;
};

// Freeze enrollment
const freezeEnrollment = async (freezeData: FreezeModel) => {
  const { data } = await api.post('/enrollments/freeze', freezeData);
  return data;
};
```

## React Query Example

```typescript
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';

// Fetch students
export const useStudents = (companyId: number, filters?: StudentFilterModel) => {
  return useQuery({
    queryKey: ['students', companyId, filters],
    queryFn: () => getStudents(companyId, filters),
  });
};

// Create student mutation
export const useCreateStudent = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: createStudent,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['students'] });
    },
  });
};
```

---

> **Last Updated:** December 20, 2024  
> **Version:** 1.0.0  
> **Contact:** [development@marqa.uz](mailto:development@marqa.uz)

