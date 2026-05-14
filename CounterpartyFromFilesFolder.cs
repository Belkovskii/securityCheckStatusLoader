using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader
{
    public class CounterpartyFromFilesFolder
    {
        public Guid ExternalId { get; set; }

        public SecurityRecordFromFilesFolder[]? SecurityRecords { get; set; }
    }
}
