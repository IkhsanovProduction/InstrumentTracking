using System;
using System.Collections.Generic;
using System.Text;

namespace InstrumentTracking.Domain.Dtos
{
    public class RequestDto
    {
        public int ID { get; set; }
        public int EquipmentID { get; set; }
        public int EngineerID { get; set; }
        public string EngineerName { get; set; }
        public string EngineerSurname { get; set; }
        public string Status { get; set; }
        public DateTime BindingDate { get; set; }
        public DateTime UnbindingDate { get; set; }
        public string WellNumber { get; set; }
        public string Comment { get; set; }
    }
}
