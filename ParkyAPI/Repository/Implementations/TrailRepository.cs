using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ParkyAPI.Repository.Implementations
{
    public class TrailRepository : ITrailRepository
    {
        private readonly AppDbContext _db;
        public TrailRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int id)
        {
            return _db.Trails.Include(t => t.NationalPark).FirstOrDefault(n => n.Id == id);
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.Include(t => t.NationalPark).OrderBy(n => n.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            return _db.Trails.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            
        }

        public bool TrailExists(int id)
        {
            return _db.Trails.Any(n => n.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateTrail(Trail trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int id)
        {
            return _db.Trails.Include(t => t.NationalPark).Where(n => n.NationalParkId == id).ToList();
        }
    }
}
