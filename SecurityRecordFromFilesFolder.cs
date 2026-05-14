using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader
{
    public class SecurityRecordFromFilesFolder
    {
        public Guid Id { get; set; }

        public byte[]? FileItem { get; set; }
    }
}
