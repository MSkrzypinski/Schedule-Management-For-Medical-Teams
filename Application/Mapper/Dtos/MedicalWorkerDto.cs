using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mapper.Dtos
{
    public class MedicalWorkerDto
    {
        public Guid Id { get; set; }
        public AddressDto Address { get; }
        public DateTime DateOfBirth { get; }
        public List<EmploymentContract> EmploymentContracts { get; }
        public UserDto User { get; }
        public List<Shift> Shifts { get; }
        public List<DayOffDto> DaysOff { get; }
        public List<MedicalWorkerProfession> MedicalWorkerProfessions { get; }
    }
}