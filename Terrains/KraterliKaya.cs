using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Kraterli kayanın robotun uçma ve sıçrama yeteneklerine göre enerji maliyetini belirler.
    public class KraterliKaya : Zemin
    {
        public override char Sembol => 'K';

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            if (govde.Sicrayabilir)
                return EnerjiMaliyetleri.KayaSicrama;

            if (govde.Ucabilir)
                return EnerjiMaliyetleri.KayaHavali;

            return EnerjiMaliyetleri.KayaNormal;
        }
    }
}
