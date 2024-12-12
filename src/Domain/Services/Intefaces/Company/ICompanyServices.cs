using Domain.Models;
using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Services.Intefaces.Company
{
    public interface ICompanyServices
    {
        Task<CompanyResponseModel> CreateAsync(CompanyRequestModel companyRequestModel);

        Task<Paginate<Entities.Company>> GetCompaniesAsync(int userId, int page, int limit);

        Task<UserCompanyResponseModel> AssociateUserCompanyAsync(UserCompanyRequestModel userCompanyRequestModel);
    }
}
