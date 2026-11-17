using System;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;
using GezegenPostacilari.Terrains;

namespace GezegenPostacilari.Commands
{
    // Robotun yönüne göre ilerlemesini zemin davranışlarına yönlendirir.
    public class IleriKomutu : Komut
    {
        public int AdimSayisi { get; }

        public IleriKomutu(int adimSayisi)
        {
            if (adimSayisi < 1 || adimSayisi > 99)
            {
                throw new ArgumentOutOfRangeException(nameof(adimSayisi));
            }

            AdimSayisi = adimSayisi;
        }

        public override void Uygula(Robot robot, Simulasyon simulasyon)
        {
            if (robot is null)
            {
                throw new ArgumentNullException(nameof(robot));
            }

            if (simulasyon is null)
            {
                throw new ArgumentNullException(nameof(simulasyon));
            }

            for (int uygulananAdim = 0; uygulananAdim < AdimSayisi; uygulananAdim++)
            {
                if (robot.Durum != RobotDurumu.AKTIF)
                {
                    break;
                }

                (int yatayFark, int dikeyFark) = YonFarkiniGetir(robot.Yon);
                int hedefX = robot.Konum.X + yatayFark;
                int hedefY = robot.Konum.Y + dikeyFark;

                if (!simulasyon.Harita.SinirlarIcinde(hedefX, hedefY) ||
                    simulasyon.HucreDoluMu(hedefX, hedefY, robot))
                {
                    break;
                }

                Konum girisKonumu = new Konum(hedefX, hedefY);
                Zemin zemin = simulasyon.Harita.ZeminGetir(hedefX, hedefY);

                if (!zemin.EtkilesimiUygula(
                    robot,
                    simulasyon,
                    girisKonumu,
                    yatayFark,
                    dikeyFark))
                {
                    break;
                }
            }
        }

        private static (int YatayFark, int DikeyFark) YonFarkiniGetir(Yon yon)
        {
            return yon switch
            {
                Yon.N => (0, -1),
                Yon.E => (1, 0),
                Yon.S => (0, 1),
                Yon.W => (-1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(yon))
            };
        }
    }
}
