using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapper.Dtos
{
    public class InformationAboutTeamDto
    {
        public string Code { get; set; }
        public string City { get; set; }
        public int SizeOfTeam { get; set; }
        public MedicalTeamType MedicalTeamType { get; set; }
    }
}
