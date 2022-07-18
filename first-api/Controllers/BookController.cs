using first_api.Data;
using first_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.OData;

namespace first_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext context;
        public BookController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllAsync()
        {
            return Ok(await context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetByIdAsync([FromRoute] int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
                return BadRequest("Book not found");
           return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBookAsync(Book book)
        {
            try
            {
                context.Books.Add(book);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return StatusCode(500, ex.InnerException?.Message);
            }
            return Ok(await context.Books.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBookAsync([FromBody] Book book, [FromRoute] int id)
        {
            var oldBook = await context.Books.FindAsync(id);
            if (oldBook == null)
                return BadRequest("Book not found");
            try
            {
                oldBook.Title = book.Title;
                oldBook.Author = book.Author;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return StatusCode(500, ex.InnerException?.Message);
            }
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBookAsync([FromRoute] int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
                return BadRequest("Book not found");
            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Book>> UpdateOneAsync([FromRoute] int id, [FromBody] JsonPatchDocument book)
        {
            var oldBook = await context.Books.FindAsync(id);
            if (oldBook == null)
                return BadRequest("Book not found");
            book.ApplyTo(oldBook);
            await context.SaveChangesAsync();
            return Ok(book);
        }

    }
}
