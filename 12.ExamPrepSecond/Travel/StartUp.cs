namespace Travel
{
    using System.IO;
    using System.Linq;
    using Core;
    using Core.Contracts;
    using Core.Controllers;
    using Core.Controllers.Contracts;
    using Core.IO;
    using Core.IO.Contracts;
    using Entities;
    using Entities.Airplanes;
    using Entities.Contracts;
    using Travel.Entities.Factories;
    using Travel.Entities.Factories.Contracts;

    public static class StartUp
    {
        public static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IAirport airport = new Airport();
            IAirplaneFactory airplaneFactory = new AirplaneFactory();
            IItemFactory itemFactory = new ItemFactory();

            IAirportController airportController = new AirportController(airport,airplaneFactory, itemFactory);
            IFlightController flightController = new FlightController(airport);

            IEngine engine = new Engine(reader, writer, airportController, flightController);

            engine.Run();
        }
    }
}