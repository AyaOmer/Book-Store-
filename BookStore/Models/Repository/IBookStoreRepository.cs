namespace BookStore.Models.Repository
{
    public interface IBookStoreRepository <T>
    {
        IList<T> List();
        T Find(int id);
        void Add(T entity);
        void Update(int id,T entity);
        void Delete(int id);
        List<T> Search(string term);


    }
}
