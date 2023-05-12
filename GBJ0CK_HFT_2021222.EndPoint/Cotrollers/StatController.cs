using GBJ0CK_HFT_2021222.Endpoint.Services;
using GBJ0CK_HFT_2021222.Logic;
using GBJ0CK_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GBJ0CK_HFT_2021222.Endpoint
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {

        ILolPlayerLogic cl;
        ILolManagerLogic dl;
        //IHubContext<SignalRHub> hub;

        public StatController(ILolPlayerLogic cl, ILolManagerLogic dl/*, IHubContext<SignalRHub> hub*/)
        {
            this.cl = cl;
            this.dl = dl;
            //this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<LolPlayer> GetLolPlayerWhereMoreThan28Employees()
        {
            //this.hub.Clients.All.SendAsync("GetLolPlayerWhereMoreThan28Employees");
            return cl.GetLolPlayerWhereMoreThan28Employees();
        }


        [HttpGet]
        public IEnumerable<LolPlayer> GetLolPlayerWhereLolTeamOwnerIsBengi()
        {
            return cl.GetLolPlayerWhereLolTeamOwnerIsBengi();
            //this.hub.Clients.All.SendAsync("GetLolPlayerWhereLolTeamOwnerIsBengi");
        }

        [HttpGet]
        public IEnumerable<LolManager> GetLolManagerWhereLolPlayer18()
        {
            return dl.GetLolManagerWhereLolPlayer18();
            //this.hub.Clients.All.SendAsync("GetLolManagerWhereLolPlayer18");
        }

        [HttpGet]
        public IEnumerable<LolManager> GetLolManagerWhereLolPlayerModelIsZeus()
        {
            return dl.GetLolManagerWhereLolPlayerModelIsZeus();
            //this.hub.Clients.All.SendAsync("GetLolManagerWhereLolPlayerModelIsZeus");
        }

        [HttpGet]
        public IEnumerable<LolManager> GetLolManagerWherePriceIs100()
        {
            return dl.GetLolManagerWherePriceIs100();
            //this.hub.Clients.All.SendAsync("GetLolManagerWherePriceIs100");
        }
    }
}
