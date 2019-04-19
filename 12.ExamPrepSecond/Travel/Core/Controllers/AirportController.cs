namespace Travel.Core.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Contracts;
	using Entities;
	using Entities.Contracts;
	using Entities.Factories;
	using Entities.Factories.Contracts;
    using Travel.Entities.Airplanes.Contracts;
    using Travel.Entities.Items.Contracts;

    public class AirportController : IAirportController
	{
        private const int BagValueConfiscationThreshold = 3000;

        private IAirport airport;
        private IAirplaneFactory airplaneFactory;
        private IItemFactory itemFactory;

        public AirportController(IAirport airport, IAirplaneFactory airplaneFactory, IItemFactory itemFactory)
        {
            this.airport = airport;
            this.airplaneFactory = airplaneFactory;
            this.itemFactory = itemFactory;
        }

        public string RegisterPassenger(string username)
        {
            if (this.airport.GetPassenger(username) != null)
            {
                throw new InvalidOperationException($"Passenger {username} already registered!");
            }

            IPassenger passenger = new Passenger(username);

            this.airport.AddPassenger(passenger);

            return $"Registered {passenger.Username}";
        }

        public string RegisterBag(string username, IEnumerable<string> bagItems)
        {
            IPassenger passenger = this.airport.GetPassenger(username);

            IItem[] items = bagItems
                .Select(i => this.itemFactory.CreateItem(i))
                .ToArray();
            IBag bag = new Bag(passenger, items);

            passenger.Bags.Add(bag);

            return $"Registered bag with {string.Join(", ", bagItems)} for {username}";
        }

        public string RegisterTrip(string source, string destination, string planeType)
        {
            IAirplane airplane = this.airplaneFactory.CreateAirplane(planeType);

            ITrip trip = new Trip(source, destination, airplane);

            this.airport.AddTrip(trip);

            return $"Registered trip {trip.Id}";
        }

        public string CheckIn(string username, string tripId, IEnumerable<int> bagIndexes)
        {
            IPassenger passenger = this.airport.GetPassenger(username);
            ITrip trip = this.airport.GetTrip(tripId);

            bool checkedIn = trip.Airplane.Passengers.Any(p => p.Username == username);

            if (checkedIn)
            {
                throw new InvalidOperationException($"{username} is already checked in!");
            }

            int confiscatedBags = CheckInBags(passenger, bagIndexes);

            trip.Airplane.AddPassenger(passenger);

            return
                $"Checked in {passenger.Username} with {bagIndexes.Count() - confiscatedBags}/{bagIndexes.Count()} checked in bags";
        }

        private int CheckInBags(IPassenger passenger, IEnumerable<int> bagsToCheckIn)
        {
            IList<IBag> bags = passenger.Bags;

            var confiscatedBagCount = 0;

            foreach (var i in bagsToCheckIn)
            {
                IBag currentBag = bags[i];
                bags.RemoveAt(i);

                if (ShouldConfiscate(currentBag))
                {
                    airport.AddConfiscatedBag(currentBag);
                    confiscatedBagCount++;
                }
                else
                {
                    this.airport.AddCheckedBag(currentBag);
                }
            }

            return confiscatedBagCount;
        }

        private static bool ShouldConfiscate(IBag bag)
        {
            IItem[] items = bag.Items.ToArray();

            int luggageValue = 0;

            for (int i = 0; i < items.Length; i++)
            {
                luggageValue += items[i].Value;
            }

            var shouldConfiscate = luggageValue > BagValueConfiscationThreshold;

            return shouldConfiscate;
        }
    }
}