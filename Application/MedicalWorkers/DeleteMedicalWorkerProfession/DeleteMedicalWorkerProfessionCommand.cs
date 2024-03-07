using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects.Enums;
using MediatR;

namespace Application.MedicalWorkers.DeleteMedicalWorkerProfession
{
    public class DeleteMedicalWorkerProfessionCommand : IRequest<Unit>
    {
        public Guid medicalWorkerId { get; set; }
        public MedicalWorkerProfessionEnum medicalWorkerProfessionEnum { get; set; }
    }
}