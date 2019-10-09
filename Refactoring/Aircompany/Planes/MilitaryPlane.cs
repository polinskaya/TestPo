using Airline.Enums;

namespace Airline.Planes
{
    public class MilitaryPlane : Plane
    {
        public MilitaryPlane(string model, int maxSpeed, int maxFlightDistance, int maxLoadCapacity, MilitaryType type)
            : base(model, maxSpeed, maxFlightDistance, maxLoadCapacity)
        {
            Type = type;
        }

        public MilitaryType Type { get; }

        public override bool Equals(object obj)
        {
            return obj is MilitaryPlane plane && base.Equals(obj) && Type == plane.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 1701194404;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();

            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString().Replace("}", ", type=" + Type + '}');
        }
    }
}