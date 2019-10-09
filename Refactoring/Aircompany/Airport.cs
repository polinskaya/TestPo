using System.Collections.Generic;
using System.Linq;
using Airline.Enums;
using Airline.Planes;

namespace Airline
{
    public class Airport
    {
        public Airport(IEnumerable<Plane> planes)
        {
            Planes = planes.ToList();
        }

        public List<Plane> Planes { get; }

        public List<PassengerPlane> GetPassengersPlanes()
        {
            return Planes.Where(plane => plane.GetType() == typeof(PassengerPlane)).Cast<PassengerPlane>().ToList();
        }

        public List<MilitaryPlane> GetMilitaryPlanes()
        {
            return Planes.Where(plane => plane.GetType() == typeof(MilitaryPlane)).Cast<MilitaryPlane>().ToList();
        }

        public PassengerPlane GetPassengerPlaneWithMaxPassengersCapacity()
        {
            return GetPassengersPlanes().Aggregate((w, x) => w.PassengersCapacity > x.PassengersCapacity ? w : x);
        }

        public List<MilitaryPlane> GetTransportMilitaryPlanes()
        {
            return GetMilitaryPlanes().Where(militaryPlane => militaryPlane.Type.Equals(MilitaryType.Transport)).ToList();
        }

        public List<Plane> GetSortedByMaxDistance()
        {
            return Planes.OrderBy(w => w.MaxFlightDistance).ToList();
        }

        public List<Plane> GetSortedByMaxSpeed()
        {
            return Planes.OrderBy(w => w.MaxSpeed).ToList();
        }

        public List<Plane> GetSortedByMaxLoadCapacity()
        {
            return Planes.OrderBy(w => w.MaxLoadCapacity).ToList();
        }

        public override string ToString()
        {
            return "Airport{" + "planes=" + string.Join(", ", Planes.Select(x => x.Model)) + '}';
        }
    }
}