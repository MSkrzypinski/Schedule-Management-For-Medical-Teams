using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization
{
    public class UserMustBeThisMedicalWorkerRequirementHandler : AuthorizationHandler<UserMustBeThisMedicalWorkerRequirement,MedicalWorker>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserMustBeThisMedicalWorkerRequirement requirement, MedicalWorker resource)
        {
            var userId = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (resource == null || !resource.User.Id.ToString().Equals(userId))
            {
               context.Fail();
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
