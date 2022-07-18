using Application.Mapper.Dtos;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.MedicalWorkers.Requests
{
    public class AddMedicalWorkerProfessionRequest
    {
        public Guid MedicalWorkerId {get;set; }
    }
}
