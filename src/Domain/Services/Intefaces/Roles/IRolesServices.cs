namespace Domain.Services.Intefaces.Roles
{
    public interface IRolesServices
    {
        Task<IEnumerable<Entities.Roles>> GetRolesAsync();
    }
}
