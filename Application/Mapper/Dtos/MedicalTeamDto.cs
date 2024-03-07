using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Mapper.Dtos
{
    public class MedicalTeamDto
    {
        public Guid Id { get; set; }
        public InformationAboutTeamDto InformationAboutTeam { get;set; }
        public bool IsActive {get;set;}
        public CoordinatorDto Coordinator { get; set;}
    }
}