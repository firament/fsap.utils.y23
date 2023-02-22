using sak.utils.FileDedup.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sak.utils.FileDedup.Models
{

    #region INTERFACES

    public interface IFileEntry
    {
        DateTimeOffset ADD_ON { get; set; }
        DateTimeOffset EDIT_ON { get; set; }
        TimeSpan Hash_Time { get; set; }
        HASH_TYPES Hash_Type { get; set; }
        string Hash_Value { get; set; }
        long ID { get; set; }
        YES_NO Is_Duplicate { get; set; }
        string Name { get; set; }
        RECORD_STATUS Status { get; set; }
    }

    public interface IRunConfig
    {
        string ID { get; set; }
        DateTimeOffset DateUpdated { get; set; }
        List<string> Paths { get; set; }
        string Out_Path { get; set; }
        HASH_TYPES UseHash { get; set; }
    }

    #endregion INTERFACES


    #region CLASSES

    public class FileEntry : IFileEntry
    {
        public long ID { get; set; }           // DateTimeOffset.UtcNow..UtcTicks
        public string Name { get; set; } = string.Empty;
        public HASH_TYPES Hash_Type { get; set; }
        public TimeSpan Hash_Time { get; set; } // TimeSpan.TotalMilliseconds
        public string Hash_Value { get; set; } = string.Empty;
        public YES_NO Is_Duplicate { get; set; }
        public RECORD_STATUS Status { get; set; }
        public DateTimeOffset ADD_ON { get; set; }
        public DateTimeOffset EDIT_ON { get; set; }

    }

    public class RunConfig : IRunConfig
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;
        public List<String> Paths { get; set; } = new List<string>();
        public string Out_Path { get; set; } = string.Empty;
        public HASH_TYPES UseHash { get; set; }

    }

    #endregion // CLASSES
}
