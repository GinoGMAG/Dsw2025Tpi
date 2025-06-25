using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Domain;

public class Customer : EntityBase
{
    public string email { get; set; }
    public string name { get; set; }
    public string phoneNumber { get; set; }
    public List<Order> orders { get; set; } = new List<Order>();
}
