using GezegenPostacilari.Constants;
using GezegenPostacilari.Robots;
using System.Runtime.CompilerServices;

namespace GezegenPostacilari.Terrains
{
    // Bataklığın geçiş maliyetini belirler ve güvenli olmayan sıçrama inişlerini engeller.
    public class Bataklik : Zemin
    {
        public override char Sembol => 'B';
        public override bool SicramaInisineUygunMu => false;

        [ModuleInitializer]
        internal static void KayitOl()
        {
            ZeminFabrikasi.Kaydet(new Bataklik());
        }

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            ArgumentNullException.ThrowIfNull(govde);
            return govde.Ucabilir
                ? EnerjiMaliyetleri.BataklikHavali
                : EnerjiMaliyetleri.BataklikNormal;
        }
    }
}
