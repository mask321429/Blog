using Blog.Services;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog2023.Controllers
{
    [ApiController]
    [Route("api/address")]

    public class AddressController : Controller
    {
        private readonly IAddressService _AddressService;
        public AddressController(IAddressService authorService)
        {
            _AddressService = authorService;
        }
        [HttpGet]
        [Route("searchAddress")]
        public async Task<List<SearchAdressDTO>> SearchAdress(int? parentObjectId, string? query)
        {
            var values = await _AddressService.SearchAdress(parentObjectId, query);

            return values;

        }
        [HttpGet("chain/{id}")]
        public async Task<IActionResult> GetAddressChain(Guid id)
        {
            
            
                var addressChain = await _AddressService.ChainAddress(id);

                if (addressChain == null)
                {
                    return NotFound();
                }

                return Ok(addressChain);
            
         
        }

    }
}
