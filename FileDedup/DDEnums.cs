namespace sak.utils.FileDedup.Infra
{
    /// <summary>
    /// 
    /// </summary>
    public enum YES_NO
    {

        /// <summary>
        /// 
        /// </summary>
        NO = 0,  // No

        /// <summary>
        /// 
        /// </summary>
        YES = 1,  // Yes

    }


    /// <summary>
    /// 
    /// </summary>
    public enum RecordStatusEnum
    {

        /// <summary>
        /// 
        /// </summary>
        NOT_SET = 0,   // Bad Code if this is seen

        /// <summary>
        /// 
        /// </summary>
        NEW = 1,       // New record added to system

        /// <summary>
        /// 
        /// </summary>
        PROCESSED = 2, // Record is processed and updated

        /// <summary>
        /// 
        /// </summary>
        DELETED = 9,   // Record is Deleted, do not use

    }


    /// <summary>
    /// 
    /// </summary>
    public enum HashTypeEnum
    {

        /// <summary>
        /// 
        /// </summary>
        NONE = 0,

        /// <summary>
        /// 
        /// </summary>
        MD5 = 1,

        /// <summary>
        /// 
        /// </summary>
        SHA256 = 2,

        /// <summary>
        /// 
        /// </summary>
        SHA512 = 3,

        /// <summary>
        /// 
        /// </summary>
        CUSTOM = 9,

    }

    /// <summary>
    /// 
    /// </summary>
    public enum ResultEnum
    {

        /// <summary>
        /// 
        /// </summary>
        NOT_SET = 0,

        /// <summary>
        /// 
        /// </summary>
        SUCCESS = 1,

        /// <summary>
        /// 
        /// </summary>
        FAIL = 2,

        /// <summary>
        /// 
        /// </summary>
        RUNNING = 3,

        /// <summary>
        /// 
        /// </summary>
        PARTIAL_FAIL,

    }
}
