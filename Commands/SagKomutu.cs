using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Commands
{
    // Aktif robotu enerji harcamadan 90 derece sağa döndürür.
    public class SagKomutu : Komut
    {
        public override void Uygula(Robot robot, Simulasyon simulasyon)
        {
            robot.SagaDon();
        }
    }
}


