using star_wars.Repositories;
using star_wars.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace star_wars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RebelsController : ControllerBase
    {
        private readonly IRebelRepository _rebelRepository;
        private readonly ILogger<RebelsController> _logger;

        public RebelsController(IRebelRepository rebelRepository)
        {
            _rebelRepository = rebelRepository;
        }

        public RebelsController(ILogger<RebelsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into RebelsController");
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

        [HttpGet("{name}/{planet}")]
        public async Task GetRebelOnPlanet(string name, string planet)
        {
            await _rebelRepository.GetRebelOnPlanet(name, planet);
        }

        [HttpPost]
        public async Task<ActionResult<Rebel>> PostRebels([FromBody] Rebel rebel)
        {
            Rebel addRebel = await _rebelRepository.Create(rebel);
            return CreatedAtAction(nameof(GetRebels), new { name = addRebel.Name }, addRebel);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRebel(int id, [FromBody] Rebel rebel)
        {
            if (id != rebel.Id)
            {
                return BadRequest();
            }

            await _rebelRepository.Update(rebel);

            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Kill(string name)
        {
            var rebelToKill = await _rebelRepository.GetName(name);
            if (rebelToKill == null)
            {
                return NotFound();
            }

            await _rebelRepository.Kill(rebelToKill.Name);
            return StatusCode(200);

        }
    }
}
