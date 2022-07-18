using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MedicRole
    {
        Driver = 0,
        Manager = 1,
        RegularMedic = 2
    }
}
