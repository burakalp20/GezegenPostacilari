using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Robots
{
    // Kraterli kayaların üzerinden atlayabilen robot gövdesini tanımlar.
    public class SicrayanGovde : RobotGovdesi
    {
        public override string Ad => "SICRAYAN";

        public override bool Sicrayabilir => true;
    }
}
