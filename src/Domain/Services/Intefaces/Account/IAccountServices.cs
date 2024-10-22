using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Services.Intefaces.Account
{
    public interface IAccountServices
    {
        Task<UserTokenResponseModel> SignInAsync(UserRequestModel requestModel);
    }
}
