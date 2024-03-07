using Domain.DDDBlocks;
using Domain.Entities.Rules.MedicalTeamRules;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class InformationAboutTeam : ValueObject
    { 
        public string Code { get; set; }
        public string City { get; set; }
        public int SizeOfTeam { get; set; }
        public MedicalTeamType MedicalTeamType { get; set; }

        private InformationAboutTeam()
        {
            //For Ef
        }
        public InformationAboutTeam(string code, string city, int sizeOfTeam, MedicalTeamType medicalTeamType)
        {
            SizeOfTeamMustBeGreaterThanOne.Check(sizeOfTeam);

            Code = code;
            City = city;
            SizeOfTeam = sizeOfTeam;
            MedicalTeamType = medicalTeamType;
        }

        public override IEnumerable<object> GetPropertiesToCompare()
        {
            yield return Code;
            yield return City;
            yield return SizeOfTeam;
            yield return MedicalTeamType;
        }
    }
}
