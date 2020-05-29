using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CSVToSeed.Models;

namespace CSVToSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to HurriCant");
            Console.WriteLine("Type 'exit' to exit");
            string line;
            while ((line = Console.ReadLine()) != "exit")
            {
                if(line == "-create")
                {

                }
                Console.WriteLine("Enter the path to the seed csv file:");
                line = Console.ReadLine();
                Console.WriteLine("Enter the path to the Links.csv file (default: f:/Networking-Metadata/src/data/Network/Datacenters/WAN/Links.csv):");
                var linkscsv = Console.ReadLine();
                if(linkscsv.Length == 0) {
                linkscsv = "f:/Networking-Metadata/src/data/Network/Datacenters/WAN/Links.csv";
                }
                var links = LoadLinksCSV(linkscsv);
                XmlSerializer ser = new XmlSerializer(typeof(Seed));
                TextWriter writer = new StreamWriter("s33dlet.xml");
                var seed = ReadSeedFileCSV(line, links);
                ser.Serialize(writer, seed);
                writer.Close();
                Console.WriteLine($"Seeds planted:{seed.Wiring.Count()}");
            }
        }

        private static Seed ReadSeedFileCSV(string pathToCSV, Dictionary<string, List<Links>> links)
        {
            if (File.Exists(pathToCSV))
            {
                var seed = new Seed("WAN");
                var rows = File.ReadLines(pathToCSV).Select(line => line.Split(',')).ToList();
                foreach(var row in rows)
                {
                    var a_device = row[0];
                    var a_if = row[1];
                    var a_logical_if = row[2];
                    var a_ipv4 = row[3];
                    var a_ipv6 = row[4];
                    var z_device = row[0+5];
                    var z_if = row[1+5];
                    var cont = false;
                    if (links.ContainsKey(a_device))
                    {
                        foreach (var link in links[a_device])
                        {
                            if (link.Equals(a_if, z_device, z_if))
                            {
                                Console.Error.WriteLine($"{a_device} found in Links.csv with {a_if} connected to {z_device}:{z_if}");
                                cont = true;
                            }
                        }
                    }
                    if (cont)
                    {
                        continue;
                    }
                    var z_logical_if = row[2+5];
                    var z_ipv4 = row[3+5];
                    var z_ipv6 = row[4+5];
                    var wiring = new Wiring(new Start(ConvertDeviceNameToType(a_device), a_device, a_if, ConvertConnectorType(a_if), a_logical_if, a_ipv4, a_ipv6),
                        new End(ConvertDeviceNameToType(z_device), z_device, z_if, ConvertConnectorType(z_if), z_logical_if, z_ipv4, z_ipv6), new DevicePattern(), Models.Action.Add);
                    seed.Wiring.Add(wiring);
                }
                return seed;
            }
            else
            {
                Console.WriteLine($"Failed to load csv file. Try with -create to build an example.\n");
            }
            return null;
        }

        private static Dictionary<string, List<Links>> LoadLinksCSV(string path)
        {
            if (File.Exists(path))
            {
                var rows = File.ReadLines(path).Select(line => { if (!line.Contains("#")) {
                        return line.Split(',');
                    } return null;
                }).ToList();
                var dict = new Dictionary<string, List<Links>>();
                foreach (var row in rows)
                {
                    if (row != null)
                    {
                        var a_device = row[0];
                        var a_if = row[1];
                        var z_device = row[2];
                        var z_if = row[3];
                        var speed = row[4];
                        if (!dict.ContainsKey(a_device))
                        {
                            dict.Add(a_device, new List<Links> { new Links(a_device, a_if, z_device, z_if, speed)});
                        }else
                        {
                            dict[a_device].Add(new Links(a_device, a_if, z_device, z_if, speed));
                        }
                    }
                }
                return dict; 
            }
            return null;
        }

        private static string ConvertDeviceNameToType(string deviceName)
        {
            if(deviceName.Contains("car", StringComparison.InvariantCultureIgnoreCase))
            {
                return "CorporateAggregateRouter";
            }
            if(deviceName.Contains("ier", StringComparison.InvariantCultureIgnoreCase))
            {
                return "InternetEdgeRouter";
            }
            if (deviceName.Contains("icr", StringComparison.InvariantCultureIgnoreCase))
            {
                return "InternetCoreRouter";
            }
            if (deviceName.Contains("oob", StringComparison.InvariantCultureIgnoreCase))
            {
                return "CoreTS";
            }
            throw new ArgumentException($"Unable to convert {deviceName} to device type. It either needs to be added to the dictionary or it is misspelled.");
        }
        private static string ConvertConnectorType(string if_name)
        {
            return "LR4";
        }
    }
}
