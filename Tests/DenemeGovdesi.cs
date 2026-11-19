using System.Runtime.CompilerServices;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Dogrulama
{
    // Üretim sınıfları düzenlenmeden eklenen uçabilen test gövdesini tanımlar.
    internal sealed class DenemeGovdesi : RobotGovdesi
    {
        public override string Ad { get; }
        public override bool Ucabilir => true;

        public DenemeGovdesi(string ad = "DENEMEUCAN")
        {
            Ad = ad;
        }

        [ModuleInitializer]
        internal static void KayitOl()
        {
            GovdeFabrikasi.Kaydet(new DenemeGovdesi());
        }
    }
}
