using Blog.Data.Models;
using Blog2023.Data.DTO;

namespace Blog.Services.Interfaces
{
    public interface IAddressService
    {
        Task<List<SearchAdressDTO>> SearchAdress(int? parentObjectId, string? query);


        Task<List<SearchAdressDTO>> ChainAddress(Guid id);
    }
}
