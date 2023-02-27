using sak.utils.FileDedup;
using sak.utils.FileDedup.Support;

namespace y23.doorway
{
    internal class FileDedup : IDisposable
    {
        public FileDedup() => Console.WriteLine("Initialized: y23.doorway.FileDedup");

        public void RunTasks()
        {
            //DumpConfigSample();
            RunHashGenerator();
        }

        private void DumpConfigSample()
        {
            string lsJsonSample01 = RunConfigSupport.Sample01_Json();
            string lsOutPath = Environment.CurrentDirectory;
            string lsOutFile = $"FileDedup-Config-Sample01-{DateTimeOffset.Now.ToString("yyMMdd_HHmmss")}.json";
            Console.WriteLine($"Writing output to {lsOutPath}{Path.DirectorySeparatorChar}{lsOutFile}");
            File.WriteAllTextAsync(Path.Combine(lsOutPath, lsOutFile), lsJsonSample01);
            Console.WriteLine("Done writing.");
        }

        private void RunHashGenerator()
        {
            string lsTestConfig = "D:/T30/dedup-config.json";
            // D:\T30\dedup-config.json
            string lsConfig = string.Empty;
            if (File.Exists(lsTestConfig))
            {
                lsConfig = File.ReadAllText(lsTestConfig);
            }
            using (HashGeneratorSvc hgs = new HashGeneratorSvc(lsConfig))
            {
                _ = hgs.Generate();
            }
        }

        public void Dispose() => Console.WriteLine("Disposed: y23.doorway.FileDedup");
    }
}
