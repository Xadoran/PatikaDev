using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.Application.BookOperations.Queries.GetBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using AutoMapper;
using FluentValidation;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            // Book book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            // return book;
            GetBookById query = new GetBookById(_context, _mapper);
            query.tempId = id;
            // try
            // {
                GetBookByIdValidator validator = new GetBookByIdValidator();
                validator.ValidateAndThrow(query);
                return Ok(query.Handle());
            // }
            // catch (Exception ex)
            // {   
            //     return BadRequest(ex.Message);
            // }
            
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
          //  Book book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            //return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            // try
            // {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);

                // if(!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         Console.WriteLine("Property : " + item.PropertyName + " --- Error Message: " + item.ErrorMessage);
                //     }
                // }else
                    command.Handle();
                
            // }
            // catch (Exception ex)
            // {     
            //     return BadRequest(ex.Message);
            // }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Book dBook = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            // var bo = _context.Books.Remove(dBook);
            // if(bo is null){
            //     return BadRequest();
            // }
            // _context.SaveChanges();
            // return Ok();

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.tempId = id;
            // try
            // {
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();    
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            return Ok("Kitap başarıyla silindi.");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel newerBook)
        {
            // Book dBook = _context.Books.Where(x => x.Title == newerBook.Title).SingleOrDefault();
            UpdateBookCommand command =  new UpdateBookCommand(_context);
            command.tempId = id;
            command.Model = newerBook;
            // try
            // {
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();    
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            

            // _context.SaveChanges();
            return Ok();
        }
    }
}