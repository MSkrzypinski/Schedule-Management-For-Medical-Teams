using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Shifts.Requests
{
    public class AddMedicalWorkerRequest
    {
        public Guid MedicalWorkerId { get; set; }
        public Guid ShiftId { get; set; }
    }
}
