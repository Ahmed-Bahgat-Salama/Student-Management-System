using System;
using System.Collections.Generic;

namespace StudentManagementSystem
{
    // 学生类
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | Name: {Name} | Age: {Age} | Grade: {Grade} | Email: {Email}";
        }
    }

    // 学生管理类
    public class StudentManager
    {
        private List<Student> students = new List<Student>();
        private int nextId = 1;

        // 添加学生
        public void AddStudent(Student student)
        {
            student.Id = nextId++;
            students.Add(student);
            Console.WriteLine("Student added successfully!");
        }

        // 显示所有学生
        public void DisplayStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found!");
                return;
            }

            Console.WriteLine("\n=== Student List ===");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        // 更新学生信息
        public void UpdateStudent(int id, string name, int age, string grade, string email)
        {
            var student = students.Find(s => s.Id == id);
            if (student != null)
            {
                student.Name = name;
                student.Age = age;
                student.Grade = grade;
                student.Email = email;
                Console.WriteLine("Student updated successfully!");
            }
            else
            {
                Console.WriteLine("Student not found!");
            }
        }

        // 删除学生
        public void DeleteStudent(int id)
        {
            var student = students.Find(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Student deleted successfully!");
            }
            else
            {
                Console.WriteLine("Student not found!");
            }
        }

        // 按ID搜索学生
        public Student SearchStudentById(int id)
        {
            return students.Find(s => s.Id == id);
        }

        // 按姓名搜索学生
        public List<Student> SearchStudentsByName(string name)
        {
            return students.FindAll(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }

    class Program
    {
        static StudentManager manager = new StudentManager();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Student Management System ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Search Student");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddStudentMenu();
                        break;
                    case 2:
                        manager.DisplayStudents();
                        break;
                    case 3:
                        UpdateStudentMenu();
                        break;
                    case 4:
                        DeleteStudentMenu();
                        break;
                    case 5:
                        SearchStudentMenu();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }
            }
        }

        static void AddStudentMenu()
        {
            var student = new Student();

            Console.Write("Enter student name: ");
            student.Name = Console.ReadLine();

            Console.Write("Enter student age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age!");
                return;
            }
            student.Age = age;

            Console.Write("Enter student grade: ");
            student.Grade = Console.ReadLine();

            Console.Write("Enter student email: ");
            student.Email = Console.ReadLine();

            manager.AddStudent(student);
        }

        static void UpdateStudentMenu()
        {
            Console.Write("Enter student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            var student = manager.SearchStudentById(id);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Console.WriteLine("Invalid age!");
                return;
            }

            Console.Write("Enter new grade: ");
            string grade = Console.ReadLine();

            Console.Write("Enter new email: ");
            string email = Console.ReadLine();

            manager.UpdateStudent(id, name, age, grade, email);
        }

        static void DeleteStudentMenu()
        {
            Console.Write("Enter student ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            manager.DeleteStudent(id);
        }

        static void SearchStudentMenu()
        {
            Console.WriteLine("\nSearch by:");
            Console.WriteLine("1. ID");
            Console.WriteLine("2. Name");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice!");
                return;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter student ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid ID!");
                        return;
                    }
                    var student = manager.SearchStudentById(id);
                    if (student != null)
                        Console.WriteLine(student);
                    else
                        Console.WriteLine("Student not found!");
                    break;

                case 2:
                    Console.Write("Enter student name: ");
                    string name = Console.ReadLine();
                    var results = manager.SearchStudentsByName(name);
                    if (results.Count > 0)
                    {
                        Console.WriteLine("\nSearch Results:");
                        foreach (var s in results) Console.WriteLine(s);
                    }
                    else
                    {
                        Console.WriteLine("No students found!");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }
}