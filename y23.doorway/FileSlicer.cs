using sak.utils.FileSlicer;

namespace y23.doorway
{
    internal class FileSlicer
    {
        public void RunTasks()
        {
            RunHello();
        }

        private void RunHello()
        {
            FileSlicerUtils fsu = new FileSlicerUtils();
            fsu.Hello();
        }
    }
}
