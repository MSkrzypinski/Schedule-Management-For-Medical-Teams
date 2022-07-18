using Application.Mapper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.MedicalWorkers.Requests
{
    public class CreateNewMedicalWorkerRequest
    {
        public AddressDto Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid UserId { get; set; }
    }
}
