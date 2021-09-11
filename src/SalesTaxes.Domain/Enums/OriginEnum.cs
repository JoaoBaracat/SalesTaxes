using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SalesTaxes.Domain.Enums
{
    public static class OriginEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Origin
        {
            [Description("National")]
            National = 0,

            [Description("Imported")]
            Imported = 1
        }
    }
}