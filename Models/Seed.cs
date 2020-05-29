using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CSVToSeed.Models
{
	[XmlRoot(ElementName = "Seed")]
	public class Seed
	{
		[XmlElement(ElementName = "Wiring")]
		public List<Wiring> Wiring { get; set; }
		[XmlAttribute(AttributeName = "DcFolder")]
		public string DcFolder { get; set; }

		private Seed() {
			this.Wiring = new List<Wiring>();
		}
		public Seed(string dcFolder = "WAN")
        {
			this.Wiring = new List<Wiring>();
			this.DcFolder = dcFolder;
        }
	}
}
