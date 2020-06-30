// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Seed.cs" company="Microsoft">
//   ALl rights reserved.
// </copyright>
// <summary>
//   Defines the Seed type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CSVToSeed.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot(ElementName = "Seed")]
    public class Seed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Seed"/> class.
        /// </summary>
        /// <param name="dcFolder">
        /// The dc folder.
        /// </param>
        public Seed(string dcFolder = "WAN")
        {
            this.Wiring = new List<Wiring>();
            this.Datacenters = new List<Datacenter>();
            this.DcFolder = dcFolder;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Seed"/> class from being created.
        /// </summary>
        private Seed()
        {
            this.Wiring = new List<Wiring>();
        }

        /// <summary>
        /// Gets or sets the wiring.
        /// </summary>
        [XmlElement(ElementName = "Wiring")]
        public List<Wiring> Wiring { get; set; }

        /// <summary>
        /// Gets or sets the datacenters.
        /// </summary>
        [XmlElement(ElementName = "Datacenters")]
        public List<Datacenter> Datacenters { get; set; }

        /// <summary>
        /// Gets or sets the dc folder.
        /// </summary>
        [XmlAttribute(AttributeName = "DcFolder")]
        public string DcFolder { get; set; }
    }
}
