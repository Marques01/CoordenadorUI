namespace Domain.Services.Intefaces.Authentication
{
    public interface IAuthorizeServices
    {
        Task LoginAsync(string token);

        Task LoginAsync(string token, string refreshToken);

        Task Logout();
    }
}
