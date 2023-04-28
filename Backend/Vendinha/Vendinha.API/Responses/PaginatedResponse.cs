using System.Text.Json.Serialization;

namespace Vendinha.API.Responses
{
    public class PaginatedResponse : Response
    {
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; protected set; }
        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; protected set; }
        [JsonPropertyName("totalPages")]
        public int TotalPages => TotalRecords / 10 + 1;

        public PaginatedResponse(int currentPage, int totalRecords = 0, object? data = default, bool hasError = false, string? message = default) : base(data, hasError, message)
        {
            CurrentPage = currentPage;
            TotalRecords = totalRecords;
        }
    }
}
