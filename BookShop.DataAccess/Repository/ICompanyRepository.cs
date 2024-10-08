using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DataAccess.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);

    }
}
