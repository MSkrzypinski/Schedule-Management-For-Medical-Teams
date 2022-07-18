using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MedicalWorkerProfessionEnum
    {
        Nurse,
        Doctor,
        Paramedic,
        BasicMedic
    }
}
