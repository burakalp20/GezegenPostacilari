using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Metan gölünden yalnızca uçabilen robotların geçmesini sağlar.
    public class MetanGolu : Zemin
    {
        public override char Sembol => '~';
        public override bool SicramaInisineUygunMu => false;

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
