using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Toz fırtınasının robotun uçuş yeteneğine göre enerji maliyetini belirler.
    public class TozFirtinasi : Zemin
    {
        public override char Sembol => 'T';

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            if (govde.Ucabilir)return EnerjiMaliyetleri.TozHavali;

            return EnerjiMaliyetleri.TozNormal;
        }
    }
}

