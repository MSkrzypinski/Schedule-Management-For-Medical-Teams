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

using System.Linq;
using System.Linq.Expressions;
using Moq;
using Tests.Mock;
using Domain.Entities.Rules.MedicalWorkerRules;
using Domain.Entities.Rules.EmploymentContractRules;

namespace Tests.DomainTests
{
    public class MedicalWorkerTests
    {
        [Test]
        public void NewMedicalWorker_WithAssignedUser_IsSuccessful()
        {
            List<MedicalWorker> medicalWorkers = new List<MedicalWorker>() { 
                MedicalWorkerBuilder
                .Create()
                .AddUser(x=>x.Email("John1223@gmail.com"))
                .Build() 
            };
            var mock = GenericCounterMock<MedicalWorker>.Mock(medicalWorkers).As<IGenericCounter<MedicalWorker>>().Object;

            var medicalWorker = MedicalWorkerBuilder
                .Create()
                .AddUser(x=>x.Email("John@gmail.com"))
                .InjectInterface(mock)
                .Build();

            medicalWorker
               .Should()
               .BeOfType(typeof(MedicalWorker))
               .And
               .NotBeNull(medicalWorker.Id.ToString());

            medicalWorker.User.UserRoles
             .Should()
             .HaveCount(1)
             .And
             .Match(x => x.Any(x => x.Equals(UserRole.MedicalWorker())));

        }
        [Test]
        public void NewMedicalWorker_WithUserAssignedToTwoMedicalWorker_BreaksUserCannotBeAssignedToMedicalWorkerMoreThanOnceRule()
        {
            List<MedicalWorker> medicalWorkers = new List<MedicalWorker>()
            {
                MedicalWorkerBuilder
                .Create()
                .AddUser(x=>x.Email("John@gmail.com"))
                .Build()
            };

            var mock = GenericCounterMock<MedicalWorker>.Mock(medicalWorkers).As<IGenericCounter<MedicalWorker>>().Object;

            Action act = () =>
            {
                UserCannotBeAssignedToMedicalWorkerMoreThanOnce.Check(new UserBuilder().Email("John@gmail.com").Build(),mock);
            };

            act.Should()
                .Throw<ApplicationException>()
                .WithMessage("User is already assigned to other medical worker");
            
        }
        [Test]
        public void EmploymentContract_CannotBeCreated_WithoutCorrectMedicalWorkerProfession_BreaksMedicalWorkerMustHaveTheCorrectMedicalWorkerProfessionRule()
        {
            var medicalWorker = new MedicalWorkerBuilder().AddMedicalWorkerProfession(new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Doctor)).Build();

            Action action = () =>
            {
                MedicalWorkerMustHaveTheCorrectMedicalWorkerProfession.Check(medicalWorker, new MedicalWorkerProfession(MedicalWorkerProfessionEnum.Nurse));
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Medical worker must have the correct medical worker type");
        }
        [Test]
        public void MedicalWorker_CannotBeCreated_WithoutAddress()
        {
            Action action = () =>
            {
                MedicalWorker.Create
                (
                    null,
                    new DateTime(1999,2,2),
                    new UserBuilder().Build(),
                    new Mock<IGenericCounter<MedicalWorker>>().Object
                );
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Address cannot be null");
        }
        [Test]
        public void MedicalWorker_AddDayOff_WithStartDateGreaterThanEndDate_BreaksStartDateMustBeEarlierThanEndDateRule()
        {

            var startDate = DateTime.Now.AddDays(2);
            var endDate = DateTime.Now;

            Action action = () =>
            {
                StartDateMustBeEarlierThanEndDate.Check(startDate, endDate);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Start date must be earlier than end date");
        }
        [Test]
        public void MedicalWorker_AddDayOff_WithPastStartDate_BreaksStartDateMustNotBePastDateRule()
        {

            var startDate = DateTime.Now.AddDays(-2);

            Action action = () =>
            {
                StartDateMustNotBePastDate.Check(startDate);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Start date mustn't be past date");
        }
        [Test]
        public void MedicalWorker_CannotBeCreated_WithoutUser()
        {
            Action action = () =>
            {
                MedicalWorker.Create
                (
                    new Address("Warsaw","95-200","Warsaw",2,2),
                    new DateTime(1999, 2, 2),
                    null,
                    new Mock<IGenericCounter<MedicalWorker>>().Object
                );
            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("User cannot be null");
        }
        [Test]
        public void MedicalWorker_CannotBeAddedDaysOff_MonthIsIncorrect_BreaksDaysOffCanOnlyBeAddedForTheNextMonthRule()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            Action action = () =>
            {
                DaysOffCanOnlyBeAddedForTheNextMonth.Check(start,end);
            };

            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Days off can only be added for the next month");

        }
        [Test]
        public void MedicalWorker_TryingToAddDaysOffInLast10DaysOfMonth_CannotBeAddedDaysOff_BreaksDaysOffCannotBeAddedInTheLast10DaysOfMonthRule()
        {
            Action action = () =>
            {
                DaysOffCannotBeAddedInTheLast10DaysOfMonth.dateTime = new DateTime(2022, 01, 22);
                DaysOffCannotBeAddedInTheLast10DaysOfMonth.Check();
            };
            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Days off cannot be add in the last 10 days of month");
        }
        [Test]

        public void MedicalWorker_AddDaysOff_IfDaysOffAreOverlapTheyShouldUnionInOneDayOff()
        {
            var medicalWorker = new MedicalWorkerBuilder().Build();

            medicalWorker.AddDayOff(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 2, 19, 00, 00).AddMonths(1), 
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 19, 00, 00).AddMonths(1));

            medicalWorker.AddDayOff(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 19, 00, 00).AddMonths(1), 
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5, 19, 00, 00).AddMonths(1));

            medicalWorker.AddDayOff(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 19, 00, 00).AddMonths(1), 
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 19, 00, 00).AddMonths(1));

            medicalWorker.AddDayOff(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 3, 19, 00, 00).AddMonths(1), 
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 19, 00, 00).AddMonths(1));


            medicalWorker.DaysOff
                .Should()
                .HaveCount(1)
                .And
                .Contain(
                    new DayOff(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 19, 00, 00).AddMonths(1), 
                        new DateTime(DateTime.Now.Year, DateTime.Now.Month, 7, 19, 00, 00).AddMonths(1)));
        }
    }
}
