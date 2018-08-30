using ClinicStatus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicStatus.Services
{
    public interface IFloorData
    {
        IEnumerable<Floor> GetAll();
        Floor Get(int ID);
        Floor Add(Floor newFloor);

    }

    public class InMemoryFloorData : IFloorData
    {
       List<Floor> _Floors;

        public InMemoryFloorData()
        {
            _Floors = new List<Floor>
            {
                new Floor{ID = 1, Name = "الدور الأول"},
                new Floor{ID = 0, Name = "الدور الأرضي"}
            };
        }


        public Floor Add(Floor newFloor)
        {
            newFloor.ID = _Floors.Max(f => f.ID) + 1;
            _Floors.Add(newFloor);

            return newFloor;
        }

        public Floor Get(int ID)
        {
            return _Floors.FirstOrDefault(f => f.ID == ID);
        }

        public IEnumerable<Floor> GetAll()
        {
            return _Floors;
        }
    }
}
