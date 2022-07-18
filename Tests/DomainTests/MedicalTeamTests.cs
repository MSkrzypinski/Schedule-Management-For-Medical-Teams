using Domain.Entities;

using Domain.Entities.Rules.MedicalTeamRules;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Builders;
using Tests.Mock;

namespace Tests.DomainTests
{
    public class MedicalTeamTests
    {
        [Test]
        public void NewMedicalTeam_WithoutUniqueMedicalTeam_BreaksMedicalTeamMustBeUniqueRule()
        {
            List<MedicalTeam> medicalTeams = new List<MedicalTeam>()
            {
                new MedicalTeamBuilder()
                .AddInformationAboutTeam("92-100","Warsaw",3,MedicalTeamType.P)
                .Build() 
            };

            var mock = GenericCounterMock<MedicalTeam>.Mock(medicalTeams).As<IGenericCounter<MedicalTeam>>().Object;

            Action act = () =>
            {
                MedicalTeamMustBeUnique.Check("92-100",mock);
            };

            act
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Medical team must be unique");
        }

        [Test]
        public void NewMedicalTeam_WithUniqueMedicalTeam_IsSuccessful()
        {
            List<MedicalTeam> medicalTeams = new List<MedicalTeam>()
            {
                new MedicalTeamBuilder()
                .Build()
            };

            var mock = GenericCounterMock<MedicalTeam>.Mock(medicalTeams);

            var medicalTeam = MedicalTeamBuilder
                .Create()
                .AddInformationAboutTeam("DDD3212CSD2", "Warsaw", 3, MedicalTeamType.S)
                .Build();          

            medicalTeam
                .Should()
                .BeOfType(typeof(MedicalTeam))
                .And
                .NotBeNull(medicalTeam.Id.ToString());

            medicalTeam.Coordinator.MedicalTeams
              .Should()
              .HaveCount(1)
              .And
              .Match(x => x.Any(x => x.Id.Equals(medicalTeam.Id)));
        }
        [Test]
        public void NewMedicalTeam_CannotBeCreatedWithSizeOfTeamLessThanTwoMedics_BreaksSizeOfTeamMustBeGreaterThanOneRule()
        {
            Action action = () =>
            {
                SizeOfTeamMustBeGreaterThanOne.Check(1);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Size of team must be greater than one");
        }
        [Test]
        public void MedicalTeam_CannotBeCreated_WithoutInformationAboutTeam()
        {

            Action action = () =>
            {
                MedicalTeam.Create
                (
                    null, 
                    new CoordinatorBuilder().Build(),
                    new Mock<IGenericCounter<MedicalTeam>>().Object
                );
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Information about team cannot be null");
        }
        [Test]
        public void MedicalTeam_CannotBeCreated_WithoutCoordinator()
        {

            Action action = () =>
            {
                MedicalTeam.Create
                (
                    new InformationAboutTeam("Warsaw21","Warsaw",3,MedicalTeamType.P),
                    null,
                    new Mock<IGenericCounter<MedicalTeam>>().Object
                );
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Coordinator cannot be null");
        }
    }
}
