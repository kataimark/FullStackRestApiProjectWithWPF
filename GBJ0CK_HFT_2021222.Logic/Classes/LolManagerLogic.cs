using GBJ0CK_HFT_2021222.Models;
using GBJ0CK_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Logic
{
    public class LolManagerLogic : ILolManagerLogic
    {
        IRepository<LolManager> LolManagerRepo;
        IRepository<LolTeam> LolTeamRepo;
        IRepository<LolPlayer> LolPlayerRepo;

        public LolManagerLogic(IRepository<LolManager> LolManagerRepo, IRepository<LolTeam> LolTeamRepo, IRepository<LolPlayer> LolPlayerRepo)
        {
            this.LolManagerRepo = LolManagerRepo;
            this.LolTeamRepo = LolTeamRepo;
            this.LolPlayerRepo = LolPlayerRepo;
        }

        public void Create(LolManager obj)
        {
            if (obj.Name.Any(c => char.IsDigit(c)))
            {
                throw new ArgumentException("Name can't contain numbers");
            }
            if (obj.Name == "")
            {
                throw new ArgumentNullException("Name can't be null");
            }
            LolManagerRepo.Create(obj);
        }

        public void Delete(int id)
        {
            LolManagerRepo.Delete(id);
        }

        public LolManager Read(int id)
        {
            if (id < LolManagerRepo.ReadAll().Count() + 1)
                return LolManagerRepo.Read(id);
            else
                throw new IndexOutOfRangeException("Id is too big!");
            return LolManagerRepo.Read(id);
        }

        public IQueryable<LolManager> ReadAll()
        {
            return LolManagerRepo.ReadAll();
        }

        public void Update(LolManager obj)
        {
            LolManagerRepo.Update(obj);
        }
        public IEnumerable<LolManager> GetLolManagerWhereLolPlayer18()
        {
            var q = from LolPlayers in LolPlayerRepo.ReadAll()
                    join LolTeams in LolTeamRepo.ReadAll()
                    on LolPlayers.LolTeam_Id equals LolTeams.Id
                    join LolManagers in LolManagerRepo.ReadAll()
                    on LolTeams.LolManager_Id equals LolManagers.Id
                    where LolPlayers.Age == 18
                    select LolManagers;
            return q;
        }

        public IEnumerable<LolManager> GetLolManagerWhereLolPlayerModelIsZeus()
        {
            var q = from LolPlayers in LolPlayerRepo.ReadAll()
                    join LolTeams in LolTeamRepo.ReadAll()
                    on LolPlayers.LolTeam_Id equals LolTeams.Id
                    join LolManagers in LolManagerRepo.ReadAll()
                    on LolTeams.LolManager_Id equals LolManagers.Id
                    where LolPlayers.Name == "Zeus"
                    select LolManagers;
            return q;
        }

        public IEnumerable<LolManager> GetLolManagerWherePriceIs100()
        {
            var q = from LolPlayers in LolPlayerRepo.ReadAll()
                    join LolTeams in LolTeamRepo.ReadAll()
                    on LolPlayers.LolTeam_Id equals LolTeams.Id
                    join LolManagers in LolManagerRepo.ReadAll()
                    on LolTeams.LolManager_Id equals LolManagers.Id
                    where LolPlayers.Price == 100
                    select LolManagers;
            return q;
        }

    }
}
