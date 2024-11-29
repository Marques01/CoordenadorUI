using Domain.Entities;
using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Services.Intefaces.Account
{
    public interface IAccountServices
    {
        Task<UserTokenResponseModel> SignInAsync(UserRequestModel requestModel);

        Task<UserTokenResponseModel> RefreshAsync(string token);

        Task<UserResponseModel> SignUpAsync(UserRequestModel requestModel);

        Task IncludeUserRoleAsync(UserRoles userRoles);
    }
}
