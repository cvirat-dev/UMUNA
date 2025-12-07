using System;

namespace Umuna.Core.Contracts.Api
{
    public interface IReadDto
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
