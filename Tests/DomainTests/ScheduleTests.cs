using Domain.Entities;

using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Builders;
using Tests.Mock;
using System.Linq;
using Domain.Entities.Rules.ScheduleRules;
using Domain.ValueObjects.Enums;

namespace Tests.DomainTests
{
    public class ScheduleTests
    {
      
        [Test]
        public void NewSchedule_WithCorrectData_IsSuccessful()
        {
            var listOfSchedules = new List<Schedule>() { new ScheduleBuilder().Build() };

            var mock = GenericCounterMock<Schedule>.Mock(listOfSchedules).As<IGenericCounter<Schedule>>().Object;

            var schedule = ScheduleBuilder
                .Create()
                .AddMonthhAndYearOfSchedule(DateTime.Now.Year, 4)
                .InjectInterface(mock)
                .Build();

            schedule
                .Should()
                .BeOfType(typeof(Schedule))
                .And
                .NotBeNull(schedule.Id.ToString());

            schedule.Shifts
                .Should()
                .HaveCount(DateTime.DaysInMonth(schedule.MonthAndYearOfSchedule.Year, schedule.MonthAndYearOfSchedule.Month)*2);
        }
        [Test]
        public void NewSchedule_CreateSecondScheduleForTheSameTeamAndDate_BreaksScheduleMustBeOnlyOneForSpecificDateAndTeamRule()
        {
            var listOfSchedules = new List<Schedule>()
            {
                new ScheduleBuilder()
                    .AddMedicalTeam(x=>x.AddInformationAboutTeam("Warsaw12_P", "Warsaw", 3, MedicalTeamType.P))
                    .AddMonthhAndYearOfSchedule(DateTime.Now.Year, 3)
                    .Build()
            };

            var mock = GenericCounterMock<Schedule>.Mock(listOfSchedules).As<IGenericCounter<Schedule>>().Object;

            var medicalTeam = new MedicalTeamBuilder()
                .AddInformationAboutTeam("Warsaw12_P","Warsaw",3,MedicalTeamType.P)
                .Build();

            var monthAndYearOfSchedule = new MonthAndYearOfSchedule(DateTime.Now.Year, 3);

            Action act = () =>
            {
                ScheduleMustBeOnlyOneForSpecificDateAndTeam.Check(medicalTeam,monthAndYearOfSchedule,mock);
            };

            act
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Schedule must be only one for specific date and team");
        }

        [TestCase(0)]
        [TestCase(13)]
        [Test]
        public void Schedule_CannotBeCreated_WithInvalidMonth(int month)
        {
            Action action = () =>
            {
                ScheduleBuilder
                .Create()
                .AddMonthhAndYearOfSchedule(DateTime.Now.Year, month)
                .Build();
            };
            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage("Month must be in range 1-12");
        }
        [Test]
        public void Schedule_CannotBeCreated_WithInvalidYear()
        {
            var year = DateTime.Now.Year + 2;

            Action action = () =>
            {
                ScheduleBuilder
                .Create()
                .AddMonthhAndYearOfSchedule(year, 10)
                .Build();
            };
            action
                .Should()
                .Throw<ApplicationException>()
                .WithMessage($"Year must be {DateTime.Now.Year} or {DateTime.Now.Year + 1}");
        }

        [Test]
        public void Schedule_CannotBeCreated_WithoutMedicalTeam()
        {
            Action action = () =>
            {
                Schedule
                .Create
                (
                    null,
                    new MonthAndYearOfSchedule(DateTime.Now.Year, 2),
                    new Mock<IGenericCounter<Schedule>>().Object
                );

            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Medical team cannot be null");
        }
        [Test]
        public void Schedule_CannotBeCreated_WithoutYearAndMonth()
        {
            Action action = () =>
            {
                Schedule
                .Create
                (
                    new MedicalTeamBuilder().Build(),
                    null,
                    new Mock<IGenericCounter<Schedule>>().Object
                );

            };

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Month and year cannot be null");
        }
       
      
    }
}
