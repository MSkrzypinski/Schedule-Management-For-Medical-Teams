using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Coordinators.CreateNewCoordinator
{
    public class CreateNewCoordinatorCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
