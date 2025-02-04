﻿using Domain.Models;
using Domain.Models.Request;
using Domain.Models.Response;

namespace Domain.Services.Intefaces.Person
{
    public interface IPersonServices
    {
        Task<PersonResponseModel> CreatePersonAsync(PersonRequestModel personRequestModel);

        Task<Paginate<Entities.Person>> GetPersonsAsync(int page, int pageSize);

        Task<Paginate<Entities.Person>> GetPersonsByCompanyIdAsync(int companyId,int page, int pageSize);

        Task<PersonResponseModel> GetByEmailAsync(string email);
    }
}
