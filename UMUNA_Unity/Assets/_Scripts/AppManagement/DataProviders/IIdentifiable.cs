
using System;

namespace UMUNA.AppManagement.DataProviders
{
    public interface IIdentifiable
    {
        Guid Guid { get; }
    }
}
