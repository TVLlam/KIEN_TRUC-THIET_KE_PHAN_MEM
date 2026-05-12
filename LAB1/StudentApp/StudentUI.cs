using System;
using System.Collections.Generic;

namespace StudentApp
{
    /// <summary>
    /// UI layer – hiển thị menu và xử lý tương tác người dùng.
    /// </summary>
    public class StudentUI
    {
        private readonly StudentService _service = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowStudents();
                ShowMenu();

                string choice = (Console.ReadLine() ?? string.Empty).Trim();
                switch (choice)
                {
                    case "1": AddStudent();    break;
                    case "2": DeleteStudent(); break;
                    case "3": EditStudent();   break;
                    case "4": SearchMenu();    break;
                    case "0": return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

                Console.WriteLine("\nNhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        // ===================== HIỂN THỊ =====================

        private void ShowStudents()
        {
            var list = _service.GetStudents();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              DANH SÁCH SINH VIÊN                            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            if (list.Count == 0)
            {
                Console.WriteLine("  (Chưa có sinh viên nào.)");
            }
            else
            {
                PrintHeader();
                foreach (var s in list)
                    PrintRow(s);
                Console.WriteLine(new string('─', 95));
            }
        }

        private static void PrintHeader()
        {
            Console.WriteLine(new string('─', 95));
            Console.WriteLine($"{"ID",-5} {"Họ tên",-22} {"Email",-25} {"Địa chỉ",-20} {"Tuổi",-6} {"Xếp loại",-10}");
            Console.WriteLine(new string('─', 95));
        }

        private static void PrintRow(Student s)
        {
            Console.WriteLine($"{s.Id,-5} {s.Name,-22} {s.Email,-25} {s.Address,-20} {s.Age,-6} {s.Grade,-10}");
        }

        private void PrintList(List<Student> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("  Không tìm thấy sinh viên nào.");
                return;
            }
            PrintHeader();
            foreach (var s in list) PrintRow(s);
            Console.WriteLine(new string('─', 95));
        }

        // ===================== MENU =====================

        private static void ShowMenu()
        {
            Console.WriteLine("\n┌── Chức năng ────────────────┐");
            Console.WriteLine("│  1. Thêm sinh viên          │");
            Console.WriteLine("│  2. Xoá sinh viên           │");
            Console.WriteLine("│  3. Sửa sinh viên           │");
            Console.WriteLine("│  4. Tìm kiếm                │");
            Console.WriteLine("│  0. Thoát                   │");
            Console.WriteLine("└─────────────────────────────┘");
            Console.Write("Chọn: ");
        }

        // ===================== THÊM =====================

        private void AddStudent()
        {
            Console.WriteLine("\n--- THÊM SINH VIÊN ---");
            string name    = ReadString("Họ tên: ");
            string email   = ReadString("Email: ");
            string address = ReadString("Địa chỉ: ");
            int    age     = ReadInt("Tuổi: ");
            string grade   = ReadGrade();

            var s = _service.AddStudent(name, email, address, age, grade);
            Console.WriteLine($"✔ Đã thêm sinh viên ID={s.Id}.");
        }

        // ===================== XOÁ =====================

        private void DeleteStudent()
        {
            Console.Write("\nNhập ID sinh viên cần xoá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool ok = _service.RemoveStudent(id);
                Console.WriteLine(ok ? "✔ Đã xoá." : "✘ Không tìm thấy ID.");
            }
            else
            {
                Console.WriteLine("✘ ID không hợp lệ.");
            }
        }

        // ===================== SỬA =====================

        private void EditStudent()
        {
            Console.Write("\nNhập ID sinh viên cần sửa: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("✘ ID không hợp lệ.");
                return;
            }

            // Tìm sinh viên hiện tại để gợi ý
            var current = _service.GetStudents().Find(s => s.Id == id);
            if (current == null)
            {
                Console.WriteLine("✘ Không tìm thấy sinh viên.");
                return;
            }

            Console.WriteLine($"Sinh viên hiện tại: {current}");
            Console.WriteLine("(Nhấn Enter để giữ nguyên giá trị cũ)");

            string name    = ReadStringOptional($"Họ tên [{current.Name}]: ", current.Name);
            string email   = ReadStringOptional($"Email [{current.Email}]: ", current.Email);
            string address = ReadStringOptional($"Địa chỉ [{current.Address}]: ", current.Address);
            int    age     = ReadIntOptional($"Tuổi [{current.Age}]: ", current.Age);
            string grade   = ReadStringOptional($"Xếp loại [{current.Grade}]: ", current.Grade);

            bool ok = _service.EditStudent(id, name, email, address, age, grade);
            Console.WriteLine(ok ? "✔ Đã cập nhật." : "✘ Cập nhật thất bại.");
        }

        // ===================== TÌM KIẾM =====================

        private void SearchMenu()
        {
            Console.Clear();
            Console.WriteLine("--- TÌM KIẾM SINH VIÊN ---");
            Console.WriteLine("  a. Theo ID");
            Console.WriteLine("  b. Theo Tên");
            Console.WriteLine("  c. Theo Địa chỉ");
            Console.WriteLine("  d. Theo Xếp loại");
            Console.Write("Chọn: ");

            string opt = (Console.ReadLine() ?? string.Empty).Trim().ToLower();
            List<Student> result;

            switch (opt)
            {
                case "a":
                    Console.Write("Nhập ID: ");
                    if (int.TryParse(Console.ReadLine(), out int sid))
                        result = _service.SearchById(sid);
                    else { Console.WriteLine("✘ ID không hợp lệ."); return; }
                    break;

                case "b":
                    Console.Write("Nhập tên: ");
                    result = _service.SearchByName(Console.ReadLine() ?? "");
                    break;

                case "c":
                    Console.Write("Nhập địa chỉ: ");
                    result = _service.SearchByAddress(Console.ReadLine() ?? "");
                    break;

                case "d":
                    Console.Write("Nhập xếp loại (A/B/C/D): ");
                    result = _service.SearchByGrade(Console.ReadLine() ?? "");
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    return;
            }

            Console.WriteLine($"\nKết quả ({result.Count} sinh viên):");
            PrintList(result);
        }

        // ===================== HELPER =====================

        private static string ReadString(string prompt)
        {
            string val;
            do
            {
                Console.Write(prompt);
                val = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(val))
                    Console.WriteLine("  ⚠ Không được để trống.");
            } while (string.IsNullOrWhiteSpace(val));
            return val;
        }

        private static string ReadStringOptional(string prompt, string defaultVal)
        {
            Console.Write(prompt);
            var val = Console.ReadLine()?.Trim();
            return string.IsNullOrWhiteSpace(val) ? defaultVal : val;
        }

        private static int ReadInt(string prompt)
        {
            int val;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out val) && val > 0) return val;
                Console.WriteLine("  ⚠ Vui lòng nhập số nguyên dương.");
            }
        }

        private static int ReadIntOptional(string prompt, int defaultVal)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input)) return defaultVal;
            return int.TryParse(input, out int val) && val > 0 ? val : defaultVal;
        }

        private static string ReadGrade()
        {
            while (true)
            {
                Console.Write("Xếp loại (A/B/C/D): ");
                var g = Console.ReadLine()?.Trim().ToUpper() ?? "";
                if (g == "A" || g == "B" || g == "C" || g == "D") return g;
                Console.WriteLine("  ⚠ Xếp loại phải là A, B, C hoặc D.");
            }
        }
    }
}
