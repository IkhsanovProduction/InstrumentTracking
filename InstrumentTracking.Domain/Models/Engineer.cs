using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InstrumentsTracking.Domain.Models
{
    public class Engineer
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Request> Requests { get; set; }
        [JsonIgnore]
        public List<SamplingAct> SamplingActs { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
