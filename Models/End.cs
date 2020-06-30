// --------------------------------------------------------------------------------------------------------------------
// <copyright file="End.cs" company="Microsoft">
//   All Rights reserved.
// </copyright>
// <summary>
//   Defines the End type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CSVToSeed.Models
{
    using System.Xml.Serialization;

    /// <summary>
    /// The end.
    /// </summary>
    [XmlRoot(ElementName = "End")]
    public class End : Metadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="End"/> class.
        /// </summary>
        public End()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="End"/> class for console wirings.
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
        public End(string deviceName, string itfNames, string scope = "Datacenter")
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
        /// Initializes a new instance of the <see cref="End"/> class for standard wirings.
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
        public End(string deviceType, string deviceName, string itfNames, string connectorType, string portchannel, string ipv4, string ipv6, string scope = "Datacenter")
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
