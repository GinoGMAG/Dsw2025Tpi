using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Exceptions;

public class NotExistException : Exception
{
    public NotExistException(string? message) : base(message)
    { 


    }
}
