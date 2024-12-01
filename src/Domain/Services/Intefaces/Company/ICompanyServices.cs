using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Services.Intefaces.Company
{
    public interface ICompanyServices
    {
        Task<CompanyResponseModel> CreateAsync(CompanyRequestModel companyRequestModel);
    }
}
