// REMOVE any "using" statements, which start with "Travel." BEFORE SUBMITTING
namespace Travel.Tests
{
    using NUnit.Framework;
    using Travel.Core.Controllers;
    using Travel.Core.Controllers.Contracts;
    using Travel.Entities;
    using Travel.Entities.Airplanes;
    using Travel.Entities.Airplanes.Contracts;
    using Travel.Entities.Contracts;
    using Travel.Entities.Items;
    using Travel.Entities.Items.Contracts;

    [TestFixture]
    public class FlightControllerTests
    {
        private IAirport airport;
        private IAirplane airplane;
        private ITrip trip;

        [SetUp]
        public void SetUp()
        {
            this.airport = new Airport();
            this.airplane = new LightAirplane();
            this.trip = new Trip("Tuk", "Tam", airplane);
        }

        [Test]
        public void TestTripIsCompleted()
        {
            IPassenger passenger = new Passenger("Gosho");
            IBag bag = new Bag(passenger, new Item[] { new Colombian() });

            passenger.Bags.Add(bag);
            airplane.AddPassenger(passenger);
            airport.AddTrip(trip);
            trip.Complete();

            IFlightController flightController = new FlightController(airport);

            string actualResult = flightController.TakeOff();

            string expectedResult = "Confiscated bags: 0 (0 items) => $0";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestIfAirplaneIsOverbooked()
        {
            for (int i = 0; i < 10; i++)
            {
                IPassenger passenger = new Passenger("Gosho" + i);
                airplane.AddPassenger(passenger);
            }

            airport.AddTrip(trip);

            IFlightController flightController = new FlightController(airport);

            string actualResult = flightController.TakeOff();

            string expectedResult = "TukTam1:\r\nOverbooked! Ejected Gosho1, Gosho0, Gosho3, Gosho7, Gosho8\r\nConfiscated 0 bags ($0)\r\nSuccessfully transported 5 passengers from Tuk to Tam.\r\nConfiscated bags: 0 (0 items) => $0";

            Assert.AreEqual(expectedResult, actualResult);

            //TestIfTripIsCompleted
            Assert.AreEqual(trip.IsCompleted, true);
        }

        [Test]
        public void TestLoadCarryOnBaggage()
        {
            Passenger[] passengers = new Passenger[10];

            for (int i = 0; i < passengers.Length; i++)
            {
                passengers[i] = new Passenger("Gosho" + i);

                airplane.AddPassenger(passengers[i]);
            }

            for (int i = 0; i < 5; i++)
            {
                IBag bag;

                if (i % 2 == 0)
                {
                    bag = new Bag(passengers[i],
                        new IItem[] { new Colombian() });
                }
                else
                {
                    bag = new Bag(passengers[i],
                    new IItem[] { new Toothbrush() });
                }

                passengers[i].Bags.Add(bag);
            }

            airport.AddTrip(trip);

            IFlightController flightController = new FlightController(airport);

            string actualResult = flightController.TakeOff();

            string expectedResult = "TukTam2:\r\nOverbooked! Ejected Gosho1, Gosho0, Gosho3, Gosho7, Gosho8\r\nConfiscated 3 bags ($50006)\r\nSuccessfully transported 5 passengers from Tuk to Tam.\r\nConfiscated bags: 3 (3 items) => $50006";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
