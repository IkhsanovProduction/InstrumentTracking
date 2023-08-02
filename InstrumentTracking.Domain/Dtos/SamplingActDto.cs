using System;
using System.Collections.Generic;
using System.Text;

namespace InstrumentTracking.Domain.Dtos
{
    public class SamplingActDto
    {
        public int ID { get; set; }
        public int EngineerID { get; set; }
        public string WellNumber { get; set; }
        public string PlaceOfBirth { get; set; }
        public string DrillingDepth { get; set; }
        public string TypeOfSolution { get; set; }
        public string WorkingCapacity { get; set; }
        public string SelectionDate { get; set; }
        public string SampleVolume { get; set; }
    }
}
