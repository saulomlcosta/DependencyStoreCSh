using Dapper;
using DependencyStore.Models;
using DependencyStore.Repositories;
using DependencyStore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DependencyStore.Controllers;

public class OrderController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeService _deliveryFeeService;
    private readonly IPromoCodeRepository _promoCodeRepository;

    public OrderController(ICustomerRepository customerRepository, IDeliveryFeeService deliveryFeeService, IPromoCodeRepository promoCodeRepository)
    {
        _customerRepository = customerRepository;
        _deliveryFeeService = deliveryFeeService;
        _promoCodeRepository = promoCodeRepository;
    }

    [Route("v1/orders")]
    [HttpPost]
    public async Task<IActionResult> Place(string customerId, string zipCode, string promoCode, int[] products)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer is null)
            return NotFound();

        var deliveryFee = await _deliveryFeeService.GetDeliveryFeeAsync(zipCode);
        var coupon = await _promoCodeRepository.GetPromoCodeAsync(promoCode);
        var discount = coupon?.Value ?? 0M;

        decimal subTotal = 0;
        const string getProductQuery = "SELECT [Id], [Name], [Price] FROM PRODUCT WHERE ID=@id";
        for (var p = 0; p < products.Length; p++)
        {
            Product product;
            await using (var conn = new SqlConnection("CONN_STRING"))
                product = await conn.QueryFirstAsync<Product>(getProductQuery, new { Id = p });

            subTotal += product.Price;
        }

        var order = new Order(deliveryFee, discount, new List<Product>());
        return Ok($"Pedido {order.Code} gerado com sucesso!");
    }
}