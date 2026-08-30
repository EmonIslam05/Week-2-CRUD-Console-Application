using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentManagementApp
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Course { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"ID: {StudentId} | Name: {Name} | Age: {Age} | Course: {Course}";
        }
    }

    public class StudentManager
    {
        private readonly List<Student> students = new List<Student>();
        private readonly string logFilePath;

        public StudentManager()
        {
            logFilePath = Path.Combine(AppContext.BaseDirectory, "application.log");
        }

        public void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (student.StudentId <= 0)
                throw new ArgumentException("Student ID must be greater than 0.");

            if (string.IsNullOrWhiteSpace(student.Name))
                throw new ArgumentException("Student name cannot be empty.");

            if (student.Age < 5 || student.Age > 100)
                throw new ArgumentException("Age must be between 5 and 100.");

            if (string.IsNullOrWhiteSpace(student.Course))
                throw new ArgumentException("Course cannot be empty.");

            if (students.Any(s => s.StudentId == student.StudentId))
                throw new InvalidOperationException("A student with this ID already exists.");

            students.Add(student);
            Log($"CREATED | {student}");
        }

        public List<Student> GetAllStudents()
        {
            return new List<Student>(students);
        }

        public Student? GetStudentById(int studentId)
        {
            return students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public void UpdateStudent(int studentId, string name, int age, string course)
        {
            Student? student = GetStudentById(studentId);

            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Student name cannot be empty.");

            if (age < 5 || age > 100)
                throw new ArgumentException("Age must be between 5 and 100.");

            if (string.IsNullOrWhiteSpace(course))
                throw new ArgumentException("Course cannot be empty.");

            student.Name = name.Trim();
            student.Age = age;
            student.Course = course.Trim();

            Log($"UPDATED | {student}");
        }

        public void DeleteStudent(int studentId)
        {
            Student? student = GetStudentById(studentId);

            if (student == null)
                throw new KeyNotFoundException("Student not found.");

            students.Remove(student);
            Log($"DELETED | {student}");
        }

        private void Log(string message)
        {
            try
            {
                File.AppendAllText(
                    logFilePath,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}{Environment.NewLine}"
                );
            }
            catch (IOException)
            {
                // Logging failure should not terminate the main application.
            }
        }
    }

    internal class Program
    {
        private static readonly StudentManager manager = new StudentManager();

        static void Main()
        {
            Console.Title = "Student Management System";

            while (true)
            {
                DisplayMenu();
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateStudent();
                            break;
                        case "2":
                            ReadStudents();
                            break;
                        case "3":
                            UpdateStudent();
                            break;
                        case "4":
                            DeleteStudent();
                            break;
                        case "5":
                            Console.WriteLine("Thank you for using the Student Management System.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please select 1-5.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("       STUDENT MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Create Student");
            Console.WriteLine("2. Read Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
            Console.WriteLine("========================================");
        }

        private static void CreateStudent()
        {
            Console.Write("Enter Student ID: ");
            int id = ReadPositiveInteger();

            Console.Write("Enter Name: ");
            string name = ReadRequiredText("Name");

            Console.Write("Enter Age: ");
            int age = ReadInteger();

            Console.Write("Enter Course: ");
            string course = ReadRequiredText("Course");

            manager.AddStudent(new Student
            {
                StudentId = id,
                Name = name,
                Age = age,
                Course = course
            });

            Console.WriteLine("Student created successfully.");
        }

        private static void ReadStudents()
        {
            List<Student> students = manager.GetAllStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No student records found.");
                return;
            }

            Console.WriteLine("\nStudent Records:");
            Console.WriteLine("----------------------------------------");
            foreach (Student student in students)
                Console.WriteLine(student);
        }

        private static void UpdateStudent()
        {
            Console.Write("Enter Student ID to update: ");
            int id = ReadPositiveInteger();

            Console.Write("Enter new Name: ");
            string name = ReadRequiredText("Name");

            Console.Write("Enter new Age: ");
            int age = ReadInteger();

            Console.Write("Enter new Course: ");
            string course = ReadRequiredText("Course");

            manager.UpdateStudent(id, name, age, course);
            Console.WriteLine("Student updated successfully.");
        }

        private static void DeleteStudent()
        {
            Console.Write("Enter Student ID to delete: ");
            int id = ReadPositiveInteger();

            manager.DeleteStudent(id);
            Console.WriteLine("Student deleted successfully.");
        }

        private static int ReadPositiveInteger()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                    return value;

                Console.Write("Please enter a positive whole number: ");
            }
        }

        private static int ReadInteger()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Console.Write("Please enter a valid whole number: ");
            }
        }

        private static string ReadRequiredText(string fieldName)
        {
            while (true)
            {
                string? value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();

                Console.Write($"Please enter {fieldName}: ");
            }
        }
    }
}
