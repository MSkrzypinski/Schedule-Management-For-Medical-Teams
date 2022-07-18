using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MedicalTeamType
    {
        P = 0,
        S = 1,
        N = 2,
        T = 3
    }
}
