using Application.Mapper.Dtos;
using Domain.ValueObjects.Enums;
using System;

namespace API.MedicalTeams
{
    public class CreateNewMedicalTeamRequest
    {
        public Guid CoordinatorId { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public int SizeOfTeam { get; set; } 
    }
}