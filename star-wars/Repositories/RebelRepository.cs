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

        public async Task<Rebel> GetRebelOnPlanet(string name, string planet)
        {
            Rebel rebel = await _context.Rebels.FindAsync(name);
            if (rebel != null)
            {
                return rebel.Planet == planet ? rebel : null;
            }
            else
            {
                return null;
            }
                

        }

        public async Task Kill(string name)
        {
            var rebelToKill = await _context.Rebels.FindAsync(name);
            _context.Rebels.Remove(rebelToKill);
            await _context.SaveChangesAsync();
            
        }

        public async Task Update(Rebel rebel)
        {
            _context.Entry(rebel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

}
