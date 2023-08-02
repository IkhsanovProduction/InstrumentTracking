using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstrumentTracking.Domain.Dtos
{
    public class EngineerDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
    }
}
