using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InstrumentTracking.Domain.Models;

namespace InstrumentsTracking.Domain.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int EngineerID { get; set; }

        [JsonIgnore]
        public Engineer Engineer { get; set; }
    }
}
