using Domain.Entities;

using Domain.Entities.Rules.CoordinatorRules;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Builders;
using System.Linq;
using Tests.Mock;

namespace Tests.DomainTests
{
    public class CoordinatorTests
    {
        [Test]
        public void NewCoordinator_WithNotAssignedUser_IsSuccessful()
        {
            List<Coordinator> coordinators = new List<Coordinator>() {
                CoordinatorBuilder
                .Create()
                .AddUser(x=>x.Email("John1223@gmail.com"))
                .Build()
            };
            var mock = GenericCounterMock<Coordinator>.Mock(coordinators).As<IGenericCounter<Coordinator>>().Object;

            var coordinator = CoordinatorBuilder
                .Create()
                .AddUser(x => x.Email("John@gmail.com"))
                .InjectInterface(mock)
                .Build();

            coordinator
               .Should()
               .BeOfType(typeof(Coordinator))
               .And
               .NotBeNull(coordinator.Id.ToString());

            coordinator.User.UserRoles
                .FirstOrDefault(x => x.Equals(UserRole.Coordinator()))
                .Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(UserRole.Coordinator());

        }
        [Test]
        public void NewCoordinator_WithUserAssignedToTwoCoordinator_BreaksUserCannotBeAssignedToCoordinatorMoreThanOnceRule()
        {
            var coordinatorList = new List<Coordinator>() 
            { 
                CoordinatorBuilder
                .Create()
                .AddUser(x=>x.Email("Sock@gmail.com"))
                .Build()
            };

            var mock = GenericCounterMock<Coordinator>.Mock(coordinatorList).As<IGenericCounter<Coordinator>>().Object;

            Action act = () =>
            {
                UserCannotBeAssignedToCoordinatorMoreThanOnce.Check(new UserBuilder().Email("Sock@gmail.com").Build(), mock);
            };

            act
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("User is already assigned to other coordinator");
        }
        [Test]
        public void Coordinator_CannotBeCreated_WithoutUser()
        {
            Action action = () =>
            {
                Coordinator.Create(null,new Mock<IGenericCounter<Coordinator>>().Object);
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("User cannot be null");
        }
    }
}
