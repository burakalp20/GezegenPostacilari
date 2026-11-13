using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Robots
{
    // Buz üzerinde kayma yeteneğine sahip robot gövdesini tanımlar.
    public class KizakliGovde : RobotGovdesi
    {
        public override string Ad => "KIZAKLI";

        public override bool Kayabilir => true;
    }
}
