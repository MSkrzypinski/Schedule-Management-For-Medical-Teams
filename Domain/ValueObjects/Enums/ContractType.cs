using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContractType
    {
        BusinessToBusiness = 0,
        IndefiniteContract = 1,
        ContractOfService = 2
    }
}
