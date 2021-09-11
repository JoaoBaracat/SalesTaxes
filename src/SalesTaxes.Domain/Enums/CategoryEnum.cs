using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SalesTaxes.Domain.Enums
{
    public static class CategoryEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Category
        {
            [Description("Books")]
            Books = 0,

            [Description("Food")]
            Food = 1,

            [Description("Medical products")]
            MedicalProducts = 2,

            [Description("Cosmetics")]
            Cosmetics = 3,

            [Description("Others")]
            Others = 4
        }
    }
}