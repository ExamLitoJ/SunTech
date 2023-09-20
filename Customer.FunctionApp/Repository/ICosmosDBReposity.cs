using SunTech.Customer.FuncApp.Requests;
using SunTech.Customer.FuncApp.Response;
using System.Threading.Tasks;

namespace SunTech.Customer.FuncApp.Repository;

internal interface ICosmosDBReposity
{
    Task<CommandResponse> CustomerInsertRecordAsync(CustomerInsertRequest insertRequest);

    Task<CommandResponse> UpdateRecordAsync(CustomerUpdateRequest updateRequest);
}
