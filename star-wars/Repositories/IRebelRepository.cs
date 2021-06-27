using star_wars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace star_wars.Repositories
{
    public interface IRebelRepository
    {
        Task<IEnumerable<Rebel>> Get();

        Task<Rebel> GetName(string name);

        Task<Rebel> GetRebelOnPlanet(string name, string planet);
        Task<Rebel> Create(Rebel rebel);
        Task Update(Rebel rebel);
        Task Kill(string name);
    }
}
