using System.Collections.Generic;

namespace Airline.Planes
{
    public abstract class Plane
    {
        protected Plane(string model, int maxSpeed, int maxFlightDistance, int maxLoadCapacity)
        {
            Model = model;
            MaxSpeed = maxSpeed;
            MaxFlightDistance = maxFlightDistance;
            MaxLoadCapacity = maxLoadCapacity;
        }

        public string Model { get; }
        public int MaxSpeed { get; }
        public int MaxFlightDistance { get; }
        public int MaxLoadCapacity { get; }

        public override string ToString()
        {
            return "Plane{" +
                   "model='" + Model + '\'' +
                   ", maxSpeed=" + MaxSpeed +
                   ", maxFlightDistance=" + MaxFlightDistance +
                   ", maxLoadCapacity=" + MaxLoadCapacity +
                   '}';
        }

        public override bool Equals(object obj)
        {
            return obj is Plane plane &&
                   Model == plane.Model &&
                   MaxSpeed == plane.MaxSpeed &&
                   MaxFlightDistance == plane.MaxFlightDistance &&
                   MaxLoadCapacity == plane.MaxLoadCapacity;
        }

        public override int GetHashCode()
        {
            var hashCode = -1043886837;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + MaxSpeed.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxFlightDistance.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxLoadCapacity.GetHashCode();

            return hashCode;
        }
    }
}