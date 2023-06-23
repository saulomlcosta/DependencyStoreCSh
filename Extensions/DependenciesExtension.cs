using DependencyStore.Repositories;
using DependencyStore.Services;
using DependencyStore.Services.Contracts;
using Microsoft.Data.SqlClient;

namespace DependencyStore.Extensions;
public static class DependenciesExtension
{
  public static void RegisterSqlConnection(this IServiceCollection services, string connectionString)
  {
    services.AddScoped<SqlConnection>(x => new SqlConnection(connectionString));
  }

  public static void RegisterConfiguration(this IServiceCollection services)
  {
    services.AddSingleton<Configuration>();
  }

  public static void RegisterServices(this IServiceCollection services)
  {
    services.AddScoped<IDeliveryFeeService, DeliveryFeeService>();
  }

  public static void RegisterRepositories(this IServiceCollection services)
  {
    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
  }
}
