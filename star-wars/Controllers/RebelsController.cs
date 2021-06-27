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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello, this is the index!");
            return View();
        }

    }
}
