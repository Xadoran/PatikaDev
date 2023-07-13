using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>(); 
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
        }
    }
}