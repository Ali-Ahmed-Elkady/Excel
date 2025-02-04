using Microsoft.EntityFrameworkCore;

namespace DAL.Repo
{
    public class Repo<T> : Irepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repo(ApplicationDbContext Context)
        {
            _context = Context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T Entity)
        {
            _dbSet.Add(Entity) ;
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = _dbSet.Find(Id);
            if (entity != null) 
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T Get(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                throw new ApplicationException("An error occurred while retrieving the entity.", ex);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
            _context.SaveChanges();
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
        }
    }
}
