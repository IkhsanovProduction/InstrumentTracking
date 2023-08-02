using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentsTracking.Dto
{
    public class RegisterDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int EngineerID { get; set; }
    }
}
