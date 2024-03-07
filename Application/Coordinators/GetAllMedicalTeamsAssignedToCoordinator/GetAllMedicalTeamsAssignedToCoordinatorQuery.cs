using Application.Mapper.Dtos;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Coordinators.GetAllMedicalTeamsAssignedToCoordinator
{
    public class GetAllMedicalTeamsAssignedToCoordinatorQuery : IRequest<IEnumerable<MedicalTeamDto>>
    {
        public Guid UserId { get; set; }
    }
}
