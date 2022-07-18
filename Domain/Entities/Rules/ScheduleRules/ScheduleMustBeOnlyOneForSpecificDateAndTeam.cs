
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities.Rules.ScheduleRules
{
    public class ScheduleMustBeOnlyOneForSpecificDateAndTeam
    {
        public static string Message = "Schedule must be only one for specific date and team";
        private static IGenericCounter<Schedule> _scheduleCounter;
        public static void Check(MedicalTeam medicalTeam,MonthAndYearOfSchedule monthAndYearOfSchedule, IGenericCounter<Schedule> scheduleCounter)
        {
            _scheduleCounter = scheduleCounter;

            if (_scheduleCounter.Count(x=>x.MedicalTeam.InformationAboutTeam.Code.Equals(medicalTeam.InformationAboutTeam.Code) 
                && x.MonthAndYearOfSchedule.Equals(monthAndYearOfSchedule)).Result>0)
            {
               throw new ApplicationException(Message);
            }
        }
    }
}
