using AutoMapper;
using Server.DTOs.Author;
using Server.DTOs.Book;
using Server.DTOs.Exemplar;
using Server.DTOs.Genre;
using Server.DTOs.Publisher;
using Server.Models;
using System.Security.Policy;

namespace Server.Configuration
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateAuthorMap();
            CreateBookMap();
            CreateExemplarMap();
            CreateGenreMap();
            CreatePublisherMap();
        }

        public void CreateAuthorMap()
        {
            CreateMap<Author, GetAuthorResponse>();
            CreateMap<CreateAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>();
        }

        public void CreateBookMap()
        {
            CreateMap<Book, GetBookResponse>()
                .ForMember(
                response => response.AuthorName,
                conf => conf.MapFrom(model => model.Author.Name))
                .ForMember(
                response => response.GenreName,
                conf => conf.MapFrom(model => model.Genre.Name))
                .ForMember(
                response => response.PublisherName,
                conf => conf.MapFrom(model => model.Publisher.Name));
            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Author>();
        }

        public void CreateExemplarMap()
        {
            CreateMap<Exemplar, GetExemplarResponse>()
                .ForMember(
                response => response.BookName,
                conf => conf.MapFrom(model => model.Book.Name));
            CreateMap<CreateExemplarRequest, Exemplar>();
            CreateMap<UpdateExemplarRequest, Exemplar>();
        }

        public void CreateGenreMap()
        {
            CreateMap<Genre, GetGenreResponse>();
            CreateMap<CreateGenreRequest, Genre>();
            CreateMap<UpdateGenreRequest, Genre>();
        }

        public void CreatePublisherMap()
        {
            CreateMap<Models.Publisher, GetPublisherResponse>();
            CreateMap<CreatePublisherRequest, Models.Publisher>();
            CreateMap<UpdatePublisherRequest, Models.Publisher>();
        }
    }
}
