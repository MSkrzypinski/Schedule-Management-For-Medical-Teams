using Application.Mapper.Dtos;
using Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalTeams.CreateNewMedicalTeam
{
    public class CreateNewMedicalTeamCommand : IRequest<Guid>
    {
        public Guid CoordinatorId { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public int SizeOfTeam { get; set; }
        public MedicalTeamType MedicalTeamType { get; set; }
    }
}
