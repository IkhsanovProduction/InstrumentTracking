using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Infrastructure
{
    public class AuthOptions
    {
        public const string ISSUER = ""; 
        public const string AUDIENCE = ""; 
        const string KEY = "";  
        public const int LIFETIME = 43200; 

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
