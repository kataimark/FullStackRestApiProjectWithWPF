using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Logic
{
    public interface ILolPlayerLogic
    {
        //CRUD
        void Create(LolPlayer obj);
        LolPlayer Read(int id);
        IQueryable<LolPlayer> ReadAll();
        void Update(LolPlayer obj);
        void Delete(int id);


        IEnumerable<LolPlayer> GetLolPlayerWhereMoreThan28Employees();

        IEnumerable<LolPlayer> GetLolPlayerWhereLolTeamOwnerIsBengi();

    }
}
