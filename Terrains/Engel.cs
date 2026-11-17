using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Terrains
{
    // Robotların geçemediği ve üzerine sıçrayarak inemediği engeli tanımlar.
    public class Engel : Zemin
    {
        public override char Sembol => 'X';
        public override bool SicramaInisineUygunMu => false;

        public override bool GecilebilirMi(RobotGovdesi govde)
        {
            return false;
        }

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            return EnerjiMaliyetleri.GecisYok;
        }
    }
}
