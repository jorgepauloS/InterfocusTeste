using System.Text.Json.Serialization;

namespace Vendinha.Commons.DTOs
{
    public class BaseDto<T>
    {
        public T Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
    }
}
