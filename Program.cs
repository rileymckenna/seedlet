// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
//   All rights reserved.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CSVToSeed
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using CSVToSeed.Models;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The networking metadata path.
        /// </summary>
        private static string networkingMetadataPath;

        /// <summary>
        /// The datacenter path.
        /// </summary>
        private static string datacenterPath;

        /// <summary>
        /// The links file path.
        /// </summary>
        private static string linksFilePath;

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to HurriCant");
            Console.WriteLine("Type 'exit' to exit or press the 'enter' key to continue.");
            Console.WriteLine("What is the path to your networking-metadata folder? (default:WAN)");
            while (!CheckNetworkingMetadataPath(networkingMetadataPath = Console.ReadLine()));

            string line = string.Empty;
            while (line != "exit")
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    var missingDirectory = true;
                    while (missingDirectory)
                    {
                        Console.WriteLine("What data center folder will you be working in? (default:WAN)");
                        line = Console.ReadLine();
                        datacenterPath = Path.Combine(networkingMetadataPath, "Datacenters", line);

                        linksFilePath = $"{datacenterPath}\\Links.csv";
                        if (File.Exists(linksFilePath))
                        {
                            missingDirectory = false;
                        }
                        else
                        {
                            Console.WriteLine($"Missing directory: {datacenterPath}. Try again.");
                        }
                    }
                }

                var seed = new Seed(line);
                XmlSerializer ser = new XmlSerializer(typeof(Seed));
                TextWriter writer = null;
                Console.WriteLine("Lets create a seed file. Along the process, you will be able to add additional members or a csv file for large moves.");
                Console.WriteLine("Is this a 'decom' or 'add' wiring?");
                var cont = true;
                var operation = string.Empty;
                while (cont)
                {
                    operation = Console.ReadLine();
                    if (operation != null && (operation.Equals("add", StringComparison.InvariantCultureIgnoreCase)
                                              || operation.Equals(
                                                  "decom",
                                                  StringComparison.InvariantCultureIgnoreCase)))
                    {
                        cont = false;
                    }
                    else
                    {
                        Console.WriteLine("Check your spelling, is this a 'decom' or 'add' wiring?");
                    }
                }

                if (operation.Equals("decom", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Ok, lets add a decom");
                    Console.WriteLine("What is the name of the device you would like to decom?");
                    var deviceName = Console.ReadLine();
                    try
                    {
                        var prefix = deviceName?.Split('-')[0];
                        var decommName = $"{prefix}_decomm";
                        writer = new StreamWriter($"{decommName}_seed_{DateTime.UtcNow.ToFileTimeUtc()}.xml");
                        seed.Datacenters.Add(new Datacenter(decommName, "Decomm", true, deviceName));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    string response = string.Empty;
                    Console.WriteLine("Are you adding standard 'wiring' or 'console'?");
                    var loop = true;
                    while (loop)
                    {
                        response = Console.ReadLine();
                        if (response.Equals("wiring", StringComparison.InvariantCultureIgnoreCase) || response.Equals(
                                "console",
                                StringComparison.InvariantCultureIgnoreCase))
                        {
                            loop = false;
                        }
                        else
                        {
                            Console.WriteLine("Could not parse response please try again.");
                        }
                    }

                    if (response.Equals("console", StringComparison.InvariantCultureIgnoreCase))
                    {
                        response = string.Empty;
                        Console.WriteLine("From 'manual' entry or from 'file'?");
                        loop = true;
                        while (loop)
                        {
                            response = Console.ReadLine();
                            if (response.Equals("manual", StringComparison.InvariantCultureIgnoreCase) || response.Equals(
                                    "file",
                                    StringComparison.InvariantCultureIgnoreCase))
                            {
                                loop = false;
                            }
                            else
                            {
                                Console.WriteLine("Could not parse response please try again.");
                            }
                        }

                        if (response.Equals("manual", StringComparison.InvariantCultureIgnoreCase))
                        {
                            bool another = true;
                            while (another)
                            {
                                Console.WriteLine("What is the starting device name?");
                                var startDeviceName = Console.ReadLine();
                                Console.WriteLine("What is the ending device name?");
                                var endDeviceName = Console.ReadLine();
                                Console.WriteLine("What is the ending device port?");
                                var endPort = Console.ReadLine();
                                seed.Wiring.Add(
                                    new Wiring(
                                        new Start(startDeviceName, "console"),
                                        new End(endDeviceName, endPort),
                                        new DevicePattern(WiringType.Serial),
                                        Models.Action.Add));
                                Console.WriteLine("Would you like to add another? y/n");
                                another = Console.ReadLine()?.StartsWith('y') ?? false;
                            }
                        }
                        else
                        {
                            seed = ProcessSeedFile(seed, false);
                        }
                    }
                    else
                    {
                        seed = ProcessSeedFile(seed, true);
                    }

                    try
                    {
                        var start = seed.Wiring.First().Start.DeviceRegex;
                        var end = seed.Wiring.First().End.DeviceRegex;
                        var fileName =
                            $"{start.Substring(1, start.Length - 2)}_{end.Substring(1, end.Length - 2)}_{DateTime.UtcNow.ToFileTimeUtc()}.xml";
                        
                        // save file in seed ultra
                        var path = Path.Combine(networkingMetadataPath, "buildouts", "SeedUltra", fileName);
                        if (AddLineToNetGraphDeclaration(fileName))
                        {
                            writer = new StreamWriter(path);
                            SaveFile(writer, ser, seed);
                        }
                        else
                        {
                            Console.WriteLine("Unable to add declaration to Netgraphdeclaration.");
                        }

                        // save file in seed processed
                        path = Path.Combine(Path.GetDirectoryName(linksFilePath), "Seeds-Processed", fileName);
                        writer = new StreamWriter(path);
                        SaveFile(writer, ser, seed);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                line = string.Empty;
                datacenterPath = string.Empty;
            }
        }

        private static void SaveFile<T>(TextWriter writer, XmlSerializer serializer, T type)
        {
            try
            {
                if (writer != null)
                {
                    serializer.Serialize(writer, type);
                    writer.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool AddLineToNetGraphDeclaration(string fileName)
        {
            var file = Path.Combine(Path.GetDirectoryName(linksFilePath), "NetgraphDeclaration.xml");
            if (File.Exists(file))
            {
                try
                {
                    var dcName = fileName.Split('-')[0].Split('.')[1];
                    var row = new Row
                                  {
                                      DCName = dcName,
                                      Tag = Path.GetFileNameWithoutExtension(fileName),
                                      Operation = "SeedUltra"
                                  };
                    Console.WriteLine($"Copy and add this line to: {file}\n <Row DCName=\"{dcName}\" Tag=\"{row.Tag}\" Operation=\"SeedUltra\" />");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The check networking metadata path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckNetworkingMetadataPath(string path)
        {
            var temp = Path.GetFullPath($"{path}\\src\\data\\Network\\CopyNetworkMetadata.proj");
            if (File.Exists(temp))
            {
                networkingMetadataPath = Path.GetDirectoryName(temp);
                return true;
            }

            return false;
        }

        /// <summary>
        /// The process seed file.
        /// </summary>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <param name="isStandard">
        /// The is Standard.
        /// </param>
        /// <returns>
        /// The <see cref="Seed"/>.
        /// </returns>
        private static Seed ProcessSeedFile(Seed seed, bool isStandard)
        {
            Console.WriteLine("Enter the path to the seed csv file:");
            var line = Console.ReadLine();
            if (line == null)
            {
                Console.WriteLine("No path specified for seed csv.");
            }

            try
            {

                var links = LoadLinksCSV(linksFilePath);
                if (isStandard)
                {
                    seed = ReadSeedFileCSV(seed, line, links);
                }
                else
                {
                    seed = ReadConsoleSeedFileCSV(seed, line, links);
                }

                return seed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// The read seed file csv.
        /// </summary>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <param name="pathToCSV">
        /// The path to csv.
        /// </param>
        /// <param name="links">
        /// The links.
        /// </param>
        /// <returns>
        /// The <see cref="Seed"/>.
        /// </returns>
        private static Seed ReadSeedFileCSV(Seed seed, string pathToCSV, Dictionary<string, List<Links>> links)
        {
            if (File.Exists(pathToCSV))
            {
                var rows = File.ReadLines(pathToCSV).Select(line => line.Split(',')).ToList();
                foreach (var row in rows)
                {
                    var a_device = row[0];
                    var a_if = row[1];
                    var a_logical_if = row[2];
                    var a_ipv4 = row[3];
                    var a_ipv6 = row[4];
                    var z_device = row[0 + 5];
                    var z_if = row[1 + 5];
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
                    var z_logical_if = row[2 + 5];
                    var z_ipv4 = row[3 + 5];
                    var z_ipv6 = row[4 + 5];
                    var wiring = new Wiring(
                        new Start(
                            ConvertDeviceNameToType(a_device),
                            a_device,
                            a_if,
                            ConvertConnectorType(a_if),
                            a_logical_if,
                            a_ipv4,
                            a_ipv6),
                        new End(
                            ConvertDeviceNameToType(z_device),
                            z_device,
                            z_if,
                            ConvertConnectorType(z_if),
                            z_logical_if,
                            z_ipv4,
                            z_ipv6),
                        new DevicePattern(),
                        Models.Action.Add);
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

        /// <summary>
        /// The read console seed file csv.
        /// </summary>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <param name="pathToCSV">
        /// The path to csv.
        /// </param>
        /// <param name="links">
        /// The links.
        /// </param>
        /// <returns>
        /// The <see cref="Seed"/>.
        /// </returns>
        private static Seed ReadConsoleSeedFileCSV(Seed seed, string pathToCSV, Dictionary<string, List<Links>> links)
        {
            if (File.Exists(pathToCSV))
            {
                var rows = File.ReadLines(pathToCSV).Select(line => line.Split(',')).ToList();
                foreach (var row in rows)
                {
                    var aDevice = row[0];
                    var aIf = row[1];
                    var zDevice = row[0 + 2];
                    var zIf = row[1 + 2];
                    var cont = false;
                    if (links.ContainsKey(aDevice))
                    {
                        foreach (var link in links[aDevice])
                        {
                            if (link.Equals(aIf, zDevice, zIf))
                            {
                                Console.Error.WriteLine($"{aDevice} found in Links.csv with {aIf} connected to {zDevice}:{zIf}");
                                cont = true;
                            }
                        }
                    }
                    if (cont)
                    {
                        continue;
                    }
                    var wiring = new Wiring(
                        new Start(
                            aDevice,
                            aIf),
                        new End(
                            zDevice,
                            zIf),
                        new DevicePattern(WiringType.Serial),
                        Models.Action.Add);
                    seed.Wiring.Add(wiring);
                }
                return seed;
            }
            else
            {
                Console.WriteLine($"Failed to load csv file.");
            }
            return null;
        }

        /// <summary>
        /// The load links csv.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        private static Dictionary<string, List<Links>> LoadLinksCSV(string path)
        {
            if (File.Exists(path))
            {
                var rows = File.ReadLines(path).Select(line =>
                {
                    if (!line.Contains("#"))
                    {
                        return line.Split(',');
                    }
                    return null;
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
                            dict.Add(a_device, new List<Links> { new Links(a_device, a_if, z_device, z_if, speed) });
                        }
                        else
                        {
                            dict[a_device].Add(new Links(a_device, a_if, z_device, z_if, speed));
                        }
                    }
                }
                return dict;
            }

            Console.WriteLine($"Failed to load the Links.csv file from directory:{linksFilePath}");
            return null;
        }

        private static string ConvertDeviceNameToType(string deviceName)
        {
            if (deviceName.Contains("car", StringComparison.InvariantCultureIgnoreCase))
            {
                return "CorporateAggregateRouter";
            }
            if (deviceName.Contains("ier", StringComparison.InvariantCultureIgnoreCase))
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
