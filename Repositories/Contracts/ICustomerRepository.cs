using DependencyStore.Models;

namespace DependencyStore.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(string customerId);
}