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

namespace GBJ0CK_HFT_2021222.Endpoint.Cotrollers
{
    [Route("[controller]")]
    [ApiController]
    public class LolTeamController : ControllerBase
    {
        ILolTeamLogic tl;
        IHubContext<SignalRHub> hub;

        public LolTeamController(ILolTeamLogic tl, IHubContext<SignalRHub> hub)
        {
            this.tl = tl;
            this.hub = hub;
        }


        [HttpGet]
        public IEnumerable<LolTeam> Get()
        {
            return tl.ReadAll();
        }

        [HttpGet("{id}")]
        public LolTeam Get(int id)
        {
            return tl.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] LolTeam value)
        {
            tl.Create(value);
            this.hub.Clients.All.SendAsync("LolTeamCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] LolTeam value)
        {
            tl.Update(value);
            this.hub.Clients.All.SendAsync("LolTeamUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var lolteamToDelete = this.tl.Read(id);
            tl.Delete(id);
            this.hub.Clients.All.SendAsync("LolTeamDeleted", lolteamToDelete);
            this.hub.Clients.All.SendAsync("LolPlayerDeleted", null);
        }
    }
}
