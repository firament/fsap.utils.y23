using Microsoft.VisualBasic;

using sak.utils.FileDedup.Infra;

namespace sak.utils.FileDedup.Models
{

    #region INTERFACES

    public interface IFileEntry
    {
        DateTimeOffset ADD_ON { get; set; }
        DateTimeOffset EDIT_ON { get; set; }
        TimeSpan Hash_Time { get; set; }
        HashTypeEnum Hash_Type { get; set; }
        string Hash_Value { get; set; }
        long ID { get; set; }
        YES_NO Is_Duplicate { get; set; }
        string Name { get; set; }
        RecordStatusEnum Status { get; set; }
    }

    public interface IRunConfig
    {
        string ID { get; set; }
        DateTimeOffset DateUpdated { get; set; }
        List<string> Paths { get; set; }
        string Out_Path { get; set; }
        HashTypeEnum UseHash { get; set; }
    }

    public interface IDedupReport: IInternalReport
    {
        string RunID { get; set; }
        DateTimeOffset RunDate { get; set; }
        //ResultEnum Result { get; set; }
        //string Message { get; set; }
        IRunConfig? RunConfig { get; set; }
        //List<string> Entries { get; set; }
    }

    public interface IInternalReport
    {
        string Message { get; set; }
        ResultEnum Result { get; set; }
        List<string> Entries { get; set; }
    }

    #endregion INTERFACES


    #region CLASSES

    public class FileEntry : IFileEntry
    {
        public long ID { get; set; }           // DateTimeOffset.UtcNow..UtcTicks
        public string Name { get; set; } = string.Empty;
        public HashTypeEnum Hash_Type { get; set; }
        public TimeSpan Hash_Time { get; set; } // TimeSpan.TotalMilliseconds
        public string Hash_Value { get; set; } = string.Empty;
        public YES_NO Is_Duplicate { get; set; }
        public RecordStatusEnum Status { get; set; }
        public DateTimeOffset ADD_ON { get; set; }
        public DateTimeOffset EDIT_ON { get; set; }

    }

    public class RunConfig : IRunConfig
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;
        public List<string> Paths { get; set; } = new List<string>();
        public string Out_Path { get; set; } = string.Empty;
        public HashTypeEnum UseHash { get; set; }

    }

    public class DedupReport : IDedupReport
    {
        public DedupReport()
        {
            RunID = Guid.NewGuid().ToString();
            RunDate = DateTimeOffset.Now;
            Result = ResultEnum.NOT_SET;
            Message = string.Empty;
            RunConfig = null;
            Entries = new List<string>();
        }
        public string RunID { get; set; }
        public DateTimeOffset RunDate { get; set; }
        public ResultEnum Result { get; set; }
        public string Message { get; set; }
        public IRunConfig? RunConfig { get; set; }
        public List<string> Entries { get; set; }

    }

    public class InternalReport : IInternalReport
    {
        public InternalReport()
        {
            Result = ResultEnum.NOT_SET;
            Message = string.Empty;
            Entries = new List<string>();
        }
        public ResultEnum Result { get; set; }
        public string Message { get; set; }
        public List<string> Entries { get; set; }
    }


    #endregion // CLASSES
}
