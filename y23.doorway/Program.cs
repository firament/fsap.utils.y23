namespace y23.doorway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //FileSlicerTasks();

            FileDedupTasks();

            Console.WriteLine("Bye, World!");

        }

        private static void FileSlicerTasks()
        {
            FileSlicer fs = new FileSlicer();
            fs.RunTasks();
        }
        private static void FileDedupTasks()
        {
            using (FileDedup fd = new FileDedup())
            {
                fd.RunTasks();
            };
        }
    }
}
