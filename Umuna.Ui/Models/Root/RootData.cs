using Umuna.Core.Contracts.Api.User;

namespace Umuna.Ui.Models.Root
{
    public class RootData
    {
        public UserCreateDto User { get; set; } = new UserCreateDto();
    }
}
