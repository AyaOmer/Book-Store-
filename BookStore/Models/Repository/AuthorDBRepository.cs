namespace BookStore.Models.Repository
{
    public class AuthorDBRepository: IBookStoreRepository<Author>
    {
        BookStoreDBContext DB;
        

        public AuthorDBRepository(BookStoreDBContext _DB)
        {
         DB=_DB;
        }
        public void Add(Author entity)
        {
           
            DB.Authors.Add(entity);
            DB.SaveChanges();

        }

        public void Delete(int id)
        {
            var author = Find(id);
            DB.Authors.Remove(author);
            DB.SaveChanges();

        }

        public Author Find(int id)
        {
            var author = DB.Authors.FirstOrDefault(d => d.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return DB.Authors.ToList();
        }

        public List<Author> Search(string term)
        {
            return DB.Authors.Where(b => b.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author entity)
        {
            DB.Update(entity);
            DB.SaveChanges();
        }
    }

}
