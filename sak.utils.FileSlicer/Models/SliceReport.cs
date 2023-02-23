/*
 * Generated with CLR ver. 4.0.30319.42000
 * Copyright 2023 sak
 * 
 * SliceReport.cs
 * Author      : sak
 * Contributors: 
 *  (one entry for each person that contributes to code in this file)
 *  <contributor name 1>
 *  
 * Description : 
 *  Brief description of this file's code intent.
 */

using sak.utils.FileSlicer.Common;

namespace sak.utils.FileSlicer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISliceReport
    {

        /// <summary>
        /// 
        /// </summary>
        string RunID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTimeOffset RunDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ResultEnum Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RunConfig { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> Entries { get; set; }
    }

    public class SliceReport : ISliceReport
    {
        public SliceReport()
        {
            RunID = Guid.NewGuid().ToString();
            RunDate = DateTimeOffset.Now;
            Result = ResultEnum.NOT_SET;
            Message = string.Empty;
            RunConfig = string.Empty;
            Entries = new List<string>();
        }
        public string RunID { get; set; }
        public DateTimeOffset RunDate { get; set; }
        public ResultEnum Result { get; set; }
        public string Message { get; set; }
        public String RunConfig { get; set; }
        public List<string> Entries { get; set; }

    }
}
