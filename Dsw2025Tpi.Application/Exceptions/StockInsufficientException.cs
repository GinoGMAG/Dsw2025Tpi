
namespace Dsw2025Tpi.Application.Exceptions;

public class StockInsufficientException : Exception
{
    public StockInsufficientException(string? message) : base(message)
    {
    }
}
