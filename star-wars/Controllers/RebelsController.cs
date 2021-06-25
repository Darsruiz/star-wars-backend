using star_wars.Repositories;
using star_wars.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace star_wars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RebelsController : ControllerBase
    {
        private readonly IRebelRepository _rebelRepository;
        
        public RebelsController(IRebelRepository rebelRepository)
        {
            _rebelRepository = rebelRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Rebel>> GetRebels()
        {
            return await _rebelRepository.Get();
        }

        [HttpGet("{name}")]

        public async Task<ActionResult<Rebel>> GetRebels(string name)
        {
            return await _rebelRepository.GetName(name);
        }

        [HttpPost]
        public async Task<ActionResult<Rebel>>PostRebels([FromBody] Rebel rebel)
        {
            Rebel killRebel = await _rebelRepository.Create(rebel);
            return CreatedAtAction(nameof(GetRebels), new { name = killRebel.Name }, killRebel);
        }

    }
}
