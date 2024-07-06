using Application.Utilities;
using System.Text.Json;

namespace Application.ErrorHandlers
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string Date { get; set; } = DateTimeHelper.FormatDateTime(DateTime.UtcNow);

        public override string ToString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(this, options);
        }
    }

    public class MessageDetail : ErrorDetail { }
}
