using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext ctx;

        public Repository(DbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T obj)
        {
            ctx.Add(obj);
            ctx.SaveChanges();
        }
        public abstract T Read(int id);
        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }
        public abstract void Update(T obj);
        public abstract void Delete(int id);
    }
}
