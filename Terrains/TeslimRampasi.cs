using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Robotun paketini teslim ederek haritadan ayrıldığı rampayı tanımlar.
    public class TeslimRampasi : Zemin
    {
        public override char Sembol => 'H';

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            return EnerjiMaliyetleri.Teslim;
        }

        public override void VarisSonrasiUygula(Robot robot)
        {
            robot.TeslimEt();
        }
    }
}
