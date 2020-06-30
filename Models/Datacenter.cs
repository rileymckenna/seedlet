// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Datacenter.cs" company="Microsoft">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the Datacenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



namespace CSVToSeed.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// The datacenter class.
    /// </summary>
    [XmlRoot(ElementName = "Datacenter")]
    public class Datacenter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Datacenter"/> class. 
        /// Prevents a default instance of the <see cref="Datacenter"/> class from being created.
        /// </summary>
        /// <param name="pid">
        /// The pid.
        /// </param>
        /// <param name="intent">
        /// The intent.
        /// </param>
        /// <param name="deleted">
        /// The deleted.
        /// </param>
        /// <param name="rack_hostName">
        /// The rack_host Name.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public Datacenter(string pid, string intent, bool deleted, string rack_hostName)
        {
            this.PID = pid;
            this.Intent = intent;
            this.Deleted = deleted;
            this.Rack = new Rack(rack_hostName);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Datacenter"/> class from being created. 
        /// Initializes a new instance of the <see cref="Datacenter"/> class.
        /// </summary>
        private Datacenter()
        {
        }

        /// <summary>
        /// Gets or sets the pid.
        /// </summary>
        [XmlAttribute(AttributeName = "PID")]
        public string PID { get; set; }

        /// <summary>
        /// Gets or sets the intent.
        /// </summary>
        [XmlAttribute(AttributeName = "Intent")]
        public string Intent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether deleted.
        /// </summary>
        [XmlAttribute(AttributeName = "Deleted")]
        public bool Deleted { get; set;  }

        /// <summary>
        /// Gets or sets the rack.
        /// </summary>
        [XmlElement(ElementName = "Rack")]
        public Rack Rack { get; set; }

    }
}
