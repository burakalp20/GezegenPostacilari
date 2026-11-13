using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Metan gölünden yalnızca uçabilen robotların geçebilmesini sağlar.
    public class MetanGolu : Zemin
    {
        public override char Sembol => '~';

        public override bool GecilebilirMi(RobotGovdesi govde)
        {
            return govde.Ucabilir;
        }

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            return EnerjiMaliyetleri.GolHavali;
        }
    }
}
