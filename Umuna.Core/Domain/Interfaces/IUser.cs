using System;
using System.Collections.Generic;
using Umuna.Core.Domain.Data;

namespace Umuna.Core.Domain.Interfaces
{
    public interface IUser : IHasCreatedAt
    {
        string Email { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        ICollection<CameraPosition> CameraPositions { get; set; }
    }
}