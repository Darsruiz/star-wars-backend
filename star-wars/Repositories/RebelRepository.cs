using Microsoft.EntityFrameworkCore;
using star_wars.CustomExceptions;
using star_wars.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace star_wars.Repositories
{
    public class RebelRepository : IRebelRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly RebelContext _context;

        public RebelRepository(RebelContext context)
        {
            try
            {
                _context = context;
            } 
            catch
            {
                logger.Warn("Exception caught: Couldn't create _context");
            }
            
        }

        public Task WriteToFile(Rebel rebel)
        {
            string rebelInfoFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string[] rebelInfo = { $"{ rebel.Name } at { rebel.Planet } on { rebel.Datetime }" };
            try
            {
                File.AppendAllLines(Path.Combine(rebelInfoFilePath, "Rebels.txt"), rebelInfo);
                return Task.CompletedTask;
            }
            catch
            {
                File.WriteAllText(Path.Combine(rebelInfoFilePath, "Rebels.txt"), rebelInfo[0]);
                return Task.CompletedTask;
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
                logger.Warn("Exception caught: Couldn't save changes");
                return null;
            }
            
        }

        public async Task<IEnumerable<Rebel>> Get()
        {
            try
            {
                return await _context.Rebels.ToListAsync();
            }
            catch (ObjectNotFoundException)
            {
                logger.Warn("Exception caught: Couldn't Get()");
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
                logger.Warn("Exception caught: Couldn't find name " + name);
                return null;
            }
            
        }

        public async Task GetRebelOnPlanet(string name, string planet)
        {
            
            try
            {
                Rebel rebel = await _context.Rebels.FindAsync(name);
                if (rebel != null && planet != null)
                {
                    await WriteToFile(rebel);
                }
            }
            catch (CustomException)
            {
                logger.Warn("Exception caught: GetRebelOnPlanet() Couldn't find rebel with name " + name);
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
                        await _context.SaveChangesAsync();
                        return $"Rebel {rebelToKill} has been Removed";
                    }
                    catch
                    {
                        logger.Warn("Exception caught: Kill() Couldn't SaveChangesAsync");
                        return "Exception caught: Kill() Couldn't SaveChangesAsync";
                    }
                }
                else
                {
                    logger.Info("Couldn't find " + name);
                    return "Couldn't find " + name;
                }
                
                
            }
            catch
            {
                logger.Warn("Exception caught: Kill() Couldn't find rebel with name " + name);
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
            catch (DbUpdateConcurrencyException)
            {
                logger.Error("Exception caught: Update() DbUpdateConcurrencyException");
                throw new NotImplementedException();
            }
        }
    }

}
