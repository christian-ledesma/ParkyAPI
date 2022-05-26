using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ParkyAPI.Repository.Implementations
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly AppDbContext _db;
        public NationalParkRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int id)
        {
            return _db.NationalParks.FirstOrDefault(n => n.Id == id);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(n => n.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            return _db.NationalParks.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(n => n.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
