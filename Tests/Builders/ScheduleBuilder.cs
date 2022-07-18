using Domain.Entities;

using Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Builders
{
    public class ScheduleBuilder
    {
        private MedicalTeam medicalTeam = new MedicalTeamBuilder().Build();
        private MonthAndYearOfSchedule monthhAndYearOfSchedule = new MonthAndYearOfSchedule(DateTime.Now.Year, 2);
        private IGenericCounter<Schedule> _scheduleCounter = new Mock<IGenericCounter<Schedule>>().Object;
        public static ScheduleBuilder Create()
        {
            return new ScheduleBuilder();
        }
        public ScheduleBuilder AddMedicalTeam(Action<MedicalTeamBuilder> action)
        {
            var medicalTeamBuilder = new MedicalTeamBuilder();
            action(medicalTeamBuilder);
            medicalTeam = medicalTeamBuilder.Build();
            return this;
        }
        public ScheduleBuilder AddMonthhAndYearOfSchedule(int year,int month)
        {
            monthhAndYearOfSchedule = new MonthAndYearOfSchedule(year,month);
            return this;
        }
        public ScheduleBuilder InjectInterface(IGenericCounter<Schedule> scheduleCounter)
        {
            _scheduleCounter = scheduleCounter;
            return this;
        }
        public Schedule Build()
        {
            return Schedule.Create(medicalTeam, monthhAndYearOfSchedule, _scheduleCounter);
        }
    }
}
