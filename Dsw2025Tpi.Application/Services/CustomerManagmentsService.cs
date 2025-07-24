using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Domain;
using Dsw2025Tpi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Services;

public class CustomerManagmentsService : ICustomerManagmentsService
{
    private IRepository _repository;

    public CustomerManagmentsService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Customer> CreateCustomerAsync(string email, string name, string phoneNumber)
    {
        var customer = new Customer(email, name, phoneNumber);
        await _repository.Add(customer);
        return customer;
    }

}
