using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence;
using Domain.Entities;
using Domain.Entities.Rules.ShiftRules;
using Domain.ValueObjects.Enums;
using MediatR;

namespace Application.MedicalWorkers.GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamId
{
    public class GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdQueryHandler : IRequestHandler<GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdQuery, IList<MedicalWorker>>
    {
        private readonly IMedicalWorkerRepository _medicalWorkerRepository;
        private readonly IShiftRepository _shiftRepository;

        public GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdQueryHandler(IMedicalWorkerRepository medicalWorkerRepository, IShiftRepository shiftRepository)
        {
            _medicalWorkerRepository = medicalWorkerRepository;
            _shiftRepository = shiftRepository;
        }

        public async Task<IList<MedicalWorker>> Handle(GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdQuery request, CancellationToken cancellationToken)
        {
            var shift = await _shiftRepository.GetByIdAsync(request.ShiftId);
            if(shift == null)
            {
                throw new ApplicationException("Shift not found");
            }
            var medicalWorkers = await _medicalWorkerRepository.GetMedicalWorkersAssignedToMedcialTeamByMedcialTeamIdAsync(request.MedicalTeamId,request.MedicRole);
            if(medicalWorkers == null)
            {
                return medicalWorkers;
            }
            var tbl = new List<MedicalWorker>();
            var flag = false;
            foreach (var medicalWorker in medicalWorkers)
            {
                flag = false;
                foreach (var item in medicalWorker.Shifts)
                {
                    if (shift.DateRange.Includes(item.DateRange))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    tbl.Add(medicalWorker);
                }
            }
            //medicalWorkers = tbl;
            medicalWorkers = medicalWorkers.Filter(x => !x.Shifts.Any(x => x.DateRange.Includes(shift.DateRange)) || x.Shifts.Length() == 0).ToList();
            medicalWorkers = medicalWorkers.Filter(x=> !x.DaysOff.Any(x=>x.Includes(shift.DateRange.Start,shift.DateRange.End)) || x.DaysOff.Length() == 0).ToList();

            if(request.MedicRole.Equals(MedicRole.Driver))
            {
                medicalWorkers = medicalWorkers.Filter(med=> 
                !med.Shifts.Any(x => x.Driver != null && x.Driver.Id.Equals(med.Id) 
                    && ((Math.Abs((x.DateRange.Start - shift.DateRange.End).TotalHours) < 12) 
                    || (Math.Abs((x.DateRange.End - shift.DateRange.Start).TotalHours) < 12)))).ToList();
                
            }

            return medicalWorkers;

        }
    }
}