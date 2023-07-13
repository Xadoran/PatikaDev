using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBook
{
    public class GetBookById
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int tempId;

        public GetBookById(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext; 
            _mapper = mapper;
        }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == tempId);
            if (book is null)
            {
                throw new InvalidOperationException("Aradağınız kitap bulunamadı.");
            }
            
            BookViewModel vw = _mapper.Map<BookViewModel>(book); //new BookViewModel();
            
            // vw.Title = book.Title;
            // vw.PageCount = book.PageCount;
            // vw.Genre = ((GenreEnum)book.GenreId).ToString();
            // vw.PublishDate = book.PublishDate.ToString();

            return vw;
        }

    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
    }
}