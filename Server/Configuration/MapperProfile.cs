using AutoMapper;
using Server.DTOs.Author;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Configuration
{
    internal class MapperProfile: Profile
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
