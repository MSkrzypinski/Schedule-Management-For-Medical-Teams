using Application.Mapper.Dtos;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MedicalWorkers.AddMedicalWorkerProfession
{
    public class AddMedicalWorkerProfessionCommand : IRequest<Unit>
    {
        public Guid MedicalWorkerId { get; set; }
        public MedicalWorkerProfessionEnum MedicalWorkerProfessionEnum { get; set; }
    }
}
