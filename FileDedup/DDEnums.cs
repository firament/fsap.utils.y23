using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sak.utils.FileDedup.Infra
{
    public enum YES_NO
    {
        NO = 0,  // No
        YES = 1,  // Yes
    }

    public enum RECORD_STATUS
    {
        NOT_SET = 0,   // Bad Code if this is seen
        NEW = 1,       // New record added to system
        PROCESSED = 2, // Record is processed and updated
        DELETED = 9,   // Record is Deleted, do not use
    }

    public enum HASH_TYPES
    {
        NONE = 0,
        MD5 = 1,
        SHA256 = 2,
        SHA512 = 3,
        CUSTOM = 9,
    }
}
