using Discount.API.Entites;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController:ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productName}",Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupen), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupen>> GetDiscount(string productName)
        {
            var coupen = await _repository.GetDiscount(productName);
            return Ok(coupen);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupen), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupen>> CreateDiscount([FromBody] Coupen coupen)
        {
            await _repository.CreateDiscount(coupen);
            return CreatedAtRoute("GetDiscount", new { productName = coupen.ProductName },coupen);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupen), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupen>> UpdateDiscount([FromBody] Coupen coupen)
        {
            return Ok( await _repository.UpdateDiscount(coupen));        
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(Coupen), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupen>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName));
        }

    }
}
