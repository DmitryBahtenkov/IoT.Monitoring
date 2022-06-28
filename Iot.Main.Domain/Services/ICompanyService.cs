#nullable enable

using Iot.Main.Domain.Models.CompanyModel;
using Iot.Main.Domain.Models.CompanyModel.DTO;

namespace Iot.Main.Domain.Services;

public interface ICompanyService
{
    public Task<CompanyData> Create(CreateCompanyRequest request);
    public Task<CompanyData> Update(int id, UpdateCompanyRequest request);
    public Task<List<CompanyData>> GetAll(CompanyFilter filter);
    public Task<CompanyData?> Get(CompanyFilter filter);
}
