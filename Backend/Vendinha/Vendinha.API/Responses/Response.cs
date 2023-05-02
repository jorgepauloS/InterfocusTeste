using System.Text.Json.Serialization;

namespace Vendinha.API.Responses
{
    public class Response
    {
        [JsonPropertyName("hasErrors")]
        public bool HasErrors { get; protected set; }
        [JsonPropertyName("message")]
        public string? Message { get; protected set; }
        [JsonPropertyName("data")]
        public object? Data { get; protected set; }

        public Response(object? data = default, bool hasError = false, string? message = default)
        {
            Data = data;
            HasErrors = hasError;
            Message = message;
        }
    }
}
