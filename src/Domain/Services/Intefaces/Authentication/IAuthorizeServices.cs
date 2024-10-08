namespace Domain.Services.Intefaces.Authentication
{
    public interface IAuthorizeServices
    {
        Task LoginAsync(string token);

        Task Logout();
    }
}
