using Backend.Models;

namespace Backend.Dtos;

public class ProviderDto
{
    public string? Name { get; set; }
    public decimal SummedBalance { get; set; }
}