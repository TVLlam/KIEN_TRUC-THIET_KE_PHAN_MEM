using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace StudentApp
{
    /// <summary>
    /// DATA layer – chịu trách nhiệm lưu trữ và đọc/ghi file.
    /// </summary>
    public class StudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;
        private readonly string _filePath = "students.txt";

        public StudentRepository()
        {
            LoadFromFile();
        }

        // ---------- CRUD ----------

        public List<Student> GetAll() => _students;

        public Student Add(string name, string email, string address, int age, string grade)
        {
            var student = new Student
            {
                Id      = _nextId++,
                Name    = name,
                Email   = email,
                Address = address,
                Age     = age,
                Grade   = grade
            };
            _students.Add(student);
            SaveToFile();
            return student;
        }

        public bool Delete(int id)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                _students.Remove(item);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool Update(int id, string name, string email, string address, int age, string grade)
        {
            var item = _students.FirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                item.Name    = name;
                item.Email   = email;
                item.Address = address;
                item.Age     = age;
                item.Grade   = grade;
                SaveToFile();
                return true;
            }
            return false;
        }

        // ---------- Search ----------

        public List<Student> SearchById(int id)
            => _students.Where(s => s.Id == id).ToList();

        public List<Student> SearchByName(string name)
            => _students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Student> SearchByAddress(string address)
            => _students.Where(s => s.Address.Contains(address, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Student> SearchByGrade(string grade)
            => _students.Where(s => s.Grade.Equals(grade, StringComparison.OrdinalIgnoreCase)).ToList();

        // ---------- File I/O ----------

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath)) return;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var item = Student.FromFileString(line);
                _students.Add(item);
                if (item.Id >= _nextId)
                    _nextId = item.Id + 1;
            }
        }

        private void SaveToFile()
        {
            File.WriteAllLines(_filePath, _students.Select(s => s.ToFileString()));
        }
    }
}
