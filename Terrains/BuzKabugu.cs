using GezegenPostacilari.Constants;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Terrains
{
    // Buz kabuğunun enerji maliyetini ve kızaklı robotun kaymasını yönetir.
    public class BuzKabugu : Zemin
    {
        public override char Sembol => 'I';
        public override bool KaymaYuzeyiMi => true;

        public override int EnerjiMaliyeti(RobotGovdesi govde)
        {
            if (govde.Kayabilir)
            {
                return EnerjiMaliyetleri.BuzKayma;
            }

            if (govde.Ucabilir)
            {
                return EnerjiMaliyetleri.BuzHavali;
            }

            return EnerjiMaliyetleri.BuzNormal;
        }

        public override bool EtkilesimiUygula(
            Robot robot,
            Simulasyon simulasyon,
            Konum girisKonumu,
            int yatayFark,
            int dikeyFark)
        {
            if (!robot.Govde.Kayabilir)
            {
                return base.EtkilesimiUygula(
                    robot,
                    simulasyon,
                    girisKonumu,
                    yatayFark,
                    dikeyFark);
            }

            Konum kaymaSonu = girisKonumu;

            while (true)
            {
                int sonrakiX = kaymaSonu.X + yatayFark;
                int sonrakiY = kaymaSonu.Y + dikeyFark;

                if (!simulasyon.Harita.SinirlarIcinde(sonrakiX, sonrakiY) ||
                    simulasyon.HucreDoluMu(sonrakiX, sonrakiY, robot))
                {
                    break;
                }

                Zemin sonrakiZemin = simulasyon.Harita.ZeminGetir(
                    sonrakiX,
                    sonrakiY);

                if (!sonrakiZemin.KaymaYuzeyiMi)
                {
                    break;
                }

                kaymaSonu = new Konum(sonrakiX, sonrakiY);
            }

            return robot.HareketEt(
                kaymaSonu,
                EnerjiMaliyetleri.BuzKayma);
        }
    }
}
