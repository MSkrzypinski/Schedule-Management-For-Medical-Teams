﻿using Application.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MedicalTeams
{
    public class MedicalTeamRepository : BaseRepository<MedicalTeam>, IMedicalTeamRepository
    {
        public MedicalTeamRepository(ScheduleManagementContext scheduleManagementContext) : base(scheduleManagementContext)
        {

        }

    }
}
