using System;

namespace StudentApp
{
    public class Student
    {
        public int Id { get; set; }
        public string Name    { get; set; } = string.Empty;
        public string Email   { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Grade   { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[{Id}] {Name} | Email: {Email} | Địa chỉ: {Address} | Tuổi: {Age} | Xếp loại: {Grade}";
        }

        // Ghi ra file: các trường cách nhau bằng '|'
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Email}|{Address}|{Age}|{Grade}";
        }

        // Đọc từ file
        public static Student FromFileString(string line)
        {
            var parts = line.Split('|');
            return new Student
            {
                Id      = int.Parse(parts[0].Trim()),
                Name    = parts[1].Trim(),
                Email   = parts[2].Trim(),
                Address = parts[3].Trim(),
                Age     = int.Parse(parts[4].Trim()),
                Grade   = parts[5].Trim()
            };
        }
    }
}
