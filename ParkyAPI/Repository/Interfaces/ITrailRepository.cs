using ParkyAPI.Models;
using System.Collections.Generic;

namespace ParkyAPI.Repository.Interfaces
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationalPark(int id);
        Trail GetTrail(int id);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
