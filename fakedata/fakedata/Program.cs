using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace fakedata
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "[IoTHub_URI]"; 
        static string deviceKey = "[Device Key]"; //obtained from CarDeviceIdentity console application

        static string tripid;
        static string id;
        static Dictionary<string,double> coord = new Dictionary<string, double>();
        static double lat;
        static double longi;
        static double fr; //MAFFlowRate
        static double el; //EngineLoad 
        static double stfb; //ShortTermFuelBank1
        static double ltfb; //LongTermFuelBank1
        static double rpm; //EngineRPM 
        static double spd; //Speed
        static double tp; //ThrottlePosition
        static double rt; //Runtime
        static double dis; //DistanceWithMalfunctionLight
        static double rtp; //RelativeThrottlePosition
        static double ot; //OutsideTemperature
        static double efr; //EngineFuelRate
        static string VIN;
        static int sequence;

        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("[DeviceId]", deviceKey));

            var temprand = new Random();
            id = "wk" + temprand.Next(1000);
            tripid = "cd" + temprand.Next(100);
            VIN = "1HSHKGUR1MH310395"; //http://randomvin.com/ valid vin generator
            Random seqrand = new Random();
            int seqnumb = seqrand.Next(0, 300);
            //start point: you can set up the coordinates of the starting point of your trip
            lat = 37.505233;
            coord.Add("lat", lat);
            longi = 15.082164;
            coord.Add("long", longi);

            for (int i = 0; i < seqnumb; i++)
            {
                getLocation(longi, lat, 500);
                lat = coord["lat"];
                longi = coord["long"];
                sequence = i + 1;
                CreateDataToSend();
                SendDataToCloud();
                Task.Delay(1000).Wait();
            }

            Console.Read();


        }

        private static async void CreateDataToSend()
        {
            var rand = new Random();

            fr = rand.Next(0, 50); //MAFFlowRate
            el = rand.Next(0, 100); //EngineLoad 
            stfb = rand.Next(0, 200) - 100; //ShortTermFuelBank1
            ltfb = rand.Next(0, 200) - 100; //LongTermFuelBank1
            rpm = rand.Next(0, 2005); //EngineRPM 
            spd = rand.Next(5, 70); //Speed
            tp = rand.Next(0, 100); //ThrottlePosition
            rt = rt + 2; //Runtime
            dis = dis + 0.1; //DistanceWithMalfunctionLight
            rtp = rand.Next(0, 100); //RelativeThrottlePosition
            ot = rand.Next(0, 38); //OutsideTemperature
            efr = rand.NextDouble() * 6 + 8; //EngineFuelRate
        }

        //Calculate points of your trip
        public static Dictionary<string, double> getLocation(double x0, double y0, int radius)
        {
            Random random = new Random();

            // Convert radius from meters to degrees
            double radiusInDegrees = radius / 111000f;

            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(y0);

            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;

            //coord.TryGetValue("lat", out foundLatitude);
            //coord.TryGetValue("long", out foundLongitude);
            coord["lat"] = foundLatitude;
            coord["long"] = foundLongitude;

            return coord;
        }

        private static async void SendDataToCloud()
        {
            var TripPoint = new
            {
                TripPointId = id,
                Lat = lat,
                Lon = longi,
                Speed = spd,
                RecordedTimeStamp = DateTime.Now,
                Sequence = sequence,
                EngineRPM = rpm,
                ShortTermFuelBank1 = stfb,
                LongTermFuelBank1 = ltfb,
                ThrottlePosition = tp,
                RelativeThrottlePosition = rtp,
                Runtime = rt,
                DistanceWithMIL = dis,
                EngineLoad = el,
                MAFFlowRate = fr,
                OutsideTemperature = ot,
                EngineFuelRate = efr,
                VIN = VIN
            };

            var TripData = new
            {
                TripId = tripid,
                UserId = "user2",
                Name = "trip2",
                TripDataPoint = TripPoint
            };

            var messageString = JsonConvert.SerializeObject(TripData);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            await deviceClient.SendEventAsync(message);
            Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
            
            
        }
        
    }
}
