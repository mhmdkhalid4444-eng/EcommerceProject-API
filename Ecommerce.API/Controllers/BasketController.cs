using Ecommerce.Application.Contracts;
using Ecommerce.Application.DTOs.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ApiBaseController
    {
        private readonly IBasketservice _basketService;

        public BasketController(IBasketservice basketService)
        {
            _basketService = basketService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id , CancellationToken cancellationToken = default)
        {
            var basket = await _basketService.GetBasketAsync(id, cancellationToken);
            return ToActionResult(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken cancellationToken = default)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basket, cancellationToken: cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken cancellationToken = default)
        {
            var result = await _basketService.DeleteBasketAsync(id, cancellationToken);
            return ToActionResult(result);
        }
    }
}
