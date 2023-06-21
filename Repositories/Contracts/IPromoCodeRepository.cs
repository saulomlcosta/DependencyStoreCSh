using DependencyStore.Models;

namespace DependencyStore.Repositories;

public interface IPromoCodeRepository
{
    Task<PromoCode?> GetPromoCodeAsync(string promoCode);
}