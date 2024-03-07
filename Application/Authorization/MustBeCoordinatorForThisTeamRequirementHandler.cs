using Application.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization
{
    public class MustBeCoordinatorForThisTeamRequirementHandler : AuthorizationHandler<MustBeCoordinatorForThisTeamRequirement, MedicalTeam>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;

        public MustBeCoordinatorForThisTeamRequirementHandler(ICoordinatorRepository coordinatorRepository)
        {
            _coordinatorRepository = coordinatorRepository;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeCoordinatorForThisTeamRequirement requirement, MedicalTeam resource)
        {
            var userId = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var coordinator = await _coordinatorRepository.GetCoordinatorIncludeAllPropertiesByUserIdAsync(Guid.Parse(userId));

            if (coordinator == null || resource.Coordinator == null || !resource.Coordinator.Id.Equals(coordinator.Id))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
