using System;
using System.Collections.Generic;

namespace StudentApp
{
    /// <summary>
    /// LOGIC layer – nghiệp vụ ứng dụng, giao tiếp giữa UI và Repository.
    /// </summary>
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public List<Student> GetStudents() => _repo.GetAll();

        public Student AddStudent(string name, string email, string address, int age, string grade)
            => _repo.Add(name, email, address, age, grade);

        public bool RemoveStudent(int id) => _repo.Delete(id);

        public bool EditStudent(int id, string name, string email, string address, int age, string grade)
            => _repo.Update(id, name, email, address, age, grade);

        public List<Student> SearchById(int id)         => _repo.SearchById(id);
        public List<Student> SearchByName(string name)  => _repo.SearchByName(name);
        public List<Student> SearchByAddress(string address) => _repo.SearchByAddress(address);
        public List<Student> SearchByGrade(string grade) => _repo.SearchByGrade(grade);
    }
}
