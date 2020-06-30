// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Start.cs" company="Microsoft">
//   All rights reserved.
// </copyright>
// <summary>
//   Defines the Start type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CSVToSeed.Models
{
    using System.Xml.Serialization;

    /// <summary>
    /// The start.
    /// </summary>
    [XmlRoot(ElementName = "Start")]
    public class Start : Metadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Start"/> class.
        /// </summary>
        public Start() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Start"/> class for CONSOLE connections.
        /// </summary>
        /// <param name="deviceName">
        /// The device name.
        /// </param>
        /// <param name="itfNames">
        /// The itf names.
        /// </param>
        /// <param name="scope">
        /// The scope.
        /// </param>
        public Start(string deviceName, string itfNames, string scope = "Datacenter")
        {
            this.DeviceType = null;
            this.DeviceRegex = $"^{deviceName}$";
            this.Scope = scope;
            this.ItfNames = itfNames;
            this.ConnectorType = null;
            this.PortChannel = null;
            this.IPv4 = null;
            this.IPv6 = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Start"/> class for standard wiring connections.
        /// </summary>
        /// <param name="deviceType">
        /// The device type.
        /// </param>
        /// <param name="deviceName">
        /// The device name.
        /// </param>
        /// <param name="itfNames">
        /// The itf names.
        /// </param>
        /// <param name="connectorType">
        /// The connector type.
        /// </param>
        /// <param name="portchannel">
        /// The portchannel.
        /// </param>
        /// <param name="ipv4">
        /// The ipv 4.
        /// </param>
        /// <param name="ipv6">
        /// The ipv 6.
        /// </param>
        /// <param name="scope">
        /// The scope.
        /// </param>
        public Start(string deviceType, string deviceName, string itfNames, string connectorType, string portchannel, string ipv4, string ipv6, string scope = "Datacenter")
        {
            this.DeviceType = deviceType;
            this.DeviceRegex = $"^{deviceName}$";
            this.Scope = scope;
            this.ItfNames = itfNames;
            this.ConnectorType = connectorType;
            this.PortChannel = portchannel;
            this.IPv4 = ipv4;
            this.IPv6 = ipv6;
        }
    }
}
