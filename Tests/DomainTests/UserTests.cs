using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Domain.Entities.Rules.UserRules;
using Domain.Entities.Rules.MedicalTeamRules;
using Domain.Entities.Rules.ScheduleRules;
using FluentAssertions;
using Tests.Builders;
using Moq;
using System.Linq.Expressions;

using System.Linq;
using Domain.DDDBlocks;
using Tests.Mock;

namespace Tests.DomainTests
{
    public class UserTests
    {
        [Test]
        public void NewUserRegistration_WithoutUniqueEmail_BreaksUserMustBeUniqueRule()
        {
            List<User> users = new List<User>() { UserBuilder.Create().Email("WARS").Build() };
            var mock = GenericCounterMock<User>.Mock(users).As<IGenericCounter<User>>().Object;

            Action act = () =>
            {
                UserMustBeUnique.Check(new Email("WARS"),mock);    
            };

            act.Should().Throw<ApplicationException>()
                .WithMessage("User must be unique");
        }
        [Test]
        public void NewUserRegistration_WithUniqueEmail_IsSuccessful()
        {
            List<User> users = new List<User>() { UserBuilder.Create().Email("WARS").Build() };
            var mock = GenericCounterMock<User>.Mock(users).As<IGenericCounter<User>>().Object;

            var user = UserBuilder
            .Create()
            .Email("SRAW")
            .InjectInterface(mock)
            .Build();

            user
                .Should()
                .BeOfType(typeof(User))
                .And
                .NotBeNull(user.Id.ToString());
        }
        [Test]
        public void User_CannotBeCreated_WithoutName()
        {
            Action action = () =>
            {
                User
                .RegisterNewUser
                (
                    null,
                    new Password("sdfsdfs"),
                    new PhoneNumber("543543543"),
                    new Email("John@gmail.com"),
                    new Mock<IGenericCounter<User>>().Object
                );

            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Name cannot be null");
        }
        [Test]
        public void User_CannotBeCreated_WithoutPassword()
        {
            Action action = () =>
            {
                User
                .RegisterNewUser
                (
                    new Name("John","Sock"),
                    null,
                    new PhoneNumber("543543543"),
                    new Email("John@gmail.com"),
                    new Mock<IGenericCounter<User>>().Object
                );

            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Password cannot be null");
        }
        [Test]
        public void User_CannotBeCreated_WithoutPhoneNumber()
        {
            Action action = () =>
            {
                User
                .RegisterNewUser
                (
                    new Name("John", "Sock"),
                    new Password("sdfsdfs"),
                    null,
                    new Email("John@gmail.com"),
                    new Mock<IGenericCounter<User>>().Object
                );

            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Phone number cannot be null");
        }
        [Test]
        public void User_CannotBeCreated_WithoutEmail()
        {
            Action action = () =>
            {
                User
                .RegisterNewUser
                (
                    new Name("John", "Sock"),
                    new Password("sdfsdfs"),
                    new PhoneNumber("543543543"),
                    null,
                    new Mock<IGenericCounter<User>>().Object
                );

            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Email cannot be null");
        }
    }
}
