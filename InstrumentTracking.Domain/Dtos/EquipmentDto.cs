using System;
using System.Collections.Generic;
using System.Text;

namespace InstrumentTracking.Domain.Dtos
{
    public class EquipmentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string InventoryNumber { get; set; }
        public DateTime DateOfCalibrationTest { get; set; }
        public DateTime DateOfTheNextCalibrationTest { get; set; }
        public string ResponsibleForCalibration { get; set; }
        public string Passport { get; set; }
        public bool IsInStock { get; set; }
    }
}
