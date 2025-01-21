using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XProject.Models
{
    public class Localization
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public Guid ProjectId { get; set; }
        public ICollection<LocalizationValue>? Values { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
    }
}
