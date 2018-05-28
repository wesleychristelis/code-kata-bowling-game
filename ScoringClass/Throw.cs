using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoringClass
{
    public class Throw
    {
        public int? Pins { get; set; }
         
        public int Hit(int pins)
        {
            Pins = pins;

            return Pins.Value;
        }
    }
}
