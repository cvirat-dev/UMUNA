using System;

namespace Umuna.Core.Contracts.DTOs
{
    public interface IReadDto
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
