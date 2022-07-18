using Domain.Entities;

using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Builders
{
    public class MedicalTeamBuilder
    {
        private InformationAboutTeam informationAboutTeam = new InformationAboutTeam("WFD3212CSD2", "Warsaw", 3, MedicalTeamType.S);
        private Coordinator coordinator = new CoordinatorBuilder().Build();
        private IGenericCounter<MedicalTeam> _medicalTeamCounter = new Mock<IGenericCounter<MedicalTeam>>().Object;

        public static MedicalTeamBuilder Create()
        {
            return new MedicalTeamBuilder();
        }
        public MedicalTeamBuilder AddInformationAboutTeam(string code, string city, int sizeOfTeam, MedicalTeamType medicalTeamType)
        {
            informationAboutTeam = new InformationAboutTeam(code, city, sizeOfTeam, medicalTeamType);
            return this;
        }
        public MedicalTeamBuilder AddCoordinator(Action<CoordinatorBuilder> action)
        {
            var coordinatorBuilder = new CoordinatorBuilder();
            action(coordinatorBuilder);
            coordinator = coordinatorBuilder.Build();
            return this;
        }
        public MedicalTeamBuilder InjectInterface(IGenericCounter<MedicalTeam> medicalTeamCounter)
        {
            _medicalTeamCounter = medicalTeamCounter;
            return this;
        }
        public MedicalTeam Build()
        {
            return MedicalTeam.Create(informationAboutTeam, coordinator, _medicalTeamCounter);
        }
    }
}
