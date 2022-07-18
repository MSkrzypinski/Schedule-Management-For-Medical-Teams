using Application.Persistence;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MedicalTeams
{
    public class MedicalTeamRepository : BaseRepository<MedicalTeam>, IMedicalTeamRepository
    {
        public MedicalTeamRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {

        }
    }
}
