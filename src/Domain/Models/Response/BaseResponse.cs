using System.Net;

namespace Domain.Models.Response
{
    public record BaseResponse<T>
    {
        public HttpStatusCode StatusCode { get; init; }

        public bool IsSuccess { get; init; }

        public IEnumerable<string> Message { get; init; } = Enumerable.Empty<string>();

        public virtual T? Model { get; init; }

        public virtual IEnumerable<T>? Items { get; init; }
    }
}
