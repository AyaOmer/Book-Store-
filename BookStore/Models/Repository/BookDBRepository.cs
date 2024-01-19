using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Models.Repository
{
    public class BookDBRepository : IBookStoreRepository<Book>
    {
        BookStoreDBContext context;
        public BookDBRepository(BookStoreDBContext _context)
        {
            context = _context;
        }
        public void Add(Book entity)
        {
            
            context.Books.Add(entity);
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            var book = Find(id);
            context.Books.Remove(book);
            context.SaveChanges();

        }

        public Book Find(int id)
        {
            var book = context.Books.Include(a => a.Author).FirstOrDefault(d => d.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return context.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book entity)
        {
            context.Books.Update(entity);
            context.SaveChanges();

        }
        public List<Book> Search(string term)
        {
            var books = context.Books.Include(a => a.Author).Where(b => b.Title.Contains(term) || b.Description.Contains(term) || b.Author.FullName.Contains(term)).ToList();
            return books;
        }
    }
}
