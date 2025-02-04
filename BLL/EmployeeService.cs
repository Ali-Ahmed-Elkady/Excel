using DAL;
using DAL.Repo;

namespace Excel.BLL
{
    public class EmployeeService
    {
        
        private readonly Irepo<Employee> repo ;
        public EmployeeService()
        {
            var Context = new ApplicationDbContext();
            repo = new Repo<Employee>(Context);
        }

        public void UploadEmployees(string path)
        {
            path.Upload(repo); 
        }
        public Employee GetById(int id)
        {
            var result = repo.Get(id);
            return result;
        }
    }
}
