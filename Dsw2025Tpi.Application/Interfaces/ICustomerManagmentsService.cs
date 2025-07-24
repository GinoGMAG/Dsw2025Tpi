using Dsw2025Tpi.Domain.Domain;

namespace Dsw2025Tpi.Application.Interfaces
{
    public interface ICustomerManagmentsService
    {
        Task<Customer> CreateCustomerAsync(string email, string name, string phoneNumber);
    }
}