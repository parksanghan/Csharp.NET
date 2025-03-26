//Program.cs

namespace _1011_회원관리프로그램
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application app = Application.Singleton;
            app.Run();

            app.Dispose();  
        }
    }
}