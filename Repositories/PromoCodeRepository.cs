using Dapper;
using DependencyStore.Models;
using DependencyStore.Repositories;
using Microsoft.Data.SqlClient;

public class PromoCodeRepository : IPromoCodeRepository
{
    private readonly SqlConnection _connection;

    public PromoCodeRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<PromoCode?> GetPromoCodeAsync(string promoCode)
    {
        var query = "SELECT * FROM PROMO_CODES WHERE CODE=@code";
        return await _connection.QueryFirstOrDefaultAsync<PromoCode>(query);
    }
}
