using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InstrumentsTracking.Domain.Models
{
    public class Equipment
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public DateTime DateOfCalibrationTest { get; set; }
        [Required]
        public DateTime DateOfTheNextCalibrationTest { get; set; }
        [Required]
        public string ResponsibleForCalibration { get; set; }
        public string Passport { get; set; }
        public bool IsLinked { get; set; }

        public static explicit operator Equipment(ValueTask<Equipment> v)
        {
            throw new NotImplementedException();
        }

        [JsonIgnore]
        public List<Request> Requests { get; set; }
    }
}
