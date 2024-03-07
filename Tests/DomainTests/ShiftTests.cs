using Domain.Entities;

using Domain.Entities.Rules.ShiftRules;
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

namespace Tests.DomainTests
{
    public class ShiftTests
    {
        [Test]
        public void Shift_CannotBeCreated_WithoutDateRange()
        {
            Action action = () =>
            {
                Shift.Create
                (
                    null, 
                    new MedicalTeamBuilder().Build(), 
                    new ScheduleBuilder().Build()
                );
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Date range cannot be null");
        }
        [Test]
        public void Shift_CannotBeCreated_WithoutMedicalTeam()
        {
            Action action = () =>
            {
                Shift.Create
                (
                    new DateRange(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0), 
                                  new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0)),
                    null,
                    new ScheduleBuilder().Build()
                ) ;
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Medical team cannot be null");
        }
        [Test]
        public void Shift_CannotBeCreated_WithoutSchedule()
        {
            Action action = () =>
            {
                Shift.Create
                (
                    new DateRange(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0),
                                  new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0)),
                    new MedicalTeamBuilder().Build(),
                    null
                );
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Schedule cannot be null");
        }
        [Test]
        public void Shift_NumberOfMedcialWorkerMustNotBeMoreThanLimit_BreaksNumberOfMedicsMustBeLessThanOrEqualToMaximumRule()
        {
            var shift = ShiftBuilder
                .Create()
                .Build();

            var driver = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            var manager = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Doctor)).Build();

            var memberOfCrew = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            driver.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Driver, MedicalWorkerProfessionEnum.Paramedic);
            memberOfCrew.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.RegularMedic, MedicalWorkerProfessionEnum.Paramedic);
            manager.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Manager, MedicalWorkerProfessionEnum.Doctor);

            shift.AddOrChangeDriver(driver);
            shift.AddCrewMember(memberOfCrew);
            shift.AddOrChangeCrewManager(manager);

            var medicalTeam = MedicalTeamBuilder
                .Create()
                .AddInformationAboutTeam("95-200","Warsaw",2,MedicalTeamType.S)
                .Build();

            Action action = () =>
            {
                NumberOfMedicsMustBeLessThanOrEqualToMaximum.Check(medicalTeam,shift.GetQuantityOfMedics());
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Number of medics must be less than or equal to maximum");
        }
        [Test]
        public void Shift_AddMedicToShift_MedicMustHaveEmploymentContractForSpecificTeam_BreaksMedicHaveContractForSpecificMedicTeamRule()
        {
            var medicalTeam = new MedicalTeamBuilder().Build();
            var medicalWorker = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            medicalWorker.AddEmploymentContract(medicalTeam, ContractType.BusinessToBusiness, MedicRole.Driver, MedicalWorkerProfessionEnum.Paramedic);

            Action action = () =>
            {
                MedicHaveContractForSpecificMedicTeam.Check(medicalWorker,medicalTeam,MedicRole.Manager);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Medic have not a contract for this team");
        }
       
        [Test]
        [TestCase("2022/03/02 19:00:00", "2022/03/03 07:00:00")]
        [TestCase("2022/03/04 19:00:00", "2022/03/05 07:00:00")]
        public void Shift_AddDriver_DriverNeedAtLeastTwelveHoursBreakBeforeTheNextShift_BreaksDriverMustHaveAtLeastTwelveHoursBreakBeforeBeganTheNextShiftRule(DateTime start,DateTime end)
        {
            start = new DateTime(DateTime.Now.Year,start.Month,start.Day,start.Hour,start.Minute,start.Second);
            end = new DateTime(DateTime.Now.Year,end.Month,end.Day,end.Hour,end.Minute,end.Second);
            var driver = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();
            driver.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Driver, MedicalWorkerProfessionEnum.Paramedic);

            var firstShift = new ShiftBuilder().AddDateRange(start, end).Build();

            firstShift.AddOrChangeDriver(driver);

            driver.Shifts.Add(firstShift);

            var secondShift = new ShiftBuilder()
                .AddDateRange(new DateTime(DateTime.Now.Year, 3, 3, 18, 0, 0), new DateTime(DateTime.Now.Year, 3, 4, 08, 0, 0))
                .Build();
        
            Action action = () =>
            {
                DriverMustHaveAtLeastTwelveHoursBreakBeforeBeganTheNextShift.Check(driver,secondShift);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Driver must have 12 hours break between shifts");
        }
       
        [Test]
        [TestCase("2022/03/01 19:00:00", "2022/03/02 07:20:00")]
        [TestCase("2022/03/02 18:00:00", "2022/03/03 07:00:00")]
        [TestCase("2022/03/02 10:00:00", "2022/03/02 11:00:00")]
        [TestCase("2022/03/02 07:00:00", "2022/03/02 19:00:00")]
        public void Shift_AddMedicToShift_CannotBeAddedIfMedicAlreadyWorkInThisTermOrHaveADayOff_BreaksMedicCanWorkInThisTermRule(DateTime start,DateTime end)
        {
            var medic = new MedicalWorkerBuilder().Build();

            var firstShift = new ShiftBuilder().AddDateRange(start, end).Build();
            
            medic.Shifts.Add(firstShift);

            var secondShift = new ShiftBuilder()
               .AddDateRange(new DateTime(2022, 3, 2, 7, 0, 0), new DateTime(2022, 3, 2, 19, 0, 0))
               .Build();

            Action action = () =>
            {
                MedicCanWorkInThisTerm.Check(medic,secondShift);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Medic must not work in this term");
        }
        [Test]
        public void Shift_Publish_TheNumberOfMedicsMustBeTheSameAsSizeOfTeam_BreaksToPublishShiftNumberOfMedicsMustBeEqualToSizeOfTeamRule()
        {
            var shift = new ShiftBuilder()
                .AddMedicalTeam(x => x.AddInformationAboutTeam("Warsaw1P", "Warsaw", 3, MedicalTeamType.P))
                .Build();

            Action action = () =>
            {
                ToPublishShiftNumberOfMedicsMustBeEqualToSizeOfTeam.Check(shift);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("To publish number of medics must be equal to size of team");
        }
        [Test]
        public void Shift_AddMedicalWorkerToShift_IsSuccessful()
        {
            var shift = new ShiftBuilder().Build();

            var medicalWorker = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            medicalWorker.AddEmploymentContract( new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Driver, MedicalWorkerProfessionEnum.Paramedic);

            shift.AddOrChangeDriver(medicalWorker);

            shift.Driver
                .Should()
                .BeOfType(typeof(MedicalWorker))
                .And
                .NotBeNull();
        }
        [Test]
        public void Shift_CannotBePublished_WithoutDriver()
        {
            var shift = new ShiftBuilder().Build();
            var manager = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            manager.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Manager, MedicalWorkerProfessionEnum.Paramedic);

            shift.AddOrChangeCrewManager(manager);

            Action action = () =>
            {
                shift.Publish();
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Driver cannot be null");
        }
        [Test]
        public void Shift_CannotBePublished_WithoutManager()
        {
            var shift = new ShiftBuilder().Build();
            var driver = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            driver.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Driver, MedicalWorkerProfessionEnum.Paramedic);
            
            shift.AddOrChangeDriver(driver);

            Action action = () =>
            {
                shift.Publish();
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Manager cannot be null");
        }
        [Test]
        public void Shift_Published_IsSuccessful()
        {
            var shift = new ShiftBuilder().Build();

            var driver = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            var manager = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Doctor)).Build();

            var memberOfCrew = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Paramedic)).Build();

            driver.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Driver, MedicalWorkerProfessionEnum.Paramedic);
            memberOfCrew.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.RegularMedic, MedicalWorkerProfessionEnum.Paramedic);
            manager.AddEmploymentContract(new MedicalTeamBuilder().Build(), ContractType.BusinessToBusiness, MedicRole.Manager, MedicalWorkerProfessionEnum.Doctor);

            shift.AddOrChangeCrewManager(manager);
            shift.AddOrChangeDriver(driver);
            shift.AddCrewMember(memberOfCrew);

            shift.Publish();

            shift.Driver.Shifts.Count.Should().Be(1);
            shift.Manager.Shifts.Count.Should().Be(1);
            shift.CrewMember.Shifts.Count.Should().Be(1);
        }
    }
}
