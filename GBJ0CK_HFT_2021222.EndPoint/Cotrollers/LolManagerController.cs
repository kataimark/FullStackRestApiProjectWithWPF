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
    public class LolManagerController : ControllerBase
    {
        ILolManagerLogic ml;
        IHubContext<SignalRHub> hub;

        public LolManagerController(ILolManagerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.ml = logic;
            this.hub = hub;
        }


        [HttpGet]
        public IEnumerable<LolManager> Get()
        {
            return ml.ReadAll();
        }

        [HttpGet("{id}")]
        public LolManager Get(int id)
        {
            return ml.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] LolManager value)
        {
            ml.Create(value);
            this.hub.Clients.All.SendAsync("LolManagerCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] LolManager value)
        {
            ml.Update(value);
            this.hub.Clients.All.SendAsync("LolManagerUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var lolmanagerToDelete = this.ml.Read(id);
            ml.Delete(id);
            this.hub.Clients.All.SendAsync("LolManagerDeleted",lolmanagerToDelete);
            this.hub.Clients.All.SendAsync("LolTeamDeleted", null);
            this.hub.Clients.All.SendAsync("LolPlayerDeleted", null);
        }
    }
}
