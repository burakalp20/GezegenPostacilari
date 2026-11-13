using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Bütün robotların 1 enerji harcayarak geçebildiği düz zemini tanımlar.
    public class DuzZemin : Zemin
    {
        public override char Sembol => '.';

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            return EnerjiMaliyetleri.Duz;
        }
    }
}
