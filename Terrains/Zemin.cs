using System;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Terrains
{
    // Tüm zemin türlerinin ortak enerji ve hareket davranışlarını tanımlar.
    public abstract class Zemin
    {
        public abstract char Sembol { get; }
        public virtual bool KaymaYuzeyiMi => false;
        public virtual bool SicramaInisineUygunMu => true;

        public abstract int EnerjiMaliyeti(RobotGovdesi govde);

        public virtual bool GecilebilirMi(RobotGovdesi govde)
        {
            if (govde is null)
            {
                throw new ArgumentNullException(nameof(govde));
            }

            return true;
        }

        public virtual bool EtkilesimiUygula(
            Robot robot,
            Simulasyon simulasyon,
            Konum girisKonumu,
            int yatayFark,
            int dikeyFark)
        {
            if (!GecilebilirMi(robot.Govde))
            {
                return false;
            }

            bool hareketGerceklesti = robot.HareketEt(
                girisKonumu,
                EnerjiMaliyeti(robot.Govde));

            if (!hareketGerceklesti)
            {
                return false;
            }

            VarisSonrasiUygula(robot);
            return robot.Durum == RobotDurumu.AKTIF;
        }

        public virtual void VarisSonrasiUygula(Robot robot)
        {
        }
    }
}
