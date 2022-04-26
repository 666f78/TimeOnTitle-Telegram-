namespace TimeOnTitle
{
    internal class Program
    {
        private static ChangeWindowTitle WindowClock = new();

        private static void Main(string[] args)
        {
            WindowClock.Start();
        }
    }
}