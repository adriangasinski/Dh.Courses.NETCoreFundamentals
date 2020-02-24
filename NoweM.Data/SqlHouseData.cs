using Microsoft.EntityFrameworkCore;
using NoweM.Core;
using System.Collections.Generic;
using System.Linq;

namespace NoweM.Data
{
    public class SqlHouseData : IHouseData
    {
        private readonly NoweMDbContext db;

        public SqlHouseData(NoweMDbContext db)
        {
            this.db = db;
        }
        public House Add(House newHouse)
        {
            db.Add(newHouse);
            return newHouse;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public House Delete(int houseId)
        {
            var house = GetHouseById(houseId);
            if (house != null)
            {
                db.Houses.Remove(house);
            }
            return house;
        }

        public House GetHouseById(int houseId)
        {
            return db.Houses.Find(houseId);
        }

        public IEnumerable<House> GetHousesByAddress(string name)
        {
            if (name != null)
            {
                name = name.ToLower();
            }
            
            var query = from r in db.Houses
                        where r.Address.ToLower().Contains(name) || string.IsNullOrEmpty(name)
                        orderby r.Address
                        select r;

            return query;
        }

        public int GetHousesCount()
        {
            return db.Houses.Count();
        }

        public House Update(House updatedHouse)
        {
            var entity = db.Houses.Attach(updatedHouse);
            entity.State = EntityState.Modified;
            return updatedHouse;
        }
    }
}
