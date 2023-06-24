using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dziennik.Shared.User
{
    public class ShortListResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
    }
}
