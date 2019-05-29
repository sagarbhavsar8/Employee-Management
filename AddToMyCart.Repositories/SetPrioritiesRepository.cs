using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.DomainModels;

namespace AddToMyCart.Repositories
{
    public interface ISetPrioritiesRepository
    {
        List<SetPriority> GetAllPriorities();
    }

    public class SetPrioritiesRepository : ISetPrioritiesRepository
    {
        AddToMyWebCartDbContext db;

        public SetPrioritiesRepository()
        {
            db = new AddToMyWebCartDbContext();
        }

        public List<SetPriority> GetAllPriorities()
        {
            return db.SetPriorities.ToList();
        }
    }
}
