// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevicePattern.cs" company="Microsoft">
//   All rights reserved
// </copyright>
// <summary>
//   The Device pattern
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CSVToSeed.Models
{
    using System.Xml.Serialization;

    /// <summary>
    /// The Device pattern
    /// </summary>
    /// [XmlRoot(ElementName = "DevicePattern")]
    public class DevicePattern
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DevicePattern"/> class.
        /// </summary>
        /// <param name="linkCountScope">
        /// The link count scope.
        /// </param>
        /// <param name="linkCount">
        /// The link count.
        /// </param>
        /// <param name="linkState">
        /// The link state.
        /// </param>
        public DevicePattern(string linkCountScope = "Local", string linkCount = "1", string linkState = "Production")
        {
            this.LinkCountScope = linkCountScope;
            this.LinkCount = linkCount;
            this.LinkState = linkState;
            this.WiringType = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicePattern"/> class.
        /// </summary>
        /// <param name="wiringType">
        /// The wiring type.
        /// </param>
        public DevicePattern(WiringType wiringType)
        {
            this.WiringType = wiringType.ToString();
            this.LinkCount = null;
            this.LinkState = null;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DevicePattern"/> class from being created.
        /// </summary>
        private DevicePattern()
        {
        }

        /// <summary>
        /// Gets or sets the link count scope.
        /// </summary>
        [XmlAttribute(AttributeName = "LinkCountScope")]
        public string LinkCountScope { get; set; }

        /// <summary>
        /// Gets or sets the link count.
        /// </summary>
        [XmlAttribute(AttributeName = "LinkCount")]
        public string LinkCount { get; set; }

        /// <summary>
        /// Gets or sets the link state.
        /// </summary>
        [XmlAttribute(AttributeName = "LinkState")]
        public string LinkState { get; set; }

        /// <summary>
        /// Gets or sets the wiring type.
        /// </summary>
        [XmlAttribute(AttributeName = "WiringType")]
        public string WiringType { get; set; }
    }
}
