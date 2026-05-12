using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    public class StudentUI
    {
        private readonly StudentService _service;

        public StudentUI(StudentService service)
        {
            _service = service;
        }

        public async Task Run()
        {
            while (true)
            {
                try { Console.Clear(); } catch { }
                await ShowStudents();
                ShowMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        await AddStudent();
                        break;
                    case "2":
                        await EditStudent();
                        break;
                    case "3":
                        await DeleteStudent();
                        break;
                    case "4":
                        await SearchById();
                        break;
                    case "5":
                        await SearchByName();
                        break;
                    case "6":
                        await SearchByAddress();
                        break;
                    case "7":
                        await SearchByGrade();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

                Console.WriteLine("\nNhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private async Task ShowStudents()
        {
            var students = await _service.GetAllAsync();
            Console.WriteLine("=== DANH SÁCH SINH VIÊN ===");
            if (students.Count == 0)
            {
                Console.WriteLine("Chưa có sinh viên nào.");
            }
            else
            {
                PrintStudents(students);
            }
            Console.WriteLine();
        }

        private void ShowMenu()
        {
            Console.WriteLine("Chức năng:");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Sửa sinh viên");
            Console.WriteLine("3. Xoá sinh viên");
            Console.WriteLine("4. Tìm kiếm theo Id");
            Console.WriteLine("5. Tìm kiếm theo Tên");
            Console.WriteLine("6. Tìm kiếm theo Địa chỉ");
            Console.WriteLine("7. Tìm kiếm theo Điểm (Grade)");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn: ");
        }

        private void PrintStudents(List<Student> students)
        {
            Console.WriteLine($"{"ID",-26} {"Tên",-20} {"Email",-25} {"Địa chỉ",-20} {"Tuổi",5} {"Grade",6}");
            Console.WriteLine(new string('-', 110));
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id,-26} {s.Name,-20} {s.Email,-25} {s.Address,-20} {s.Age,5} {s.Grade,6}");
            }
        }

        private async Task AddStudent()
        {
            Console.WriteLine("\n--- THÊM SINH VIÊN ---");
            var student = InputStudent();
            await _service.AddAsync(student);
            Console.WriteLine("✅ Thêm sinh viên thành công!");
        }

        private async Task EditStudent()
        {
            Console.Write("\nNhập ID sinh viên cần sửa: ");
            string id = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(id)) return;

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
            {
                Console.WriteLine("❌ Không tìm thấy sinh viên!");
                return;
            }

            Console.WriteLine($"Sinh viên hiện tại: {existing}");
            Console.WriteLine("Nhập thông tin mới (Enter để giữ nguyên):");

            var updated = InputStudent(existing);
            await _service.UpdateAsync(id, updated);
            Console.WriteLine("✅ Cập nhật thành công!");
        }

        private async Task DeleteStudent()
        {
            Console.Write("\nNhập ID sinh viên cần xoá: ");
            string id = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(id)) return;

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
            {
                Console.WriteLine("❌ Không tìm thấy sinh viên!");
                return;
            }

            Console.Write($"Xác nhận xoá sinh viên '{existing.Name}'? (y/n): ");
            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                await _service.DeleteAsync(id);
                Console.WriteLine("✅ Xoá thành công!");
            }
        }

        private async Task SearchById()
        {
            Console.Write("\nNhập ID cần tìm: ");
            string id = Console.ReadLine()?.Trim();
            var student = await _service.GetByIdAsync(id);
            Console.WriteLine("\n--- KẾT QUẢ ---");
            if (student != null)
                Console.WriteLine(student);
            else
                Console.WriteLine("❌ Không tìm thấy sinh viên.");
        }

        private async Task SearchByName()
        {
            Console.Write("\nNhập tên cần tìm: ");
            string name = Console.ReadLine();
            var results = await _service.SearchByNameAsync(name);
            PrintSearchResults(results);
        }

        private async Task SearchByAddress()
        {
            Console.Write("\nNhập địa chỉ cần tìm: ");
            string address = Console.ReadLine();
            var results = await _service.SearchByAddressAsync(address);
            PrintSearchResults(results);
        }

        private async Task SearchByGrade()
        {
            Console.Write("\nNhập Grade cần tìm (A/B/C/D/F): ");
            string grade = Console.ReadLine();
            var results = await _service.SearchByGradeAsync(grade);
            PrintSearchResults(results);
        }

        private void PrintSearchResults(List<Student> results)
        {
            Console.WriteLine($"\n--- KẾT QUẢ ({results.Count} sinh viên) ---");
            if (results.Count == 0)
                Console.WriteLine("Không tìm thấy sinh viên nào.");
            else
                PrintStudents(results);
        }

        private Student InputStudent(Student existing = null)
        {
            string ReadOrKeep(string prompt, string current)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                return string.IsNullOrWhiteSpace(input) ? current : input;
            }

            string name    = ReadOrKeep($"Tên{(existing != null ? $" [{existing.Name}]" : "")}: ", existing?.Name);
            string email   = ReadOrKeep($"Email{(existing != null ? $" [{existing.Email}]" : "")}: ", existing?.Email);
            string address = ReadOrKeep($"Địa chỉ{(existing != null ? $" [{existing.Address}]" : "")}: ", existing?.Address);

            int age = existing?.Age ?? 0;
            Console.Write($"Tuổi{(existing != null ? $" [{existing.Age}]" : "")}: ");
            string ageInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ageInput))
                int.TryParse(ageInput, out age);

            string grade = ReadOrKeep($"Grade (A/B/C/D/F){(existing != null ? $" [{existing.Grade}]" : "")}: ", existing?.Grade);

            return new Student
            {
                Name    = name,
                Email   = email,
                Address = address,
                Age     = age,
                Grade   = grade?.ToUpper()
            };
        }
    }
}
