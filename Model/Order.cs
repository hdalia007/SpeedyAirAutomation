using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpeedyAirAutomation.Model
{

    public static class Order
    {
        public static List<OrderData> TotalOrders(string jsonData)
        {
            return Orders(jsonData).ConvertAll(x => new OrderData() { OrderId = x.Key, Destination = x.Value });
        }

        private static Func<string, List<KeyValuePair<string, string>>> Orders = (jsonData)
            =>
        {
            if (string.IsNullOrEmpty(jsonData))
                throw new ArgumentNullException(nameof(jsonData));

            List<KeyValuePair<string, string>> _tempData = new();

            JObject _Dictionary = JObject.Parse(jsonData);

            foreach (KeyValuePair<string, JToken?> item in _Dictionary)
                _tempData.Add(new KeyValuePair<string, string>
                    (item.Key, item.Value["destination"].ToString()));

            return _tempData;
        };

        public class OrderData : Flight
        {
            public string OrderId { get; set; }

            public string Destination { get; set; }
        }

    }
}
