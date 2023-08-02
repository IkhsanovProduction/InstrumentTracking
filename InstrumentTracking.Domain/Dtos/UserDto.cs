using System;
using System.Collections.Generic;
using System.Text;

namespace InstrumentTracking.Domain.Dtos
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int EngineerID { get; set; }
    }
}
