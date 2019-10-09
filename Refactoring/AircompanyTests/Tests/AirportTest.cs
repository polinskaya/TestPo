using System.Collections.Generic;
using System.Linq;
using Airline;
using Airline.Enums;
using Airline.Planes;
using NUnit.Framework;

namespace AirlinesTests.Tests
{
    [TestFixture]
    public class AirportTest
    {
        private readonly List<Plane> _planes = new List<Plane>
        {
            new PassengerPlane("Boeing-737", 900, 12000, 60500, 164),
            new PassengerPlane("Boeing-737-800", 940, 12300, 63870, 192),
            new PassengerPlane("Boeing-747", 980, 16100, 70500, 242),
            new PassengerPlane("Airbus A320", 930, 11800, 65500, 188),
            new PassengerPlane("Airbus A330", 990, 14800, 80500, 222),
            new PassengerPlane("Embraer 190", 870, 8100, 30800, 64),
            new PassengerPlane("Sukhoi Superjet 100", 870, 11500, 50500, 140),
            new PassengerPlane("Bombardier CS300", 920, 11000, 60700, 196),
            new MilitaryPlane("B-1B Lancer", 1050, 21000, 80000, MilitaryType.Bomber),
            new MilitaryPlane("B-2 Spirit", 1030, 22000, 70000, MilitaryType.Bomber),
            new MilitaryPlane("B-52 Stratofortress", 1000, 20000, 80000, MilitaryType.Bomber),
            new MilitaryPlane("F-15", 1500, 12000, 10000, MilitaryType.Fighter),
            new MilitaryPlane("F-22", 1550, 13000, 11000, MilitaryType.Fighter),
            new MilitaryPlane("C-130 Hercules", 650, 5000, 110000, MilitaryType.Transport)
        };

        [Test]
        public void GetMilitaryPlanesWithTransportTypeTest()
        {
            var airport = new Airport(_planes);
            var transportMilitaryPlanes = airport.GetTransportMilitaryPlanes();

            foreach (var militaryPlane in transportMilitaryPlanes)
            {
                Assert.True(militaryPlane.Type.Equals(MilitaryType.Transport));
            }
        }

        [Test]
        public void GetPassengerPlaneWithMaxPassengersCapacityTest()
        {
            var airport = new Airport(_planes);
            var planeWithMaxPassengersCapacity = airport.GetPassengerPlaneWithMaxPassengersCapacity();
            PassengerPlane expectedPlaneWithMaxPassengersCapacity = new PassengerPlane("Boeing-747", 980, 16100, 70500, 242);

            Assert.True(expectedPlaneWithMaxPassengersCapacity.Equals(planeWithMaxPassengersCapacity));
        }

        [Test]
        public void GetSortedByMaxLoadCapacityTest()
        {
            var airport = new Airport(_planes);
            var sorted = airport.GetSortedByMaxLoadCapacity();

            for (var i = 0; i < sorted.Count - 1; i++)
            {
                var currentPlane = sorted[i];
                var nextPlane = sorted[i + 1];
                Assert.True(currentPlane.MaxLoadCapacity <= nextPlane.MaxLoadCapacity);
            }
        }

        [Test]
        public void GetSortedByMaxDistanceTest()
        {
            var airport = new Airport(_planes);
            var sorted = airport.GetSortedByMaxDistance();

            for (var i = 0; i < sorted.Count - 1; i++)
            {
                var currentPlane = sorted[i];
                var nextPlane = sorted[i + 1];
                Assert.True(currentPlane.MaxFlightDistance <= nextPlane.MaxFlightDistance);
            }
        }

        [Test]
        public void GetSortedByMaxSpeedTest()
        {
            var airport = new Airport(_planes);
            var sorted = airport.GetSortedByMaxSpeed();

            for (var i = 0; i < sorted.Count - 1; i++)
            {
                var currentPlane = sorted[i];
                var nextPlane = sorted[i + 1];
                Assert.True(currentPlane.MaxSpeed <= nextPlane.MaxSpeed);
            }
        }

        [Test]
        public void GetPassengerPlanesTest()
        {
            var airport = new Airport(_planes);
            var sorted = airport.GetPassengersPlanes();

            Assert.True(sorted.All(plane => plane is PassengerPlane));
        }

        [Test]
        public void GetMilitaryPlanesTest()
        {
            var airport = new Airport(_planes);
            var sorted = airport.GetMilitaryPlanes();

            Assert.True(sorted.All(plane => plane is MilitaryPlane));
        }
    }
}