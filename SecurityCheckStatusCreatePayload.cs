using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCheckStatusLoader
{
    public class SecurityCheckStatusCreatePayload
    {
        [JsonProperty("payload")]
        public CreatePayload Payload { get; set; }

        [JsonProperty("tempData")]
        public TempData TempData { get; set; }
    }

    public class CreatePayload
    {
        [JsonProperty("__id")]
        public Guid Id { get; set; }

        [JsonProperty("__createdBy")]
        public object CreatedBy { get; set; }

        [JsonProperty("__createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("__updatedBy")]
        public object UpdatedBy { get; set; }

        [JsonProperty("__updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("__deletedAt")]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("__version")]
        public object Version { get; set; }

        [JsonProperty("__directory")]
        public object Directory { get; set; }

        [JsonProperty("__archive")]
        public bool Archive { get; set; }

        [JsonProperty("__currentUserPermissions")]
        public List<string> CurrentUserPermissions { get; set; }

        [JsonProperty("__subscribers")]
        public object Subscribers { get; set; }

        [JsonProperty("__externalId")]
        public object ExternalId { get; set; }

        [JsonProperty("CRMClienId")]
        public List<Guid> CRMClienId { get; set; }

        [JsonProperty("StatusId")]
        public List<Guid> StatusId { get; set; }

        [JsonProperty("CheckAttachment")]
        public List<CheckAttachment> CheckAttachment { get; set; }

        [JsonProperty("CheckDate")]
        public DateTime CheckDate { get; set; }

        [JsonProperty("SecurotyUserId")]
        public object SecurotyUserId { get; set; }

        [JsonProperty("SecurityCheckGUID")]
        public object SecurityCheckGUID { get; set; }

        [JsonProperty("ExternalCreatedByText")]
        public object ExternalCreatedByText { get; set; }

        [JsonProperty("__name")]
        public string Name { get; set; }

        [JsonProperty("__index")]
        public object Index { get; set; }

        [JsonProperty("__tasks")]
        public object Tasks { get; set; }

        [JsonProperty("__tasks_earliest_duedate")]
        public object TasksEarliestDueDate { get; set; }

        [JsonProperty("__tasks_performers")]
        public object TasksPerformers { get; set; }

        [JsonProperty("__debug")]
        public object Debug { get; set; }

        [JsonProperty("__externalProcessMeta")]
        public object ExternalProcessMeta { get; set; }

        [JsonProperty("__approvalRouteStages")]
        public object ApprovalRouteStages { get; set; }
    }

    public class CheckAttachment
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hash")]
        public Guid Hash { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("file")]
        public FileInfo File { get; set; }
    }

    public class FileInfo
    {
        [JsonProperty("__id")]
        public Guid Id { get; set; }

        [JsonProperty("__createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("__updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("__deletedAt")]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("__name")]
        public string Name { get; set; }

        [JsonProperty("originalName")]
        public string OriginalName { get; set; }

        [JsonProperty("__currentUserPermissions")]
        public List<string> CurrentUserPermissions { get; set; } = new();

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("hash")]
        public Guid Hash { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("__subscribers")]
        public List<object> Subscribers { get; set; } = new();
    }

    public class TempData
    {
        [JsonProperty("withEventForceCreate")]
        public bool WithEventForceCreate { get; set; }

        [JsonProperty("assignExistingIndex")]
        public bool AssignExistingIndex { get; set; }
    }
}
