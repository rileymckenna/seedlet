using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CSVToSeed.Models
{
    [XmlRoot(ElementName = "Wiring")]
    public class Wiring
    {
        [XmlElement(ElementName = "Start")]
        public Start Start { get; set; }
        [XmlElement(ElementName = "End")]
        public End End { get; set; }
        [XmlElement(ElementName = "DevicePattern")]
        public DevicePattern DevicePattern { get; set; }
        [XmlAttribute(AttributeName = "Action")]
        public Action Action { get; set; }

        public Wiring()
        {
            this.Start = new Start();
            this.End = new End();
            this.DevicePattern = new DevicePattern();
        }
        public Wiring(Start start, End end, DevicePattern devicePattern, Action action)
        {
            this.Start = start;
            this.End = end;
            this.DevicePattern = devicePattern;
            this.Action = action;
        }
    }
}
