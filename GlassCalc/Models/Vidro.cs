using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassCalc.Models
{
    public class Vidro
    {
        public int Id { get; set; }
        public string Material { get; set; }
        public bool IsDoubleGlass { get; set; }
        public string WinType { get; set; }
        public int AirDimension { get; set; }
        public bool IsLowE { get; set; }
        public double Uw { get; set; }
        public string OclusionType { get; set; }
        public double Uwdn { get; set; }

    }
}
