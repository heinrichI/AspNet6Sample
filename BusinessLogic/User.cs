using System.ComponentModel.DataAnnotations;

namespace BusinessLogic
{
    public record User
    {
        public string Name { get; init; }

        public byte[]? Password { get; init; }

        public string? Email { get; init; }
    }
}