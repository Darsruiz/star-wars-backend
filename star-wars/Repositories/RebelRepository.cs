using Microsoft.EntityFrameworkCore;
using star_wars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace star_wars.Repositories
{
    public class RebelRepository : IRebelRepository
    {
        private readonly RebelContext _context;

        public RebelRepository(RebelContext context)
        {
            _context = context;
        }

        public async Task<Rebel> Create(Rebel rebel)
        {
            _context.Rebels.Add(rebel);
            await _context.SaveChangesAsync();

            return rebel;
        }

        public async Task<IEnumerable<Rebel>> Get()
        {
            return await _context.Rebels.ToListAsync();
        }

        public async Task<Rebel> GetName(string name)
        {
            return await _context.Rebels.FindAsync(name);
        }

        public async Task<Rebel> GetPlanet(string planet)
        {
            return await _context.Rebels.FindAsync(planet);
        }

        public async Task Kill(string name)
        {
            var killRebel = await _context.Rebels.FindAsync(name);
            _context.Rebels.Remove(killRebel);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Rebel rebel)
        {
            _context.Entry(rebel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}
