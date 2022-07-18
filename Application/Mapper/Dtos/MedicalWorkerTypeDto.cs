using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Mapper.Dtos
{
    public class MedicalWorkerProfessionDto
    {
        public MedicalWorkerProfessionEnum MedicalWorkerProfessionEnum { get; set; }
    }
}
