using System;
using System.Collections.Generic;
using System.Text;

namespace CSVToSeed.Models
{
    public class Links
    {
        public string StartDevice { get; set; }
        public string StartPort { get; set; }
        public string EndDevice { get; set; }
        public string EndPort { get; set; }
        public string LinkSpeed { get; set; }
        public Links(string a_device, string a_if, string z_device, string z_if, string linkSpeed)
        {
            this.StartDevice = a_device;
            this.StartPort = a_if;
            this.EndDevice = z_device;
            this.EndPort = z_if;
            this.LinkSpeed = linkSpeed;
        }

        public bool Equals(string startport, string end, string endport)
        {
            return this.StartPort.Contains(startport, StringComparison.InvariantCultureIgnoreCase) &&
                this.EndDevice.Contains(end, StringComparison.InvariantCultureIgnoreCase) &&
                this.EndPort.Contains(endport, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
