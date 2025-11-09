using System;

namespace Umuna.Core.Domain.Interfaces
{
    /// <summary>
    /// Provides a creation timestamp for domain entities.
    /// </summary>
    public interface IHasCreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}