using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader
{
    public class ScsUpdateCreateData
    {
        public string RecordId { get; set; }
        public string SecurityCheckGUID {  get; set; }
        public DateTime CheckDate {  get; set; }
        public string SecurotyUserId {  get; set; }
        public string ExternalCreatedByText {  get; set; }
        public string CRMClienId {  get; set; }
        public FileCreationModel CheckAttachment {  get; set; }

    }
}
