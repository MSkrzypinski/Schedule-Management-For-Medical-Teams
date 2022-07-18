using Domain.Entities;

using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Builders
{
    public class CoordinatorBuilder
    {
        private User user = new UserBuilder().Build();
        private List<MedicalTeam> medicalTeams = new List<MedicalTeam>();
        private IGenericCounter<Coordinator> _coordinatorCounter = new Mock<IGenericCounter<Coordinator>>().Object;

        public static CoordinatorBuilder Create()
        {
            return new CoordinatorBuilder();
        }
        public CoordinatorBuilder AddUser(Action<UserBuilder> action)
        {
            var userBuilder = new UserBuilder();
            action(userBuilder);
            user = userBuilder.Build();
            return this;
        }
        public CoordinatorBuilder InjectInterface(IGenericCounter<Coordinator> coordinatorCounter)
        {
            _coordinatorCounter = coordinatorCounter;
            return this;
        }
        public Coordinator Build()
        {
            return Coordinator.Create(user,_coordinatorCounter);
        }
    }
}
