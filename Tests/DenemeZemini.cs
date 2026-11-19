using System.Runtime.CompilerServices;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Terrains;

namespace GezegenPostacilari.Dogrulama
{
    // Yeni zemin kaydının okuyucu, hareket ve görünüm tarafından kullanılmasını sınar.
    internal sealed class DenemeZemini : Zemin
    {
        private const int UcanGecisMaliyeti = 2;
        private const int NormalGecisMaliyeti = 4;
        public override char Sembol { get; }

        public DenemeZemini(char sembol = 'Q')
        {
            Sembol = sembol;
        }

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            return govde.Ucabilir ? UcanGecisMaliyeti : NormalGecisMaliyeti;
        }

        [ModuleInitializer]
        internal static void KayitOl()
        {
            ZeminFabrikasi.Kaydet(new DenemeZemini());
        }
    }
}
