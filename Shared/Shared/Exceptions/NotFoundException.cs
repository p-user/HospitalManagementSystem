
namespace Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public string? Details { get; set; }
        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message,string details) : base(message)
        {
            Details = details;
        }

    }
}
