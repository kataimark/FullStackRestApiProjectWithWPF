using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Logic
{
    public interface ILolManagerLogic
    {
        //CRUD
        void Create(LolManager obj);
        LolManager Read(int id);
        IQueryable<LolManager> ReadAll();
        void Update(LolManager obj);
        void Delete(int id);


        IEnumerable<LolManager> GetLolManagerWhereLolPlayer18();
        IEnumerable<LolManager> GetLolManagerWhereLolPlayerModelIsZeus();
        IEnumerable<LolManager> GetLolManagerWherePriceIs100();

    }
}
