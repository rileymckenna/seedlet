using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CSVToSeed.Models
{
	[XmlRoot(ElementName = "DevicePattern")]
	public class DevicePattern
	{
		[XmlAttribute(AttributeName = "LinkCountScope")]
		public string LinkCountScope { get; set; }
		[XmlAttribute(AttributeName = "LinkCount")]
		public int LinkCount { get; set; }
		[XmlAttribute(AttributeName = "LinkState")]
		public string LinkState { get; set; }

		private DevicePattern() { }
		public DevicePattern(string linkCountScope = "Local", int linkCount = 1, string linkState = "Production")
        {
			this.LinkCountScope = linkCountScope;
			this.LinkCount = linkCount;
			this.LinkState = linkState;
        }
	}
}
