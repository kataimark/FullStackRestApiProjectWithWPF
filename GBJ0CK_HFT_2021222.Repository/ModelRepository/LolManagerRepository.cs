using Microsoft.EntityFrameworkCore;
using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Repository
{
    public class LolManagerRepository : Repository<LolManager>
    {
        public LolManagerRepository(DbContext ctx) : base(ctx)
        {
        }
        public override LolManager Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Update(LolManager obj)
        {
            var oldLolManager = Read(obj.Id);
            oldLolManager.Id = obj.Id;
            oldLolManager.Name = obj.Name;
            oldLolManager.Employees = obj.Employees;

            ctx.SaveChanges();
        }
        public override void Delete(int id)
        {
            ctx.Remove(Read(id));
            ctx.SaveChanges();
        }
    }
}
