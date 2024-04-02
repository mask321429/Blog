using AutoMapper;
using Blog.Data.Models;
using Blog2023.Data.DTO;
using Blog2023.Data.Models;

namespace DeliveryBackend.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
 
        CreateMap<UserRegisterDTO, User>();
        CreateMap<User, UserDTO>();
        CreateMap<TagModel, TagDTO>();
        CreateMap<AuthorModel, AuthorDTO>();
        CreateMap<CommunityModel, CommunityDTO>();
        CreateMap<MyCommunityModel, MyCommunityDTO>();
        CreateMap<AdministratorModel, AdministratorDTO>();
        CreateMap<PostModel, PostDTO>();
    }
}