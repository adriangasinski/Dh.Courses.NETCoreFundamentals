using System;
using System.Collections.Generic;
using System.Text;
using NoweM.Core;
using System.Linq;

namespace NoweM.Data
{
    public interface IHouseData
    {
        IEnumerable<House> GetAll();

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
        public IEnumerable<House> GetAll()
        {
            return from h in houses
                   orderby h.Address
                   select h;
        }
    }

}
