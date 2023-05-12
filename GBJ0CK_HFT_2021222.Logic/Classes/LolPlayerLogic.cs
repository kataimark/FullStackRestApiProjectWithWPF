using GBJ0CK_HFT_2021222.Models;
using GBJ0CK_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Logic
{
    public class LolPlayerLogic : ILolPlayerLogic
    {
        IRepository<LolManager> LolManagerRepo;
        IRepository<LolTeam> LolTeamRepo;
        IRepository<LolPlayer> LolPlayerRepo;

        public LolPlayerLogic(IRepository<LolManager> LolManagerRepo, IRepository<LolTeam> LolTeamRepo, IRepository<LolPlayer> LolPlayerRepo)
        {
            this.LolManagerRepo = LolManagerRepo;
            this.LolTeamRepo = LolTeamRepo;
            this.LolPlayerRepo = LolPlayerRepo;
        }

        public void Create(LolPlayer obj)
        {
            if (obj.Name == "")
            {
                throw new ArgumentNullException("Model can't be null");
            }
            if (obj.Price < 0 || obj.Age < 0)
            {
                throw new ArgumentException("Negative price and horsepower is not allowed");
            }
            LolPlayerRepo.Create(obj);
        }

        public void Delete(int id)
        {
            LolPlayerRepo.Delete(id);
        }

        public LolPlayer Read(int id)
        {
            if (id < LolPlayerRepo.ReadAll().Count() + 1)
                return LolPlayerRepo.Read(id);
            else
                throw new IndexOutOfRangeException("Id is to big!");
            return LolPlayerRepo.Read(id);
        }

        public IQueryable<LolPlayer> ReadAll()
        {
            return LolPlayerRepo.ReadAll();
        }

        public void Update(LolPlayer obj)
        {
            LolPlayerRepo.Update(obj);
        }


        public IEnumerable<LolPlayer> GetLolPlayerWhereMoreThan28Employees()
        {
            var q = from LolPlayers in LolPlayerRepo.ReadAll()
                    join LolTeams in LolTeamRepo.ReadAll()
                    on LolPlayers.LolTeam_Id equals LolTeams.Id
                    join LolManagers in LolManagerRepo.ReadAll()
                    on LolTeams.LolManager_Id equals LolManagers.Id
                    where LolManagers.Employees > 28
                    select LolPlayers;
            return q;
        }
        public IEnumerable<LolPlayer> GetLolPlayerWhereLolTeamOwnerIsBengi()
        {
            var q = from LolPlayers in LolPlayerRepo.ReadAll()
                    join LolTeams in LolTeamRepo.ReadAll()
                    on LolPlayers.LolTeam_Id equals LolTeams.Id
                    where LolTeams.Owner == "Bengi"
                    select LolPlayers;
            return q;
        }
    }
}
