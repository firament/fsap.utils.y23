using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string Version { get; set; } = "0.1";
        public string InputPath { get; set; } = string.Empty;
        public string OutputPath { get; set; } = String.Empty;
        public int SliceLineCount { get; set; } = 100;
        public int HeaderLineCount { get; set; } = 10;
        public bool SingleFileOutput { get; set; } = false;
    }
}
