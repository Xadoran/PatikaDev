using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                 if(context.Books.Any()){
                    return;
                 }
                 
                 context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },

                    new Genre{
                        Name = "Science Fiction"
                    },

                    new Genre{
                        Name = "Romance"
                    }
                 );

                
                context.Books.AddRange(
                    new Book{
                        // Id = 1,
                        Title = "Lean",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)
                    }, 
                    new Book{
                        // Id = 3,
                        Title = "Lord of the Rings",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2011,05,13)
                    },
                        new Book{
                        // Id = 2,
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 540,
                        PublishDate = new DateTime(2001,12,21)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}