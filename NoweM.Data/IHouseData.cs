using System.Collections.Generic;
using System.Text;
using NoweM.Core;

namespace NoweM.Data
{
    public interface IHouseData
    {
        IEnumerable<House> GetHousesByAddress(string name);
        House GetHouseById(int houseId);
        House Update(House updatedHouse);
        House Add(House newHouse);
        House Delete(int houseId);
        int Commit();
        int GetHousesCount();

    }
}
