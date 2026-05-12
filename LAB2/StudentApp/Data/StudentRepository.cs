using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _collection;

        public StudentRepository(string connectionString, string databaseName = "StudentDB")
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Student>("Students");
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _collection.InsertOneAsync(student);
        }

        public async Task UpdateAsync(string id, Student student)
        {
            student.Id = id;
            await _collection.ReplaceOneAsync(s => s.Id == id, student);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(s => s.Id == id);
        }

        public async Task<List<Student>> SearchByNameAsync(string name)
        {
            var filter = Builders<Student>.Filter.Regex(s => s.Name,
                new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<Student>> SearchByAddressAsync(string address)
        {
            var filter = Builders<Student>.Filter.Regex(s => s.Address,
                new MongoDB.Bson.BsonRegularExpression(address, "i"));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<Student>> SearchByGradeAsync(string grade)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Grade, grade.ToUpper());
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
