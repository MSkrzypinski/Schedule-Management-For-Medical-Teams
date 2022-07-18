using Application.Mapper.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Coordinators.GetCoordinator.GetCoordinatorById
{
    public class GetCoordinatorByIdQuery : IRequest<CoordinatorDto>
    {
        public Guid Id { get; set; }
    }
}
