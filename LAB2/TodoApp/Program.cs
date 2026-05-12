using System;
using System.Text;
using System.Threading.Tasks;
using TodoApp;

public class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string connectionString = "Server=LAPTOP-LU6935Q7\\TRANVANLAM;Database=TodoDB;Integrated Security=true;TrustServerCertificate=true";

        var repository = new TodoRepository(connectionString);
        var service = new TodoService(repository);
        var ui = new TodoUI(service);

        await ui.Run();
    }
}
