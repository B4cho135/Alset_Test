using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegistrantRoleFilter
    {
        Administrator,
        User
    }
}
