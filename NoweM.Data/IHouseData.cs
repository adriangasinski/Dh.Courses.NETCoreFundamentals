using System;
using System.Collections.Generic;
using System.Text;
using NoweM.Core;
using System.Linq;

namespace NoweM.Data
{
    public interface IHouseData
    {
        IEnumerable<House> GetHousesByAddress(string name);
        House GetHouseById(int houseId);
        House Update(House updatedHouse);
        House Add(House newHouse);
        int Commit();

    }

    public class InMemoryHouseData : IHouseData
    {
        List<House> houses;
        public InMemoryHouseData()
        {
            houses = new List<House>()
            {
                new House {Id = 1, DevId = 1, Address = "Traktowa", HType=HouseType.Segment, NumberOfHouses = 30 },
                new House {Id = 2, DevId = 2, Address = "Zielone Ogrody", HType=HouseType.Segment, NumberOfHouses = 20 },
                new House {Id = 3, DevId = 3, Address = "Zielona Sadyba", HType=HouseType.Segment, NumberOfHouses = 10 },
                new House {Id = 4, DevId = 4, Address = "Złote Korony", HType=HouseType.Bliźniak, NumberOfHouses = 5 },
                new House {Id = 5, DevId = 1, Address = "Graniczna", HType=HouseType.Jednorodzinny, NumberOfHouses = 7 }

            };

        }

        public int Commit()
        {
            return 0;
        }

        public House Add(House newHouse)
        {
            houses.Add(newHouse);
            newHouse.Id = houses.Max(h => h.Id) + 1;
            return newHouse;
        }

        public House GetHouseById(int houseId)
        {
            return houses.SingleOrDefault(r => r.Id == houseId);
        }

        public IEnumerable<House> GetHousesByAddress(string name = null)
        {
            return from h in houses
                   where string.IsNullOrEmpty(name) || h.Address.ToLower().Contains(name.ToLower())
                   orderby h.Address
                   select h;
        }

        public House Update(House updatedHouse)
        {
            var house = houses.SingleOrDefault(h => h.Id == updatedHouse.Id);
            if (house != null)
            {
                house.Address = updatedHouse.Address;
                house.NumberOfHouses = updatedHouse.NumberOfHouses;
                house.HType = updatedHouse.HType;
            }
            return house;
        }
    }

}
