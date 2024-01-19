using BookStore.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Models.Repository
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {

        IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author {Id=1, FullName="Khalid ESSAADANI"},
                new Author {Id=2, FullName="Hamid MAKBOUL"},
                new Author {Id=3, FullName="Said HAMRI"}

            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(d => d.Id)+1;
           authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.FirstOrDefault(d => d.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
           return authors.Where(b => b.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author entity)
        {
           var author= Find(id);
            author.FullName = entity.FullName;
        }
    }
}
