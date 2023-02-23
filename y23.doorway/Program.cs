namespace y23.doorway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            FileSlicerTasks();
            Console.WriteLine("Bye, World!");

        }

        private static void FileSlicerTasks()
        {
            FileSlicer fs = new FileSlicer();
            fs.RunTasks();
        }
    }
}