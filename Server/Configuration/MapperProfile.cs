using AutoMapper;
using Server.DTOs.Author;
using Server.Models;

namespace Server.Configuration
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateAuthorMap();
        }

        public void CreateAuthorMap()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<AuthorCreateRequest, Author>();
            CreateMap<AuthorUpdateRequest, Author>();
        }
    }
}
