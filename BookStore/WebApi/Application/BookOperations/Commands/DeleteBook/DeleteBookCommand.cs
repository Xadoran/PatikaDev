using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int tempId;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == tempId);
            if(book is null)
            {
                throw new InvalidOperationException("Aradığınız kitap bulunamadı.");
            }
            if ((_dbContext.Books.Remove(book)) is null)
            {
                throw new InvalidOperationException("İşlem sırasında hata oluştu.");
            }
            
            _dbContext.SaveChanges();
        }

    }
}