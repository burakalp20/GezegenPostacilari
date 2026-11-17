using GezegenPostacilari.Constants;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Terrains
{
    // Toz fırtınasının enerji maliyetini ve havalı robotun savrulmasını yönetir.
    public class TozFirtinasi : Zemin
    {
        public override char Sembol => 'T';

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            if (govde.Ucabilir)
            {
                return EnerjiMaliyetleri.TozHavali;
            }

            return EnerjiMaliyetleri.TozNormal;
        }

        public override bool EtkilesimiUygula(
            Robot robot,
            Simulasyon simulasyon,
            Konum girisKonumu,
            int yatayFark,
            int dikeyFark)
        {
            if (!robot.Govde.Ucabilir)
            {
                return base.EtkilesimiUygula(
                    robot,
                    simulasyon,
                    girisKonumu,
                    yatayFark,
                    dikeyFark);
            }

            robot.EnerjiHarca(EnerjiMaliyetleri.TozHavali);
            return false;
        }
    }
}
