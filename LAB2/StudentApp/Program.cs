using System;
using System.Text;
using System.Threading.Tasks;
using StudentApp;

public class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // MongoDB Atlas (cloud)
        string connectionString = "mongodb+srv://tranvanlam_db_user:tranvanlam05@cluster0.vdl7mfw.mongodb.net/?appName=Cluster0";

        var repository = new StudentRepository(connectionString, "StudentDB");
        var service    = new StudentService(repository);
        var ui         = new StudentUI(service);

        await ui.Run();
    }
}
