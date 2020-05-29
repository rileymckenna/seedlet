using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CSVToSeed.Models
{
    public class Metadata
    {
		[XmlAttribute(AttributeName = "DeviceType")]
		public string DeviceType { get; set; }
		[XmlAttribute(AttributeName = "DeviceRegex")]
		public string DeviceRegex { get; set; }
		[XmlAttribute(AttributeName = "Scope")]
		public string Scope { get; set; }
		[XmlAttribute(AttributeName = "ItfNames")]
		public string ItfNames { get; set; }
		[XmlAttribute(AttributeName = "ConnectorType")]
		public string ConnectorType { get; set; }
		[XmlAttribute(AttributeName = "PortChannel")]
		public string PortChannel { get; set; }
		[XmlAttribute(AttributeName = "IPv4")]
		public string IPv4 { get; set; }
		[XmlAttribute(AttributeName = "IPv6")]
		public string IPv6 { get; set; }
	}
}
