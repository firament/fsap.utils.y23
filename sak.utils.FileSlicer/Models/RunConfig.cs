namespace sak.utils.FileSlicer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRunConfig
    {

        /// <summary>
        /// 
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string InputPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string OutputPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int SliceLineCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int HeaderLineCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool SingleFileOutput { get; set; }
    }

    public class RunConfig : IRunConfig
    {
        public RunConfig()
        {
            ID = Guid.NewGuid().ToString();
            Version = "0.1";
            InputPath = String.Empty;
            OutputPath = String.Empty;
            SliceLineCount = 100;
            HeaderLineCount = 1;
            SingleFileOutput = false;
        }

        public string ID { get; set; }
        public string Version { get; set; }
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public int SliceLineCount { get; set; }
        public int HeaderLineCount { get; set; }
        public bool SingleFileOutput { get; set; }
    }
}
