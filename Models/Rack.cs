// //  ---------------------------------------------------------------------------
// //  <copyright file="Rack.cs" company="Microsoft">
// //     Copyright (c) Microsoft Corporation.  All rights reserved.
// //  </copyright>
// //  ---------------------------------------------------------------------------

namespace CSVToSeed.Models
{
    using System.Xml.Serialization;

    /// <summary>
    /// The rack.
    /// </summary>
    public class Rack
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rack"/> class.
        /// </summary>
        /// <param name="hostName">
        /// The host Name.
        /// </param>
        public Rack(string hostName)
        {
            this.HostName = hostName;
        }

        private Rack()
        {
        }

        /// <summary>
        /// Gets or sets the host name.
        /// </summary>
        [XmlAttribute(AttributeName = "Hostname")]
        public string HostName { get; set; }
    }
}