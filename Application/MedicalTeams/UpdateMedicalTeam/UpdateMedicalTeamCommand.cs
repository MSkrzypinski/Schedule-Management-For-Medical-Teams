using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mapper.Dtos;
using Domain.ValueObjects.Enums;
using MediatR;

namespace Application.MedicalTeams.UpdateMedicalTeam
{
    public class UpdateMedicalTeamCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid CoordinatorId { get; set; }
        public string City { get; set; }
        public int SizeOfTeam { get; set; }
        public MedicalTeamType MedicalTeamType { get; set; }
    }
}