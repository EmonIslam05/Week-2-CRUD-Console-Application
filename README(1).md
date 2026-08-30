# Week 2 Task: Developing a CRUD Console Application

## Student Management System

A C# console application demonstrating CRUD (Create, Read, Update, Delete) operations using an in-memory `List<Student>`. The application also includes input validation, exception handling, and file-based logging.

## Features

- Create student records
- Read/view all student records
- Update an existing student
- Delete a student
- Student fields:
  - Student ID
  - Name
  - Age
  - Course
- Prevents duplicate Student IDs
- Validates required text and numeric input
- Handles errors without crashing the application
- Logs create, update, and delete operations to `application.log`
- Uses clean, object-oriented C# code

## Requirements

- .NET 8 SDK or later
- Windows, macOS, or Linux terminal

## How to Run

1. Open a terminal in the `StudentManagementApp` folder.
2. Check the .NET installation:

```bash
dotnet --version
```

3. Run the application:

```bash
dotnet run
```

4. Use the menu:

```text
1. Create Student
2. Read Students
3. Update Student
4. Delete Student
5. Exit
```

## Logging

The application automatically creates `application.log` in the application's runtime/output directory. Successful create, update, and delete operations are recorded with a timestamp.

Example:

```text
2026-08-30 10:00:00 | CREATED | ID: 101 | Name: Rahul | Age: 20 | Course: BCA
2026-08-30 10:02:00 | UPDATED | ID: 101 | Name: Rahul Kumar | Age: 21 | Course: BCA
2026-08-30 10:04:00 | DELETED | ID: 101 | Name: Rahul Kumar | Age: 21 | Course: BCA
```

## Error Handling

The program uses validation and exception handling for:
- Invalid menu choices
- Non-numeric IDs and ages
- Empty names/courses
- Invalid age values
- Duplicate Student IDs
- Updating or deleting a non-existent student

## Project Structure

```text
StudentManagementApp/
├── Program.cs
├── StudentManagementApp.csproj
└── README.md
```

## Sample Test Cases

| Test | Action | Expected Result |
|---|---|---|
| 1 | Create student with valid data | Record is created |
| 2 | View students | Student records are displayed |
| 3 | Create duplicate ID | Error message is displayed |
| 4 | Update existing ID | Record is updated |
| 5 | Update non-existent ID | Error message is displayed |
| 6 | Delete existing ID | Record is deleted |
| 7 | Delete non-existent ID | Error message is displayed |
| 8 | Enter text for ID | Program asks for a valid number |
| 9 | Leave name/course empty | Program asks for required text |
| 10 | Create/update/delete a record | Operation is written to log |

## Task Requirement Mapping

- **CRUD:** Implemented through create, read, update, and delete methods.
- **In-memory storage:** Uses `List<Student>`.
- **Validation:** Validates IDs, names, ages, courses, and duplicate IDs.
- **Error handling:** Uses exceptions and user-friendly error messages.
- **File logging:** Uses `File.AppendAllText()` for operation logs.
- **Code quality:** Uses classes, methods, comments, meaningful names, and clear formatting.
- **Testing:** README includes sample test cases for core and error scenarios.
