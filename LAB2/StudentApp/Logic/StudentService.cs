using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Student>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Student> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Student student) => await _repository.AddAsync(student);

        public async Task UpdateAsync(string id, Student student) => await _repository.UpdateAsync(id, student);

        public async Task DeleteAsync(string id) => await _repository.DeleteAsync(id);

        public async Task<List<Student>> SearchByNameAsync(string name) => await _repository.SearchByNameAsync(name);

        public async Task<List<Student>> SearchByAddressAsync(string address) => await _repository.SearchByAddressAsync(address);

        public async Task<List<Student>> SearchByGradeAsync(string grade) => await _repository.SearchByGradeAsync(grade);
    }
}
