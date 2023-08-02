using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstrumentsTracking.Domain.Models
{
    public class SamplingAct
    {
        public int ID { get; set; }
        [Required]
        public int EngineerID { get; set; }
        [Required]
        public string WellNumber { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        public string DrillingDepth { get; set; }
        [Required]
        public string TypeOfSolution { get; set; }
        [Required]
        public string WorkingCapacity { get; set; }
        [Required]
        public string SelectionDate { get; set; }
        [Required]
        public string SampleVolume { get; set; }

        [JsonIgnore]
        public Engineer Engineer { get; set; }
    }
}
