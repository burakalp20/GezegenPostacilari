using GezegenPostacilari.Constants;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Terrains
{
    // Kraterli kayanın enerji maliyetini ve sıçrayan robotun atlayışını yönetir.
    public class KraterliKaya : Zemin
    {
        public override char Sembol => 'K';
        public override bool SicramaInisineUygunMu => false;

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            if (govde.Sicrayabilir)
            {
                return EnerjiMaliyetleri.KayaSicrama;
            }

            if (govde.Ucabilir)
            {
                return EnerjiMaliyetleri.KayaHavali;
            }

            return EnerjiMaliyetleri.KayaNormal;
        }

        public override bool EtkilesimiUygula(
            Robot robot,
            Simulasyon simulasyon,
            Konum girisKonumu,
            int yatayFark,
            int dikeyFark)
        {
            if (!robot.Govde.Sicrayabilir)
            {
                return base.EtkilesimiUygula(
                    robot,
                    simulasyon,
                    girisKonumu,
                    yatayFark,
                    dikeyFark);
            }

            int inisX = girisKonumu.X + yatayFark;
            int inisY = girisKonumu.Y + dikeyFark;

            if (!simulasyon.Harita.SinirlarIcinde(inisX, inisY) ||
                simulasyon.HucreDoluMu(inisX, inisY, robot))
            {
                return false;
            }

            Zemin inisZemini = simulasyon.Harita.ZeminGetir(inisX, inisY);

            if (!inisZemini.SicramaInisineUygunMu)
            {
                return false;
            }

            bool hareketGerceklesti = robot.HareketEt(
                new Konum(inisX, inisY),
                EnerjiMaliyetleri.KayaSicrama);

            if (!hareketGerceklesti)
            {
                return false;
            }

            inisZemini.VarisSonrasiUygula(robot);
            return robot.Durum == RobotDurumu.AKTIF;
        }
    }
}
