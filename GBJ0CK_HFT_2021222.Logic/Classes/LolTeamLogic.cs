using GBJ0CK_HFT_2021222.Models;
using GBJ0CK_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Logic
{
    public class LolTeamLogic : ILolTeamLogic
    {
        IRepository<LolTeam> LolTeamRepo;

        public LolTeamLogic(IRepository<LolTeam> LolTeamRepo)
        {
            this.LolTeamRepo = LolTeamRepo;
        }

        public void Create(LolTeam obj)
        {
            if (obj.Name.Any(c => char.IsDigit(c)) || obj.Owner.Any(c => char.IsDigit(c)))
            {
                throw new ArgumentException("Name and owner can't contain numbers");
            }
            if (obj.Name == "" || obj.Owner == "")
            {
                throw new ArgumentNullException("Can't be null");
            }
            LolTeamRepo.Create(obj);
        }

        public void Delete(int id)
        {
            LolTeamRepo.Delete(id);
        }

        public LolTeam Read(int id)
        {
            if (id < LolTeamRepo.ReadAll().Count() + 1)
                return LolTeamRepo.Read(id);
            else
                throw new IndexOutOfRangeException("Id is too big!");
            return LolTeamRepo.Read(id);
        }

        public IQueryable<LolTeam> ReadAll()
        {
            return LolTeamRepo.ReadAll();
        }

        public void Update(LolTeam obj)
        {
            LolTeamRepo.Update(obj);
        }
    }
}
