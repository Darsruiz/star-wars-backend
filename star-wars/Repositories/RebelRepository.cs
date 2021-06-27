using Microsoft.EntityFrameworkCore;
using star_wars.CustomExceptions;
using star_wars.Models;
using System.Diagnostics;
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
            try
            {
                _context = context;
            } 
            catch (CustomException)
            {
                Console.WriteLine("Exception caught: Couldn't create _context");
            }
            
        }

        public async Task<Rebel> Create(Rebel rebel)
        {
            _context.Rebels.Add(rebel);
            try
            {
                await _context.SaveChangesAsync();

                return rebel;
            }
            catch (CustomException)
            {
                Console.WriteLine("Exception caught: Couldn't save changes");
                return null;
            }
            
        }

        public async Task<IEnumerable<Rebel>> Get()
        {
            try 
            { 
                return await _context.Rebels.ToListAsync();
            }
            catch (CustomException)
            {
                Console.WriteLine("Exception caught: Couldn't Get()");
                return null;
            }
            
        }

        public async Task<Rebel> GetName(string name)
        {
            try
            {
                return await _context.Rebels.FindAsync(name);
            }
            catch (CustomException)
            {
                Console.WriteLine("Exception caught: Couldn't find name " + name);
                return null;
            }
            
        }

        public async Task<Rebel> GetRebelOnPlanet(string name, string planet)
        {
            try
            {
                Rebel rebel = await _context.Rebels.FindAsync(name);
                if (rebel != null && planet != null)
                {
                    return rebel.Planet == planet ? rebel : null;
                }
                else
                {
                    return null;
                }
            }
            catch (CustomException)
            {
                Console.WriteLine("Exception caught: GetRebelOnPlanet() Couldn't find rebel with name " + name);
                return null;
            }
        }

        public async Task<string> Kill(string name)
        {
            try
            {
                var rebelToKill = await _context.Rebels.FindAsync(name);
                if (rebelToKill != null)
                {
                    try
                    {
                        _context.Rebels.Remove(rebelToKill);
                        try
                        {
                            await _context.SaveChangesAsync();
                            return rebelToKill + "has been killed";
                        }
                        catch (CustomException)
                        {
                            Console.WriteLine("Exception caught: Kill() Couldn't SaveChangesAsync");
                            return "Exception caught: Kill() Couldn't SaveChangesAsync";
                        }
                    }
                    catch (CustomException)
                    {
                        Console.WriteLine("Exception caught: Kill() Couldn't remove " + rebelToKill);
                        return "Exception caught: Kill() Couldn't remove " + rebelToKill;
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find " + name);
                    return "Couldn't find " + name;
                }
                
                
            }
            catch (CustomException)
            {
                Console.WriteLine("Exception caught: Kill() Couldn't find rebel with name " + name);
                return "Exception caught: Kill() Couldn't find rebel with name " + name;
            }


        }

        public async Task Update(Rebel rebel)
        {
            _context.Entry(rebel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Exception caught: Update() DbUpdateConcurrencyException");
                foreach (var entry in ex.Entries)
                {
                    Debug.WriteLine("this entry" + entry);
                }
            }
        }
    }

}
