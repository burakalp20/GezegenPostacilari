using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Tüm zemin türlerinin ortak özelliklerini ve hareket kurallarını tanımlar.
    public abstract class Zemin
    {
        public abstract char Sembol { get; }

        public abstract int EnerjiMaliyeti(RobotGovdesi govde);

        public virtual bool GecilebilirMi(RobotGovdesi govde)
        {
            return true;
        }
    }
}

