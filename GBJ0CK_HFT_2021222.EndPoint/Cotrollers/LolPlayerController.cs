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
    public class LolPlayerController : ControllerBase
    {
        ILolPlayerLogic pl;
        IHubContext<SignalRHub> hub;

        public LolPlayerController(ILolPlayerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.pl = logic;
            this.hub = hub;
        }


        // GET: LolPlayer
        [HttpGet]
        public IEnumerable<LolPlayer> Get()
        {
            return pl.ReadAll();
        }

        // GET LolPlayer/5
        [HttpGet("{id}")]
        public LolPlayer Get(int id)
        {
            return pl.Read(id);
        }

        // POST LolPlayer
        [HttpPost]
        public void Post([FromBody] LolPlayer value)
        {
            pl.Create(value);
            this.hub.Clients.All.SendAsync("LolPlayerCreated", value);
        }

        // PUT LolPlayer/5
        [HttpPut]
        public void Put([FromBody] LolPlayer value)
        {
            pl.Update(value);
            this.hub.Clients.All.SendAsync("LolPlayerUpdated", value);
        }

        // DELETE LolPlayer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var lolplayerToDelete = this.pl.Read(id);
            pl.Delete(id);
            this.hub.Clients.All.SendAsync("LolPlayerDeleted", lolplayerToDelete);
        }
    }
}
