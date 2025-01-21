using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XProject.Models
{
    public class LocalizationValue
    {
        public int Id { get; set; }
        public string Language { get; set; }  
        public string Value { get; set; }   
        public int LocalizationId { get; set; }
        [JsonIgnore]
        public Localization Localization { get; set; }
    }
}
