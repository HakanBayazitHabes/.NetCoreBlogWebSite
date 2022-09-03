using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.Repositories;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfWriterRepository : GenericRepository<Writer>, IWriterDal
    {
        public int GetIdWriterByMail(string mail)
        {
            using
                Context context = new Context();
            return context.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault();
        }
    }
}
