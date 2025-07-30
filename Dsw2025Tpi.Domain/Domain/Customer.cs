using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Domain;

public class Customer : EntityBase
{
    public Customer(string email, string name, string phoneNumber)
    {
        this.Email = email;
        this.Name = name;
        this.PhoneNumber = phoneNumber;
    }

    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}
