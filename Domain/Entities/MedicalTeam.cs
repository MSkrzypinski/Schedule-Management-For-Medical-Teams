using Domain.DDDBlocks;

using Domain.Entities.Rules.MedicalTeamRules;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MedicalTeam : Entity
    {
        public Guid Id { get; set; }
        public InformationAboutTeam InformationAboutTeam { get; set; }
        public Coordinator Coordinator { get; set; }
        public bool IsActive {get;set;}
        private MedicalTeam()
        {
            //For EF
        }
        private MedicalTeam(InformationAboutTeam informationAboutTeam, Coordinator coordinator)
        {
            Id = Guid.NewGuid();
            InformationAboutTeam = informationAboutTeam; 
            Coordinator = coordinator;
            IsActive = true;

            Coordinator.MedicalTeams.Add(this);
        }
        public static MedicalTeam Create(InformationAboutTeam informationAboutTeam, Coordinator coordinator, IGenericCounter<MedicalTeam> medicalTeamCounter)
        {
            if (informationAboutTeam == null)
                throw new ArgumentException("Information about team cannot be null");

            if (coordinator == null)
                throw new ArgumentException("Coordinator cannot be null");

            MedicalTeamMustBeUnique.Check(informationAboutTeam.Code, medicalTeamCounter);

            return new MedicalTeam(informationAboutTeam, coordinator);
        }

    }
}
