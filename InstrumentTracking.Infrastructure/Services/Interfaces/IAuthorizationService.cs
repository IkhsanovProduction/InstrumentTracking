using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Infrastructure.Services
{
    public interface IAuthorizationService
    {
        public Task Register { get; set; }
        public Task Login { get; set; }
    }
}
