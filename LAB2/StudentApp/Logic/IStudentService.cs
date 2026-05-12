using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(string id);
        Task AddAsync(Student student);
        Task UpdateAsync(string id, Student student);
        Task DeleteAsync(string id);
        Task<List<Student>> SearchByNameAsync(string name);
        Task<List<Student>> SearchByAddressAsync(string address);
        Task<List<Student>> SearchByGradeAsync(string grade);
    }
}
