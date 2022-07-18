using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.MedicalWorkers.Requests
{
    public class AddEmploymentContractRequest
    {
        public Guid MedicalWorkerId { get; set; }
        public Guid MedicalTeamId { get; set; }
    }
}
