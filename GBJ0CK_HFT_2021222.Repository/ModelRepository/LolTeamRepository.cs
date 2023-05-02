using Microsoft.EntityFrameworkCore;
using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Repository
{
    public class LolTeamRepository : Repository<LolTeam>
    {
        public LolTeamRepository(DbContext ctx) : base(ctx)
        {
        }
        public override LolTeam Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Update(LolTeam obj)
        {
            var oldLolTeam = Read(obj.Id);
            oldLolTeam.Id = obj.Id;
            oldLolTeam.Name = obj.Name;
            oldLolTeam.Owner = obj.Owner;
            oldLolTeam.LolManager_Id = obj.LolManager_Id;

            ctx.SaveChanges();
        }
        public override void Delete(int id)
        {
            ctx.Remove(Read(id));
            ctx.SaveChanges();
        }
    }
}
