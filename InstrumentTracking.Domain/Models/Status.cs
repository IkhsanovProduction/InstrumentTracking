using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using InstrumentsTracking.Domain.Models;

namespace InstrumentTracking.Domain.Models
{
    public class Status
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public List<Request> Requests { get; set; }
    }
}
