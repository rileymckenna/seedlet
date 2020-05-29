using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CSVToSeed.Models
{

    [XmlRoot(ElementName = "End")]
    public class End : Metadata
    {
        public End() { }
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
