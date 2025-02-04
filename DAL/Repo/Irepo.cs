namespace DAL.Repo
{
    public interface Irepo <T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T Entity);
        void Update(T Entity);
        void Delete(int Id);
        void AddRange(IEnumerable<T> entities);
    }
}
