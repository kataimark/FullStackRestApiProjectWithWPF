using Microsoft.EntityFrameworkCore;
using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Repository
{
    public class LolPlayerRepository : Repository<LolPlayer>
    {
        public LolPlayerRepository(DbContext ctx) : base(ctx)
        {
        }
        public override LolPlayer Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Update(LolPlayer obj)
        {
            var oldLolPlayer = Read(obj.Id);
            oldLolPlayer.Id = obj.Id;
            oldLolPlayer.Name = obj.Name;
            oldLolPlayer.Age = obj.Age;
            oldLolPlayer.Price = obj.Price;
            oldLolPlayer.LolTeam_Id = obj.LolTeam_Id;

            ctx.SaveChanges();
        }
        public override void Delete(int id)
        {
            ctx.Remove(Read(id));
            ctx.SaveChanges();
        }
    }
}
