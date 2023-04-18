using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpeedyAirAutomation.Model;
using System.Reflection;
using static SpeedyAirAutomation.Model.Order;

namespace SpeedyAirAutomation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Path = $"JsonData\\coding-assigment-orders.json";

            if (!File.Exists(Path))
                throw new FileNotFoundException(Path);

            string fileData = File.ReadAllText(Path);

            List<Flight> Staticflights = new()
            {
                new Flight(){Id=1,Day=1,Arrival="YUL",Departure="YYZ"},
                new Flight(){Id=2,Day=1,Arrival="YUL",Departure="YYC"},
                new Flight(){Id=3,Day=1,Arrival="YUL",Departure="YVR"},
                new Flight(){Id=4,Day=2,Arrival="YUL",Departure="YYZ"},
                new Flight(){Id=5,Day=2,Arrival="YUL",Departure="YYC"},
                new Flight(){Id=6,Day=2,Arrival="YUL",Departure="YVR"},
            };

            #region Output 1

            Staticflights.ForEach(x =>
            Console.WriteLine($"Flight:{x.Id}, departure: {x.Departure}, arrival: {x.Arrival}, day: {x.Day}"));

            #endregion

            Console.WriteLine();

            #region OutPut 2

            List<OrderData> Orders = TotalOrders(fileData);

            List<OrderData> _includedFlights = new();
            List<OrderData> _excludedFlights = new();

            foreach (OrderData order in Orders)
            {
                if (Staticflights.Any(x => x.Departure == order.Destination))
                {
                    Staticflights.Where(x => x.Departure == order.Destination)
                                   .ToList().ForEach(
                                       x => _includedFlights.Add(new OrderData()
                                       {
                                           Id = x.Id,
                                           Arrival = x.Arrival,
                                           Day = x.Day,
                                           Departure = x.Departure,
                                           Destination = order.Destination,
                                           OrderId = order.OrderId
                                       }));
                }
                else
                {
                    _excludedFlights.Add(new OrderData()
                    {
                        OrderId = order.OrderId
                    });
                }
            }

            _includedFlights.OrderBy(x => x.OrderId).ToList()
                .ForEach(x => Console.WriteLine($"order: {x.OrderId}, flightNumber: {x.Id}, departure: {x.Departure}, arrival: {x.Arrival}, day: {x.Day}"));

            Console.WriteLine();

            _excludedFlights.OrderBy(x => x.OrderId).ToList()
                .ForEach(x => Console.WriteLine($"order: {x.OrderId}, flightNumber: not scheduled"));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            #endregion
        }
    }
}