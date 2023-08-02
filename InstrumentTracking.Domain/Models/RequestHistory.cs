using InstrumentTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstrumentsTracking.Domain.Models
{
    public class RequestHistory
    {
        public int ID { get; set; }
        [Required]
        public int EquipmentID { get; set; }
        [Required]
        public int EngineerID { get; set; }
        [Required]
        public int StatusID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string WellNumber { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Status Status { get; set; }
    }
}
