using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Logic
{
    public interface ILolTeamLogic
    {
        //CRUD
        void Create(LolTeam obj);
        LolTeam Read(int id);
        IQueryable<LolTeam> ReadAll();
        void Update(LolTeam obj);
        void Delete(int id);
    }
}
