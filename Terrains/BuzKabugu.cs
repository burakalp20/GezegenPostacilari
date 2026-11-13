using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Buz kabuğunun robotun kayma ve uçma yeteneklerine göre enerji maliyetini belirler.
    public class BuzKabugu : Zemin
    {
        public override char Sembol => 'I';

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            if (govde.Kayabilir)
                return EnerjiMaliyetleri.BuzKayma;

            if (govde.Ucabilir)
                return EnerjiMaliyetleri.BuzHavali;

            return EnerjiMaliyetleri.BuzNormal;
        }
    }
}
