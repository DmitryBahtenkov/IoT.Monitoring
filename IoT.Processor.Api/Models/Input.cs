using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.Processor.Api.Models
{
    public record Input
    {
        public double? Temp { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public DateTime Time { get; set; }
    }
}